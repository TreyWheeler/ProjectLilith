using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PositionButtons : MonoBehaviour
{
    private List<GameObject> _gameObjects = new List<GameObject>();

    public GameObject basePrefab;
    public float radius = 100;
    public float startAngle = 0;
    public float endAngle = 360;

    public float FixedStep = 0;

    public void Layout()
    {
        if (_gameObjects.Count == 1)
            PositionOneButton(_gameObjects[0]);
        else
            PositionMultipleButtons();
    }
    private void PositionOneButton(GameObject go)
    {
        float distance = RotateScript.distanceToAngle(startAngle, endAngle) / 2;// (endAngle - startAngle) / 2;
        float currentAngle = startAngle + distance;
        go.transform.parent = this.gameObject.transform;
        go.transform.localPosition = GetLocationFromAngle(currentAngle);
        go.transform.localRotation = Quaternion.identity;
        go.transform.Rotate(new Vector3(0, 0, 360 - currentAngle));
        go.transform.localScale = new Vector3(1, 1, 1);
    }
    private void PositionMultipleButtons()
    {
        float distanceStep = FixedStep;
        if (distanceStep == 0)
        {
            float distance = RotateScript.distanceToAngle(startAngle, endAngle);
            distanceStep = distance / (_gameObjects.Count - (distance == 360 ? 0 : 1));
        }
        float currentAngle = startAngle;
        foreach (var item in _gameObjects)
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

    public GameObject Add(string textureName, Func<bool> isEnabled = null, Action onClick = null)
    {
        GameObject go = new GameObject();
        UITexture buttonTexture = go.EnsureComponent<UITexture>();

        if (onClick != null)
        {
            DaemonButton button = go.EnsureComponent<DaemonButton>();
            UIButton buttonUI = go.EnsureComponent<UIButton>();          
            BoxCollider buttonBoxCollider = go.EnsureComponent<BoxCollider>();
            if (isEnabled != null)
            {
                go.EnsureComponent<IsEnabled>().Predicate = isEnabled;
                bool enabled = isEnabled();
                buttonUI.isEnabled = enabled;
                buttonBoxCollider.enabled = enabled;
            }
            buttonBoxCollider.isTrigger = true;
            buttonTexture.ResizeCollider();


            button.click += onClick;
        }

        buttonTexture.mainTexture = Resources.Load<Texture2D>("Textures/" + textureName);
        buttonTexture.autoResizeBoxCollider = true;
        buttonTexture.depth = 1;
        _gameObjects.Add(go);
        return go;
    }

    public void Clear()
    {
        foreach (GameObject go in _gameObjects)
        {
            Destroy(go);
        }
        _gameObjects.Clear();
    }
}

