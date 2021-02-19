using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public Game game;
    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponentInParent<Ball>();
        if(ball != null)
        {
            //  ball.MoveToImmediately(game.BallSpawnPoint.transform.position);
            game.OnGoal();
        }
    }
}
