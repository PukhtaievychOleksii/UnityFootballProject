using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//TODO: struct as arguments 
public class FootballTeam : MonoBehaviour
{
    public List<FootballPlayer> TeamMembers = new List<FootballPlayer>();
    public string Name;
    public Color UniformColor;
    public  Image Emblem;

    public FootballTeam(string Name,Color UniformColor,Image Emblem, List<FootballPlayer> TeamMembers) 
        : this (Name,UniformColor,Emblem)
    {

        this.TeamMembers = new List<FootballPlayer>(TeamMembers);//!!!Mistake
        SetTeamsForFootballers();
    }
    public FootballTeam(string Name,Color UniformColor,Image Emblem)
    {
        this.Name = Name;
        this.UniformColor = UniformColor;
        this.Emblem = Emblem;
    }
   private void SetTeamsForFootballers()
    {
        foreach(FootballPlayer footballer in TeamMembers)
        {
            footballer.Team = this;
        }
    }
}
