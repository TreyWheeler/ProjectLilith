using UnityEngine;
using System.Collections;
using System;

public class IsEnabled : MonoBehaviour
{

    public Func<bool> Predicate;

    UIButton buttonUI;
    BoxCollider buttonBoxCollider;
    // Use this for initialization
    void Start()
    {
        buttonUI = gameObject.GetComponent<UIButton>();
        buttonBoxCollider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Predicate != null)
        {
            bool isEnabled = Predicate();
            buttonUI.isEnabled = isEnabled;
            //buttonBoxCollider.enabled = isEnabled;
        }
    }
}
