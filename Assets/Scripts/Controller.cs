using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public FootballPlayer m_footballer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetM_Footballer(FootballPlayer player)
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
