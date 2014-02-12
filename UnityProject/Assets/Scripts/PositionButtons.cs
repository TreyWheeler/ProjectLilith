using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PositionButtons : MonoBehaviour
{
    private List<GameObject> _buttonList = new List<GameObject>();
    private RotateScript _positioningObject;

    public GameObject basePrefab;
    public float radius = 100;
    public float startAngle = 0;
    public float endAngle = 360;

    // Use this for initialization
    void Start()
    {
        _positioningObject = this.gameObject.GetComponent<RotateScript>();
    }

    private void PositionButtonsOnObject()
    {
        float distance = endAngle - startAngle;
        float distanceStep = distance / (_buttonList.Count - (distance == 360 ? 0 : 1));
        float currentAngle = startAngle;
        foreach (var item in _buttonList)
        {
            item.transform.parent = this.gameObject.transform;
            item.transform.localPosition = GetLocationFromAngle(currentAngle);
            item.transform.Rotate(new Vector3(0, 0, 360 - currentAngle));
            item.transform.localScale = new Vector3(1, 1, 1);
            currentAngle += distanceStep;
        }
    }

    private Vector3 GetLocationFromAngle(float angle)
    {
        float sphereX = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        float sphereY = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return new Vector3(sphereX, sphereY, 0);
    }

    public void Add(List<ButtonDetails> buttonDetails)
    {
        foreach (var detail in buttonDetails)
        {
            GameObject go = Instantiate(basePrefab) as GameObject;
            go.EnsureComponent<UITexture>().mainTexture = Resources.Load<Texture2D>("Textures/" + detail.textureName);
            _buttonList.Add(go);
        }
        PositionButtonsOnObject();
    }
}

public class ButtonDetails
{
    public string textureName;
    public Action buttonClick;
}
