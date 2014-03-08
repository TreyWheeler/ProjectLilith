using UnityEngine;
using System.Collections;

public class Tethered : MonoBehaviour
{
    public float radius = 7f;
    public float tetherHeight = 12f;
    public float secondsPerRevolution = 17f;

    public GameObject tetheredItem;

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

            tetheredItem.transform.position = new Vector3(transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle), tetherHeight, transform.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
            tetheredItem.gameObject.LookAt(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 lastposition = Vector3.zero;

        for (int i = 0; i < 361; i++)
        {
            var position = new Vector3(transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * i), tetherHeight, transform.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * i));

            if (lastposition != Vector3.zero)
                Gizmos.DrawLine(lastposition, position);

            lastposition = position;
        }

        if (!Loaded)
        {
            tetheredItem.transform.position = lastposition;
            tetheredItem.gameObject.LookAt(this.gameObject);
        }
    }


}
