using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCommandArgs : CommandArgs
{
    private Action<FootballPlayer> Action;
    private Func<FootballPlayer> GetArgumentFunction;
    public PassCommandArgs(Action<FootballPlayer> action,Func<FootballPlayer> getArgumentFunction,KeyCode callKey)
    {
        Action = action;
        GetArgumentFunction = getArgumentFunction;
        CallKey = callKey;
    }

    public override void OnCallKeyPressedDown()
    {
        base.OnCallKeyPressedDown();
        if (Action != null && GetArgumentFunction != null) Action(GetArgumentFunction());
    }
}
