using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommandArgs : CommandArgs
{
    private Action Action;
    public JumpCommandArgs(Action action,KeyCode callKey)
    {
        Action = action;
        CallKey = callKey;
    }
    public override void OnCallKeyPressedDown()
    {
        base.OnCallKeyPressedDown();
        Action();
    }
}
