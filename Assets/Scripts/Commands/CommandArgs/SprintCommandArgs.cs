using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintCommandArgs : CommandArgs
{
    private Action StartAction;
    private Action FinishAction;
    public SprintCommandArgs(Action startAction,Action finishAction,KeyCode callKey)
    {
        StartAction = startAction;
        FinishAction = finishAction;
        CallKey = callKey;
    }
    public override void OnCallKeyPressedDown()
    {
        base.OnCallKeyPressedDown();
        StartAction();
    }
    public override void OnCallKeyPressedUp()
    {
        base.OnCallKeyPressedUp();
        FinishAction();
    }
}
