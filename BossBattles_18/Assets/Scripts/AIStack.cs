using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStack:MonoBehaviour {
    Stack<UnitAction> plan = new Stack<UnitAction>();
    public AILogic planner;
    public void UpdatePlan() {
        if (plan.Count == 0) {
            plan.Push(planner.GetAction());
        }
    }
    private void Start() {
        StartCoroutine(Execute());
    }
    public IEnumerator Execute() {
        while (true) {
            UnitAction.activeSource = GetComponent<UnitController>();
            UpdatePlan();
            UnitAction action = plan.Pop();
            Debug.Log(action.GetType());
            if (action != null) {
                if (typeof(ToggleAction).IsAssignableFrom(action.GetType())) {
                    ((ToggleAction)action).RunToggleAction();
                    yield return null;
                } else {
                    UnitAction.activeSource = GetComponent<UnitController>();
                    yield return StartCoroutine(action.RunAction());
                }
            } else {
                yield return null;
            }
        }
    }
}
