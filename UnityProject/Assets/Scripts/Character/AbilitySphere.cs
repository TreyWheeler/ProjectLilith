using UnityEngine;
using System.Collections;

public class AbilitySphere : MonoBehaviour
{
    private bool _isBeingDrugged;
    private bool _mouseWasDown;
    Vector3 originalPosition;
    Vector3 dropLocation;
    float _t;
    float durationOfReturning = .4f;
    bool returning = false;

    // Update is called once per frame
    void Update()
    {
        bool mouseDown = Input.GetMouseButton(0);
        if (_isBeingDrugged)
        {// During Drag
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7);
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

            //this.gameObject.SetActive(false);

            //GameObject go = Helper.GetGameOjectMouseIsOver();

            //if (go != null && go.renderer != null && go.renderer.material != null)
            //    go.renderer.material.color = Color.red;

            //this.gameObject.SetActive(true);
        }

        if (!_mouseWasDown && mouseDown)
        {// Begin Drag
            if (this.gameObject.MouseOnMe())
            {
                returning = false;
                _isBeingDrugged = true;
                if (originalPosition == Vector3.zero)
                    originalPosition = this.gameObject.transform.position;
            }
        }
        else if (_mouseWasDown && !mouseDown && _isBeingDrugged)
        {// End Drag
            _isBeingDrugged = false;

            returning = true;
            dropLocation = gameObject.transform.position;
            _t = 0;
        }

        _mouseWasDown = mouseDown;

        if (returning)
        {
            _t += Time.deltaTime / durationOfReturning;
            this.gameObject.transform.position = Vector3.Lerp(dropLocation, originalPosition, _t);

            if (_t > 1f)
                returning = false;
        }
    }
}

