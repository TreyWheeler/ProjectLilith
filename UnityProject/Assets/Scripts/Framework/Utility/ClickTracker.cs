using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ClickTracker : MonoBehaviour
{
    private const float EVALUATEFREQUENCY = 5;

    Dictionary<int, ActionTracker> _clickHandlers = new Dictionary<int, ActionTracker>();
    int? _hashCodeOfObjectBeingClicked;
    private float _timeSinceLastEvaluate;

    public void AddHandler(GameObject gameObject, Action action)
    {
        int hashCode = gameObject.GetHashCode();

        if (!_clickHandlers.ContainsKey(hashCode))
        {
            _clickHandlers.Add(hashCode, new ActionTracker(gameObject));
        }

        _clickHandlers[hashCode].Add(action);
    }

    void Update()
    {
        CheckForClick();

        EvaluateGameObjectExspiration();
    }

    private void CheckForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                _hashCodeOfObjectBeingClicked = hitObject.GetHashCode();
            }
            else
            {
                _hashCodeOfObjectBeingClicked = null;
            }
        }
        else if (_hashCodeOfObjectBeingClicked != null && Input.GetMouseButtonUp(0))
        {
            _hashCodeOfObjectBeingClicked = null;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                int hashCode = hitObject.GetHashCode();

                if (_clickHandlers.ContainsKey(hashCode))
                {
                    _clickHandlers[hashCode].Execute();
                }
            }
        }
    }

    private void EvaluateGameObjectExspiration()
    {
        _timeSinceLastEvaluate += Time.deltaTime;

        if (_timeSinceLastEvaluate > EVALUATEFREQUENCY)
        {
            _timeSinceLastEvaluate = 0;

            foreach (var kvp in _clickHandlers.ToArray())
            {
                if (kvp.Value.HasExpired)
                {
                    _clickHandlers.Remove(kvp.Key);
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
                return _gameObjReference.Target == null;
            }
        }

        public void Execute()
        {
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
}
