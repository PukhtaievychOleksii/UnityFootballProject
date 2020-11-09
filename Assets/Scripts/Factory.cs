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
    public void AddFootballer(FootballPlayer footballPlayer,FootballerTextData footballerTextData,ControllerType controllerType,FieldPosition fieldPosition)
    {

        footballPlayer.SetFootballerComponents();
        footballPlayer.SetFielsdPosition(fieldPosition);
        SetEnemiesGate(ref footballPlayer);
        footballPlayer.SetParamNames(footballerTextData);
        AddController(ref footballPlayer, controllerType);
        game.footballers.Add(footballPlayer);
    }

    public void AddTeam(TeamParams teamParameters,AtackTactic atackTactic,DefenseTactic defenseTactic)
    {
        FootballTeam team = new FootballTeam(teamParameters, atackTactic, defenseTactic);
        game.Teams.Add(teamParameters.ShortName, team);
    }

    /*  public void AddController<TControllerType>(FootballPlayer actor,TControllerType controllerType) where TController : Controller
      {
       if(controllerType is Controller)
          {

          }  
      }
      */
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
        controller.SetFootballer(ref actor);
    }

    private void SetEnemiesGate(ref FootballPlayer player)
    {
        float distanceToLeftGates;
        float distanceToRightGates;
       // distanceToLeftGates = (game.GatesLeft.gameObject.transform.position - player.FieldPosition.PositionPoint.transform.position).magnitude;
        Vector3 a = game.GatesLeft.gameObject.transform.position;
        Vector3 b = player.FieldPosition.PositionPoint.transform.position;
        distanceToLeftGates = (a - b).magnitude;
        distanceToRightGates = (game.GatesRight.gameObject.transform.position - player.FieldPosition.PositionPoint.transform.position).magnitude;
        if (distanceToLeftGates > distanceToRightGates) player.EnemiesGates = game.GatesLeft;
        else player.EnemiesGates = game.GatesRight;
        //   Debug.Log("DistanceToLeft: " + distanceToLeftGates + " DistanceToRight: " + distanceToRightGates + " EnemiesGates: " + player.EnemiesGates.name);
        //Debug.Log(player.transform.position);
    }


}
