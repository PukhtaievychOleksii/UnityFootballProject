using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public Game game;
    public Spawner spawner;
    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if(ball != null)
        {
            ball.MoveToImmediately(game.BallSpawnPoint.transform.position);
        }
    }
}
