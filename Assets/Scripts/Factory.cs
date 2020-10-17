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
    public  void AddFootballer(GameObject footballerPrefab,FieldPosition position,FootballTeam team,Controller controller)
    {
        FootballPlayer footballPlayer = footballerPrefab.GetComponent<FootballPlayer>();
        if (footballPlayer == null) Debug.LogError("No footballComponent in Prefab");
        footballPlayer.Team = team;
        footballPlayer.Position = position;
        if(controller is PlayerController)
        {
            footballPlayer.controller = game.playerController;
            game.playerController.m_footballer = footballPlayer;
        }
        else
        {
            footballPlayer.gameObject.AddComponent<AIController>();
            footballPlayer.controller = footballPlayer.gameObject.GetComponent<AIController>();
        }
        game.footballers.Add(footballPlayer);
    }

    public void AddTeam(string Name, Color UniformColor, Image Emblem,List<FootballPlayer> TeamMembers)
    {
        if (TeamMembers != null) game.Teams.Add(Name, new FootballTeam(Name, UniformColor, Emblem, TeamMembers));
        else game.Teams.Add(Name, new FootballTeam(Name, UniformColor, Emblem));
    }
}
