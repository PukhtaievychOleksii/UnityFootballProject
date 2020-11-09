using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//TODO: struct as arguments 
public struct TeamParams
{
    public string Name { get; private set; }
    public string ShortName { get; private set;}
    public Color UniformColor { get; private set; }
    public Image Emblem { get; private set; }

    
}
public class FootballTeam 
{
    //TODO : ask whether it is better to write ' team.atackComponent = ...' or 'team.SetAtckComponent()'
    public List<FootballPlayer> TeamMembers = new List<FootballPlayer>();
    public TeamParams TeamParameters;
    public AtackTactic AtackTactic;
    public DefenseTactic DefenseTactic;
    
    public FootballTeam(TeamParams teamParameters,AtackTactic atackTactic,DefenseTactic defenseTactic)
    {
        TeamParameters = teamParameters;
        AtackTactic = atackTactic;
        DefenseTactic = defenseTactic;
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
