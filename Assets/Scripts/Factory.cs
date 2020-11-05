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
    public FootballPlayer AddFootballer(FootballPlayer footballPlayer,FootballerTextData footballerTextData)
    {

        footballPlayer.SetFootballerComponents();
        footballPlayer.SetParamNames(footballerTextData);
        //SetEnemiesGate(ref footballPlayer);
        return footballPlayer;
    }

    public void AddTeam(TeamParams teamParameters)
    {
       game.Teams.Add(teamParameters.Name, new FootballTeam(teamParameters));
    }

    public void AddController<TController>(FootballPlayer actor,TController ControllerType) where TController : Controller
    {
       
    }

    
}
