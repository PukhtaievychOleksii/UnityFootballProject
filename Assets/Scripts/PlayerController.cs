using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlayerController : Controller
{
    private Game game;
    private List<Command> Commands = new List<Command>();
    void Start()
    {
        if (!IsControlledFootballerFilled()) Debug.LogError("No Footballer In PlayerController");
        game = GetComponent<Game>();
        SetCommands();

    }
    void Update()
    {
        controlledFootballer.MoveComp.SetMovingDataByAxis(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        foreach (Command command in Commands) command.Update();

    }
    private void SetCommands()
    {
        Commands = new List<Command>();
        AddCommand(new ShootCommandArgs(controlledFootballer.AtackComp.Shoot, KeyCode.G));
        AddCommand(new PassCommandArgs(controlledFootballer.AtackComp.PassBall, controlledFootballer.GetTheClosestTeamMate,KeyCode.H));
        AddCommand(new JumpCommandArgs(controlledFootballer.MoveComp.Jump, KeyCode.J));
        AddCommand(new SprintCommandArgs(controlledFootballer.MoveComp.StartRunning, controlledFootballer.MoveComp.FinishRunning,KeyCode.LeftShift));
       
       
    }
    public void AddCommand(CommandArgs commandArgs)
    {
        Command command = new Command(commandArgs);
        Commands.Add(command);
    }
    
    protected override void OnControllersSwaped()
    {
        base.OnControllersSwaped();
        SetCommands();
    }
}

