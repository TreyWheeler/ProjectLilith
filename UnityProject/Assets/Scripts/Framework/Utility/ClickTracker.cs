using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ClickTracker : MonoBehaviour
{
    private const float EVALUATEFREQUENCY = 5;

    Dictionary<int, ActionTracker> _onClickHandlers = new Dictionary<int, ActionTracker>();
    Dictionary<int, ActionTracker> _onOutsideClickHandlers = new Dictionary<int, ActionTracker>();
    int? _hashCodeOfObjectBeingClicked;
    private float _timeSinceLastEvaluate;

    public void AddOnClickHandler(GameObject gameObject, Action action)
    {
        int hashCode = gameObject.GetHashCode();

        if (!_onClickHandlers.ContainsKey(hashCode))
        {
            _onClickHandlers.Add(hashCode, new ActionTracker(gameObject));
        }

        _onClickHandlers[hashCode].Add(action);
    }

    void Update()
    {
        CheckForClick();

        EvaluateGameObjectExspiration();
    }

    private void CheckForClick()
    {
        foreach (Camera camera in Camera.allCameras)
        {
            if (_hashCodeOfObjectBeingClicked == null && Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    _hashCodeOfObjectBeingClicked = hitObject.GetHashCode();

                    break;
                }
                else
                {
                    // If nothing at all was clicked (Skybox), then go ahead and raise all outside clicks
                    FireAllOutsideClickHandlers();

                    _hashCodeOfObjectBeingClicked = null;
                }
            }
            else if (_hashCodeOfObjectBeingClicked != null && Input.GetMouseButtonUp(0))
            {

                var ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.transform.gameObject;


                    if (hitObject.GetHashCode() == _hashCodeOfObjectBeingClicked)
                    {
                        EvalutateOnOutsideClick(hitObject);

                        int hashCode = hitObject.GetHashCode();

                        if (_onClickHandlers.ContainsKey(hashCode))
                        {
                            _onClickHandlers[hashCode].Execute();
                        }
                    }

                    break;
                }

                _hashCodeOfObjectBeingClicked = null;
            }
        }
    }

    private void EvalutateOnOutsideClick(GameObject gameObjectBeingClicked)
    {
        int hashOfClickedItem = gameObjectBeingClicked.GetHashCode();

        foreach (var kvp in _onOutsideClickHandlers)
        {
            // Create a strong reference to this variable so that it can't be collected after "HasExpired" evalutes
            var gameObject = kvp.Value.GameObject;

            if (!kvp.Value.HasExpired && !HasGameObjectInHeirarchy(gameObject, hashOfClickedItem))
            {
                kvp.Value.Execute();
            }
        }
    }

    private bool HasGameObjectInHeirarchy(GameObject gameObject, int hashCodeToSearchFor)
    {
        if (gameObject.GetHashCode() == hashCodeToSearchFor)
            return true;

        foreach (Transform child in gameObject.transform)
        {
            if (HasGameObjectInHeirarchy(child.gameObject, hashCodeToSearchFor))
                return true;
        }

        return false;
    }

    private void EvaluateGameObjectExspiration()
    {
        _timeSinceLastEvaluate += Time.deltaTime;

        if (_timeSinceLastEvaluate > EVALUATEFREQUENCY)
        {
            _timeSinceLastEvaluate = 0;

            foreach (var kvp in _onClickHandlers.ToArray())
            {
                if (kvp.Value.HasExpired)
                {
                    _onClickHandlers.Remove(kvp.Key);
                }
            }

            foreach (var kvp in _onOutsideClickHandlers.ToArray())
            {
                if (kvp.Value.HasExpired)
                {
                    _onOutsideClickHandlers.Remove(kvp.Key);
                }
            }
        }
    }

    public class ActionTracker
    {// Role: Weakly reference game object to determine if click even should be cleaned up
        List<Action> _actions = new List<Action>();

        WeakReference _gameObjReference;

        public ActionTracker(GameObject obj)
        {
            _gameObjReference = new WeakReference(obj);
        }

        public bool HasExpired
        {
            get
            {
                GameObject typedTarget = _gameObjReference.Target as GameObject;
                return typedTarget == null;
            }
        }

        public GameObject GameObject
        {
            get
            {
                return _gameObjReference.Target as GameObject;
            }
        }

        public void Execute()
        {
            var explicityReference = _gameObjReference.Target; // Keep this in memory for at least this method

            if (HasExpired)
                return;

            foreach (Action action in _actions)
            {
                action();
            }
        }

        public void Add(Action action)
        {
            _actions.Add(action);
        }
    }

    internal void AddOnOutsideClickHandler(GameObject gameObject, Action action)
    {
        int hashCode = gameObject.GetHashCode();

        if (!_onOutsideClickHandlers.ContainsKey(hashCode))
        {
            _onOutsideClickHandlers.Add(hashCode, new ActionTracker(gameObject));
        }

        _onOutsideClickHandlers[hashCode].Add(action);
    }

    private void FireAllOutsideClickHandlers()
    {
        foreach (var kvp in _onOutsideClickHandlers)
        {
            kvp.Value.Execute();
        }
    }
}
