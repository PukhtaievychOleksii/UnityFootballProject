using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum States
{
    Atacking,
    Defending
}
public class StateMachine 
{
    private Game game;
    public StateMachine(Game game)
    {
        this.game = game;
    }
    public void SetStates()
    {
        foreach(FootballPlayer footballer in game.footballers)
        {
            UpdateState(footballer);
        }
    }
    private void UpdateState(FootballPlayer footballer)
    {
        if (game.Ball.kepper.Team != footballer.Team) footballer.CurrentState = States.Defending;
        else footballer.CurrentState = States.Atacking;

    }
/*    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
