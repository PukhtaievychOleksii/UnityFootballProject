using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType
{
    AIController,
    PlayerController
}

public class Controller : MonoBehaviour
{
    public FootballPlayer m_footballer;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetFootballer(ref FootballPlayer player)
    {
        m_footballer = player;
        player.controller = this;
    }
    protected bool IsM_FootballerFilled()
    {
        if (m_footballer == null) return false;
        if (m_footballer.MoveComp == null || m_footballer.AtackComp == null || m_footballer.DefenseComp == null) return false;
        return true;
    }
}
