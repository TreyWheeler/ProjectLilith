using UnityEngine;
using System.Collections;

public class TouchGameObject : MonoBehaviour
{
    int radius = 3;

    void Start()
    {
        this.gameObject.AddOnClick(() =>
        {
            Debug.Log(gameObject.name);
            var characterCollider = this.gameObject.GetComponent<CapsuleCollider>();
            var characterBounds = characterCollider.bounds;
            for (float i = 0; i < 6; i++)
            {

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                sphere.name = "hi";
                //sphere.transform.position = new Vector3(bleh.center.x, bleh.max.y, bleh.center.z);
                //sphere.transform.RotateAround(sphere.transform.position + new Vector3(0,5,0), 360 / i);// = new Vector3(bleh.center.x, bleh.max.y, bleh.center.z);

                float radialCenterX = characterBounds.center.x;
                float radialCenterY = characterBounds.center.y + 1;
                float radialCenterZ = characterBounds.center.z;

                float angle = 360 * (i / 6f) * Mathf.Deg2Rad;

                float sphereX = radialCenterX + radius * Mathf.Cos(angle);
                float sphereZ = radialCenterZ + radius * Mathf.Sin(angle);

                sphere.transform.position = new Vector3(sphereX, radialCenterY, sphereZ);

                //Camera.current.transform.rotation;


            }
        });
    }
}
