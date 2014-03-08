using UnityEngine;
using System.Collections;

public class Tethered : MonoBehaviour
{
    public float radius = 7;
    public float height = 10;
    public float secondsPerRevolution = 17f;

    public Camera camera;

    private float progress = 0f;

    public bool Paused = false;
    public bool Loaded = false;

    void Start()
    {
        Loaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
        {
            progress += Time.deltaTime / secondsPerRevolution;

            if (progress > 1f)
            {
                progress -= 1f;
            }

            float angle = progress * 360f;

            camera.transform.position = new Vector3(transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle), height, transform.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
            camera.gameObject.LookAt(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 lastposition = Vector3.zero;

        for (int i = 0; i < 361; i++)
        {
            var position = new Vector3(transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * i), height, transform.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * i));

            if (lastposition != Vector3.zero)
                Gizmos.DrawLine(lastposition, position);

            lastposition = position;
        }

        if (!Loaded)
        {            
            camera.transform.position = lastposition;
            camera.gameObject.LookAt(this.gameObject);
        }
    }

    
}
