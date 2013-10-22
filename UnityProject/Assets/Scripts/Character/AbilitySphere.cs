﻿using UnityEngine;
using System.Collections;
using System;

public class AbilitySphere : MonoBehaviour
{
    private bool _isBeingDrugged;
    private bool _mouseWasDown;
    Vector3 _originalPosition;
    Vector3 _dropLocation;
    float _t;
    float _durationOfReturning = 2f;
    bool _returning = false;
    int abilityNumber;

    public GameObject OriginatingGameObject { get; set; }

    // Update is called once per frame
    void Update()
    {
        bool mouseDown = Input.GetMouseButton(0);
        if (_isBeingDrugged)
        {// During Drag
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7);
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

            if (this.gameObject.transform.position.y < 0)
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z);



        }

        if (!_mouseWasDown && mouseDown)
        {// Begin Drag
            if (this.gameObject.MouseOnMe())
            {
                _returning = false;
                _isBeingDrugged = true;
                if (_originalPosition == Vector3.zero)
                    _originalPosition = this.gameObject.transform.position;
            }
        }
        else if (_mouseWasDown && !mouseDown && _isBeingDrugged)
        {// End Drag
            _isBeingDrugged = false;

            this.gameObject.SetActive(false);
            GameObject go = Helper.GetGameOjectMouseIsOver();
            if (go != null)
            {
                Character characterDroppedOn = go.GetComponent<Character>();
                if (characterDroppedOn != null)
                {
                    Character thisCharacter = this.OriginatingGameObject.GetComponent<Character>();
                    if (thisCharacter != null)
                    {
                        thisCharacter.AbilityQue.Enqueue(new IntendedAction(new Ability(), go));
                    }
                }
            }

            this.gameObject.SetActive(true);
            _returning = true;
            _dropLocation = gameObject.transform.position;
            _t = 0;
        }

        _mouseWasDown = mouseDown;

        if (_returning)
        {
            _t += Time.deltaTime / _durationOfReturning;
            this.gameObject.transform.position = Vector3.Lerp(_dropLocation, _originalPosition, _t);

            if (_t > 1f)
                _returning = false;
        }
    }

    public void Run()
    {
        this.OriginatingGameObject.animation.CrossFade("Attack");
    }
}

