using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommandArgs : CommandArgs
{
    private Action<float> Action;
    TimeCounter counter = new TimeCounter();
    
    public ShootCommandArgs(Action<float> action,KeyCode callKey)
    {
        Action = action;
        CallKey = callKey;
       
        
    }
    public override void OnCallKeyPressedDown()
    {
        counter.SetToZeroCounter();
    }
    public override void OnCallKeyPressedUp()
    {
        if (Action != null) Action(counter.GetPassedTime());
    }


}
