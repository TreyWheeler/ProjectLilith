using UnityEngine;
using System.Collections;

public class TouchGameObject : MonoBehaviour
{

    GameObject go;
    bool mouseWasDown;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mouseWasDown && Input.GetMouseButtonUp(0))
        {
            mouseWasDown = false;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                go = hit.transform.gameObject;

                Debug.Log(go.name);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseWasDown = true;
        }
    }
}
