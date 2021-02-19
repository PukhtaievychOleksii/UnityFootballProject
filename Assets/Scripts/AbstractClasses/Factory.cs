using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory 
{
    Game game;
    public Factory(Game game)
    {
        this.game = game;
    }
    public void AddFootballer(FootballPlayer footballPlayer,FootballerVariableParams footballerVariableParams,FootballerConstantData footballerInitialData,Ball Ball)
    {
        footballPlayer.Ball = Ball;
        footballPlayer.SetInitialParameters(footballerInitialData);
        footballPlayer.SetVariableParams(footballerVariableParams);
        footballPlayer.SetFootballerComponents();
        AddController(ref footballPlayer, footballerVariableParams.ControllerType);
        game.Footballers.Add(footballPlayer);
    }

    public void AddTeam(TeamParams teamParameters,Tactic tactic)
    {
        FootballTeam team = new FootballTeam(teamParameters, tactic);
        SetAppropriateGates(team);
        team.SetOponents(game.Footballers);
        game.Teams.Add(teamParameters.Name, team);
    }

  
    public void AddController(ref FootballPlayer actor, ControllerType controllerType)/* where TController : Controller*/
    {
        
        Controller controller;
        if (controllerType == ControllerType.PlayerController)
        {
            controller = game.ControllerCenter.AddComponent<PlayerController>();
        } else
        {
            controller = game.ControllerCenter.AddComponent<AIController>();
        }
        controller.SetFootballer(actor);
        actor.controller = controller;
    }
    private void SetAppropriateGates(FootballTeam footballTeam)
    {
        if (footballTeam.FieldSide == FieldSide.Left) footballTeam.SetGates(game.GatesLeft, game.GatesRight);
        if (footballTeam.FieldSide == FieldSide.Right) footballTeam.SetGates(game.GatesRight, game.GatesLeft);
    }

}
