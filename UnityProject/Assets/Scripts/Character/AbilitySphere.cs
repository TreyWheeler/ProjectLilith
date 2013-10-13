using UnityEngine;
using System.Collections;

public class AbilitySphere : MonoBehaviour
{
    private bool _isBeingDrugged;

    private bool _mouseWasDown;
    Vector3 originalPosition;
    // Update is called once per frame
    void Update()
    {
        bool mouseDown = Input.GetMouseButton(0);
        if (_isBeingDrugged)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7);
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (!_mouseWasDown && mouseDown)
        {
            if (this.gameObject.MouseOnMe())
            {
                _isBeingDrugged = true;
                originalPosition = this.gameObject.transform.position;
            }
        }
        else if (_mouseWasDown && !mouseDown)
        {
            _isBeingDrugged = false;
        }

        _mouseWasDown = mouseDown;
    }


}

