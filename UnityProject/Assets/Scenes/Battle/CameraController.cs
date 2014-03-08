using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private VictoryCondition victoryCondition;
    public Tethered tether;

    bool isStart = true;

    public GameObject StartMarker;
    public GameObject EndMarker;

    public Camera camera;    

    void Start()
    {
        victoryCondition = GameObject.Find("GameController").GetComponent<VictoryCondition>();

        this.StartSlerpCoroutine(camera.gameObject, StartMarker, EndMarker, 2f);
    }

    void Update()
    {
        if (isStart)
        {
            if (camera.gameObject.transform.position == EndMarker.transform.position)
            {
                isStart = false;

                camera.GetComponent<DockedCamera>().Dock = EndMarker;
            }
        }

        if (victoryCondition.IsMatchOver)
        {
            tether.Paused = false;            
        }
    }
}
