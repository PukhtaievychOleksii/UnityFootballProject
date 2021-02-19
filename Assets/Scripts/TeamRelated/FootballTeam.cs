using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public struct TeamParams
{
    public string Name { get; private set; }
    public string ShortName { get; private set;}
    public Color UniformColor { get; private set; }
    public Image Emblem { get; private set; }
    public TeamParams(string name,string shortName,Color uniformColor,Image emblem)
    {
        Name = name;
        ShortName = shortName;
        UniformColor = uniformColor;
        Emblem = emblem;
    }
   



}
public class FootballTeam 
{
    public List<FootballPlayer> Members = new List<FootballPlayer>();
    public List<FootballPlayer> Oponents;
    public Gates GatesToAtack;
    public Gates GatesToDefend;
    public FieldSide FieldSide;

    public TeamParams TeamParameters;
    public Tactic Tactic;
    
    public FootballTeam(TeamParams teamParameters,Tactic tactic)
    {
        TeamParameters = teamParameters;
        Tactic = tactic;
        FieldSide = tactic.FieldSide;
    }
   public void AddTeamMember(FootballPlayer player)
    {
       
        Members.Add(player);
        RemoveFromOponets(player);
        player.VariableParams.Team = this;
        SetUniformByTeam( player.FootballerObject);
        
    }

    private void SetUniformByTeam( GameObject footballerObject)
    {
        foreach(SkinnedMeshRenderer meshRenderer in footballerObject.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            meshRenderer.material.color = TeamParameters.UniformColor;
        }
      
    }

    public void SetOponents(List<FootballPlayer> AllFootballers)
    {
        Oponents = AllFootballers;
    }

    public void SetGates(Gates gatesToProtect,Gates gatesToAtack)
    {
       GatesToAtack = gatesToAtack;
       GatesToDefend = gatesToProtect;
    }

    private void  RemoveFromOponets(FootballPlayer player)
    {
        if (Oponents == null || Oponents.Count == 0) return;
        if(Oponents.Contains(player)) Oponents.Remove(player);
    }
}
