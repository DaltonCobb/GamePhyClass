using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
    public Action[] actions;

    delegate void ActionDelagate();

    ActionDelagate startActionHandler;
    ActionDelagate stopActionHandler;

    private void Start()
    {
        setAction(Action.eActionType.Creator);
    }
    public void StartAction()
    {
        startActionHandler();
    }

    public void StopAction()
    {
        stopActionHandler();
    }

    public void setAction(Action.eActionType actionType)
    {
        startActionHandler = actions[(int)actionType].StartAction;
        stopActionHandler = actions[(int)actionType].StopAction;
    }
}
