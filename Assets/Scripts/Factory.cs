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
    public  void AddFootballer(GameObject footballerPrefab,FieldPosition fieldposition,Controller controller)
    {
        FootballPlayer footballPlayer = footballerPrefab.GetComponent<FootballPlayer>();
        if (footballPlayer == null) Debug.LogError("No footballComponent in Prefab");
        footballPlayer.PlayerFieldPosition = fieldposition;
        SetEnemiesGate(ref footballPlayer);
        controller.SetM_Footballer(footballPlayer);
        game.Controllers.Add(controller);
       /* if(controller is PlayerController)
        {
            footballPlayer.controller = game.playerController;
            game.playerController.m_footballer = footballPlayer;
        }
        else
        {
            //TODO:solve the problem
            footballPlayer.gameObject.AddComponent<AIController>();
            footballPlayer.controller = footballPlayer.gameObject.GetComponent<AIController>();
        }*/
        game.footballers.Add(footballPlayer);
    }

    public void AddTeam(TeamParams teamParameters)
    {
       game.Teams.Add(teamParameters.Name, new FootballTeam(teamParameters));
    }

    private void SetEnemiesGate(ref FootballPlayer player)
    {
        float distanceToLeftGates;
        float distanceToRightGates;
        distanceToLeftGates = (game.GatesLeft.gameObject.transform.position - player.transform.position).magnitude;
        distanceToRightGates = (game.GatesRight.gameObject.transform.position - player.transform.position).magnitude;
        if (distanceToLeftGates > distanceToRightGates) player.EnemiesGates = game.GatesLeft;
        else player.EnemiesGates = game.GatesRight;
        //   Debug.Log("DistanceToLeft: " + distanceToLeftGates + " DistanceToRight: " + distanceToRightGates + " EnemiesGates: " + player.EnemiesGates.name);
        //Debug.Log(player.transform.position);
    }
}
