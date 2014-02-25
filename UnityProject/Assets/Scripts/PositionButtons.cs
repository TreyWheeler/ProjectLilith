using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PositionButtons : MonoBehaviour
{
    private List<DaemonButton> _buttonList = new List<DaemonButton>();

    public GameObject basePrefab;
    public float radius = 100;
    public float startAngle = 0;
    public float endAngle = 360;
    private void PositionButtonsOnObject()
    {
        if (_buttonList.Count == 1)
            PositionOneButton(_buttonList[0]);
        else
            PositionMultipleButtons();
    }
    private void PositionOneButton(DaemonButton button)
    {
        float distance = (endAngle - startAngle) / 2;
        float currentAngle = startAngle + distance;
        button.transform.parent = this.gameObject.transform;
        button.transform.localPosition = GetLocationFromAngle(currentAngle);
        button.transform.localRotation = Quaternion.identity;
        button.transform.Rotate(new Vector3(0, 0, 360 - currentAngle));
        button.transform.localScale = new Vector3(1, 1, 1);
    }
    private void PositionMultipleButtons()
    {
        float distance = endAngle - startAngle;
        float distanceStep = distance / (_buttonList.Count - (distance == 360 ? 0 : 1));
        float currentAngle = startAngle;
        foreach (var item in _buttonList)
        {
            item.transform.parent = this.gameObject.transform;
            item.transform.localPosition = GetLocationFromAngle(currentAngle);
            item.transform.localRotation = Quaternion.identity;
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
        foreach (ButtonDetails detail in buttonDetails)
        {
            GameObject go = Instantiate(basePrefab) as GameObject;
            DaemonButton button = go.EnsureComponent<DaemonButton>();
            button.EnsureComponent<UITexture>().mainTexture = Resources.Load<Texture2D>("Textures/" + detail.textureName);
            if (!detail.isEnabled)
            {
                go.EnsureComponent<UIButton>().isEnabled = false;
                go.EnsureComponent<BoxCollider>().enabled = false;
            }
            button.click += detail.buttonClick;
            _buttonList.Add(button);
        }
        PositionButtonsOnObject();
    }

    public void ClearAndAdd(List<ButtonDetails> buttonDetails)
    {
        foreach (DaemonButton button in _buttonList)
        {
            Destroy(button.gameObject);
        }
        _buttonList.Clear();
        Add(buttonDetails);
    }
}

public class ButtonDetails
{
    public string textureName;
    public Action buttonClick;
    public bool isEnabled = true;
}
