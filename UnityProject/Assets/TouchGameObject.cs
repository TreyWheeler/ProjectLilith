using UnityEngine;
using System.Collections;

public class TouchGameObject : MonoBehaviour
{
    int radius = 2;

    void Start()
    {
        this.gameObject.AddOnClick(() =>
        {
            Debug.Log(gameObject.name);
            var characterCollider = this.gameObject.GetComponent<CapsuleCollider>();
            var characterBounds = characterCollider.bounds;

            //Camera.current.transform.rotation;

            GameObject radialMenu = new GameObject();
            radialMenu.name = "RadialMenu";

            float radialCenterX = characterBounds.center.x;
            float radialCenterY = characterBounds.center.y;// + 1;
            float radialCenterZ = characterBounds.center.z;

            radialMenu.transform.position = new Vector3(radialCenterX, radialCenterY, radialCenterZ);

            radialMenu.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
            
            radialMenu.AddOnOutsideClick(() =>
            {
                foreach (GameObject child in radialMenu.GetChildren())
                {
                    child.EnsureComponent<Rigidbody>();
                }

                Destroy(radialMenu, 3);
            });

            for (float i = 0; i < 6; i++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.parent = radialMenu.transform;



                float angle = 360 * (i / 6f) * Mathf.Deg2Rad;

                float sphereX = radius * Mathf.Cos(angle);//radialCenterX + radius * Mathf.Cos(angle);
                float sphereZ = radius * Mathf.Sin(angle);//radialCenterZ + radius * Mathf.Sin(angle);

                sphere.transform.localPosition = new Vector3(sphereX, 0, sphereZ);
                sphere.AddComponent<AbilitySphere>();
            }
        });
    }
}
