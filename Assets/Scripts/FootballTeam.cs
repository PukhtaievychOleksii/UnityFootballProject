using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//TODO: struct as arguments 
public struct TeamParams
{
    public string Name { get; private set; }
    public Color UniformColor { get; private set; }
    public Image Emblem { get; private set; }
}
public class FootballTeam : MonoBehaviour
{
    public List<FootballPlayer> TeamMembers = new List<FootballPlayer>();
    public TeamParams TeamParameters;

    
    public FootballTeam(TeamParams teamParameters)
    {
        TeamParameters = teamParameters;
    }
   public void AddTeamMember(FootballPlayer player)
    {
        foreach(FootballPlayer member in TeamMembers)
        {
            if (player = member) return;
        }
        TeamMembers.Add(player);
        player.Team = this;
    }
}
