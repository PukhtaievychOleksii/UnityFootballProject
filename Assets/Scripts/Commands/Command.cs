using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Command
{
    private CommandArgs CommandArgs = new CommandArgs();
    
    public Command(CommandArgs commandArgs)
    {
        CommandArgs = commandArgs;
    }
  

    public void Update()
    {
        if (Input.GetKeyDown(CommandArgs.CallKey))
        {
            CommandArgs.OnCallKeyPressedDown();
        }
        if (Input.GetKeyUp(CommandArgs.CallKey))
        {
            CommandArgs.OnCallKeyPressedUp();
        }
    }


}

