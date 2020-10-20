using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject HeroSpawnPoint;
    public GameObject BallSpawnPoint;
    private Game game;
    private Ball ball;
    
    void Awake()
    {
        //TODO :  make a prefab with spawner and instantiate it in Game
        game = GetComponent<Game>();
        game.playerController = GetComponent<PlayerController>();
        ball = game.ball;
       // SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetAndInstantiatePlayer(GameObject prefabFootballer, Vector3 position,Vector3 LookAtPoint)
    {
        GameObject playerObject = Instantiate(prefabFootballer, position, Quaternion.identity);
        
        //playerObject.transform.LookAt(LookAtPoint);
        PhysicHelper.LookAtByY(playerObject, LookAtPoint);
        FootballPlayer footPlayer = playerObject.GetComponent<FootballPlayer>();
        game.footballers.Add(footPlayer);
        return playerObject;

    }

    public void SpawnObjects()
    {
        // SpawnControlledPlayer();
        SpawnBall();
        SpawnAllPlayers();
    }

    private void SpawnControlledPlayer()
    {
        GameObject footballObject = GetAndInstantiatePlayer(game.playerPrefab, HeroSpawnPoint.transform.position,ball.gameObject.transform.position);
   
      /*  FootballPlayer footballPlayer = footballObject.GetComponent<FootballPlayer>();
        footballPlayer.controller = game.playerController;
        footballPlayer.AtackComp.SetSkillLevel(45);
        game.playerController.m_footballer = footballPlayer;*/
       //game.playerController.m_footballer.DefenseComp.SetSkillLevel(30);


    }

    private void SpawnAllPlayers()
    {
     /*   foreach (GameObject point in SpawnPoints)
        {
            if (point == HeroSpawnPoint)
            {
                SpawnControlledPlayer();
                continue;
            }
            GameObject FootballObject = GetAndInstantiatePlayer(game.playerPrefab, point.transform.position, ball.gameObject.transform.position);
            FootballPlayer footballPlayer = FootballObject.GetComponent<FootballPlayer>();
            footballPlayer.AtackComp.SetSkillLevel(30);
            footballPlayer.gameObject.AddComponent<AIController>();
            footballPlayer.controller = footballPlayer.gameObject.GetComponent<AIController>();


        }*/
    }

  

    public void SpawnBall()
    {
        Instantiate(ball.gameObject, BallSpawnPoint.transform.position, Quaternion.identity);
        PhysicHelper.StopAllPhysicForces(ball.rigidBody/*ball.gameObject.GetComponent<Rigidbody>()*/);
    }
}
