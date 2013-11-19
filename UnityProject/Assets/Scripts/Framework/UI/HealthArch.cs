using UnityEngine;
using System.Collections;

public class HealthArch : MonoBehaviour
{
    public Stat<LilithStats> Stat;
    private GameObject cube;

    // Use this for initialization
    void Start()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = this.gameObject.transform;
        cube.transform.localPosition = new Vector3(0, 3f, 0);
        cube.transform.localScale = new Vector3(1, .25f, 0f);
        cube.RemoveComponent<BoxCollider>();
        cube.GetComponent<MeshRenderer>().material.color = new Color(.6f, 0f, 0f, .5f);
        cube.GetComponent<MeshRenderer>().material.shader = Shader.Find("Transparent/Cutout/Soft Edge Unlit");
        cube.transform.localRotation = new Quaternion();
        cube.EnsureComponent<LookAtCamera>();
    }

    void Update()
    {
        if(Stat != null)
        {
            cube.SetActive(Stat.CurrentValue != 0);

            cube.transform.localScale = new Vector3(Stat.CurrentRatio, cube.transform.localScale.y, cube.transform.localScale.z);
        }
    }


}
