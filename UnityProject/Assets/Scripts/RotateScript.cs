using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RotateScript : MonoBehaviour
{
    private Vector2 _centerPosition;
    private Vector2 _currentPosition;
    private Vector2 _endPosition;
    private float _startAngle;
    private Quaternion _startRotation;
    private bool _beginDrag = true;
    private Dictionary<GameObject, bool> childrenAndEnabledState = new Dictionary<GameObject, bool>();

    public Camera Camera;
    public bool Clamp;
    public bool ClampCrossesZero;
    public float MinClampInDegrees;
    public float MaxClampInDegrees;
    public bool isRotating;
    public event Action BeganRotation;
    public event Action EndedRotation;
    void Start()
    {
        _startRotation = this.transform.rotation;

        if (Camera == null)
            Camera = Camera.main;
        BeganRotation += () =>
        {
            StartDrag();

            childrenAndEnabledState.Clear();
            foreach (GameObject child in gameObject.GetChildren())
            {
                var button = child.GetComponent<UIButton>();
                if (button != null)
                    childrenAndEnabledState.Add(child, button.isEnabled);
            }
        };
        EndedRotation += () =>
        {
            isRotating = false;
            _endPosition = Input.mousePosition;
            foreach (GameObject child in gameObject.GetChildren())
            {
                var button = child.GetComponent<UIButton>();
                if (button != null)
                    button.isEnabled = childrenAndEnabledState[child];
            }
        };
    }

    private void StartDrag()
    {
        _startAngle = GetEulerAngleFromCenter(_centerPosition, Input.mousePosition);
        _startRotation = transform.rotation;
        _endPosition = Vector2.zero;
        _beginDrag = true;
        isRotating = true;
    }

    public void BubbledOnPress()
    {
        this.OnPress();
    }
    public void OnPress()
    {
        _centerPosition = Camera.WorldToScreenPoint(this.transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            if (BeganRotation != null)
                BeganRotation();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (BeganRotation != null)
                EndedRotation();

        }
    }

    public void BubbledOnDrag()
    {
        this.OnDrag();
    }
    public void OnDrag()
    {
        if (_beginDrag)
        {
            foreach (GameObject child in gameObject.GetChildren())
            {
                var button = child.GetComponent<UIButton>();
                if (button != null)
                    button.isEnabled = false;
            }
            _beginDrag = false;
        }

        _currentPosition = Input.mousePosition;

        float currentDragAngle = GetEulerAngleFromCenter(_centerPosition, _currentPosition);

        // currentDragAngle = MouseAngleFrom 0 degrees
        // _startAngle = MouseAngleFrom 0 degrees when you started drag
        // _startRotation.eulerAngles.z = Already applied rotation when you started drag
        // so...
        // the new rotation = existing rotation + the difference betwen start and current
        float rotationAngle = EnsureBetween0And360(currentDragAngle - _startAngle + _startRotation.eulerAngles.z);

        if (Clamp && ShouldClamp(rotationAngle))
        {
            if (distanceToAngle(rotationAngle, MinClampInDegrees) < distanceToAngle(rotationAngle, MaxClampInDegrees))
                rotationAngle = MinClampInDegrees;
            else
                rotationAngle = MaxClampInDegrees;

            StartDrag();
        }

        transform.eulerAngles = new Vector3(0, 0, rotationAngle);
    }

    private float EnsureBetween0And360(float angle)
    {
        if (angle > 360)
            return angle - 360;

        if (angle < 0)
            return angle + 360;

        return angle;
    }

    private bool ShouldClamp(float currentDragAngle)
    {
        if (ClampCrossesZero)
            return currentDragAngle > MinClampInDegrees && currentDragAngle < MaxClampInDegrees;
        else
            return currentDragAngle < MinClampInDegrees || currentDragAngle > MaxClampInDegrees;
    }

    public static float distanceToAngle(float angle1, float angle2)
    {
        float bigAngle = angle1 + 360;
        float littleAngle = angle2 + 360;

        if (angle2 > angle1)
        {
            float temp = bigAngle;
            bigAngle = littleAngle;
            littleAngle = temp;
        }

        float distance = bigAngle - littleAngle;

        if (distance > 180)
            return 360 - distance;
        else
            return distance;
    }

    /// <returns>Degrees</returns>
    public static float GetAngleFromCenter(Vector2 circleCenter, Vector2 point)
    {
        float angleInDegrees = Mathf.Atan2(point.y - circleCenter.y, circleCenter.x - point.x) * Mathf.Rad2Deg;

        angleInDegrees -= 90;

        if (angleInDegrees < 0)
        {
            angleInDegrees = 360 + angleInDegrees;
        }
        return angleInDegrees;
    }

    public static float GetEulerAngleFromCenter(Vector2 circleCenter, Vector2 point)
    {
        return 360 - GetAngleFromCenter(circleCenter, point);
    }
}
