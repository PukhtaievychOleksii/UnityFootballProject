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
    public FootballPlayer controlledFootballer;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetFootballer(FootballPlayer player)
    {
        controlledFootballer = player;
    }
    protected bool IsControlledFootballerFilled()
    {
        if (controlledFootballer == null) return false;
        if (controlledFootballer.MoveComp == null || controlledFootballer.AtackComp == null || controlledFootballer.DefenseComp == null) return false;
        return true;
    }
    public void SwapConntrollers(FootballPlayer otherPlayer)
    {
        otherPlayer.controller.SetControlledFootballer(controlledFootballer);
        SetControlledFootballer(otherPlayer);
        OnControllersSwaped();
    }

    public void SetControlledFootballer(FootballPlayer footballPlayer)
    {
        controlledFootballer = footballPlayer;
        footballPlayer.controller = this;
    }
    protected virtual void OnControllersSwaped()
    {

    }
}
