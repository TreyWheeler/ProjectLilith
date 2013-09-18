using UnityEngine;
using System.Collections;

public class TouchGameObject : MonoBehaviour
{
    void Start()
    {
        this.gameObject.AddOnClick(() =>
        {
            Debug.Log(gameObject.name);
        });
    }
}
