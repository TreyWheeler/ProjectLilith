using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityRadial : MonoBehaviour
{
    int radius = 2;

    List<GameObject> radialOptions = new List<GameObject>();

    public Ability[] Abilities;

    GameObject radialMenu;

    void Start()
    {
        if (!this.gameObject.GetComponent<Character>().Team2)
        {
            this.gameObject.AddOnClick(() =>
            {
                if (!gameObject.GetComponent<Character>().IsAlive)
                    return;

                radialMenu = new GameObject();
                radialMenu.name = "Radial Menu";

                radialMenu.AddOnOutsideClick(() =>
                {
                    foreach (GameObject child in radialMenu.GetChildren())
                    {
                        child.EnsureComponent<Rigidbody>();
                        radialOptions.Remove(child);
                    }

                    Destroy(radialMenu, 3);
                });

                for (float i = 0; i < Abilities.Length; i++)
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.parent = radialMenu.transform;

                    radialOptions.Add(sphere);

                    float angle = 360 * (i / (float)Abilities.Length) * Mathf.Deg2Rad;

                    float sphereX = radius * Mathf.Cos(angle);
                    float sphereZ = radius * Mathf.Sin(angle);

                    sphere.transform.localPosition = new Vector3(sphereX, 0, sphereZ);
                    AbilitySphere abilitySphere = sphere.AddComponent<AbilitySphere>();
                    abilitySphere.Ability = Abilities[(int)i];
                    abilitySphere.OriginatingGameObject = this.gameObject;
                }
            });
        }
    }

    void Update()
    {
        if (radialMenu != null)
        {
            var characterCollider = this.gameObject.GetComponent<CapsuleCollider>();
            var characterBounds = characterCollider.bounds;

            float radialCenterX = characterBounds.center.x;
            float radialCenterY = characterBounds.center.y;// + 1;
            float radialCenterZ = characterBounds.center.z;

            radialMenu.transform.position = new Vector3(radialCenterX, radialCenterY, radialCenterZ);

            radialMenu.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
        }
    }




    void OnDestroy()
    {
        foreach (var radialOption in radialOptions)
        {
            Destroy(radialOption);
        }
    }




    internal void Close()
    {
        Destroy(radialMenu);
    }
}
