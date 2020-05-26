using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseComponent 
{
    public float m_DefenseSkill;
    public FootballPlayer owner;

    public DefenseComponent(FootballPlayer player, float defenseskill)
    {
        owner = player;
        m_DefenseSkill = defenseskill;
    }
}
