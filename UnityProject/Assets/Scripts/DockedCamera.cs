using UnityEngine;
using System.Collections;

public class DockedCamera : MonoBehaviour
{

    public Camera Camera;
    public GameObject Dock;

    void Update()
    {
        if (Dock == null)
            return;

        Camera.transform.position = Dock.transform.position;
        Camera.transform.rotation = Dock.transform.rotation;
    }
}
