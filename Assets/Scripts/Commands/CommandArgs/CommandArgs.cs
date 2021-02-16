using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandArgs 
{
    public KeyCode CallKey;

    public virtual void OnCallKeyPressedDown() { }
    public virtual void OnCallKeyPressedUp() { }
}