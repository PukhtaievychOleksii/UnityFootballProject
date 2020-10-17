using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public GameObject HeroSpawnPoint;
    private Game game;
    private Ball ball;
    
    void Start()
    {
        game = GetComponent<Game>();
        game.playerController = GetComponent<PlayerController>();
        ball = game.ball;
        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetInstantiatedPlayer(GameObject prefabFootballer, Vector3 position,Vector3 LookAtPoint)
    {
        GameObject playerObject = Instantiate(prefabFootballer, position, Quaternion.identity);
        
        //playerObject.transform.LookAt(LookAtPoint);
        PhysicHelper.LookAtByY(playerObject, LookAtPoint);
        FootballPlayer footPlayer = playerObject.GetComponent<FootballPlayer>();
        SetEnemiesGate(ref footPlayer);
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
        GameObject footballObject = GetInstantiatedPlayer(game.playerPrefab, HeroSpawnPoint.transform.position,ball.gameObject.transform.position);
   
        FootballPlayer footballPlayer = footballObject.GetComponent<FootballPlayer>();
        footballPlayer.controller = game.playerController;
        footballPlayer.AtackComp.SetSkillLevel(45); 
        game.playerController.m_footballer = footballPlayer;
       //game.playerController.m_footballer.DefenseComp.SetSkillLevel(30);


    }

    private void SpawnAllPlayers()
    {
        foreach (GameObject point in SpawnPoints)
        {
            if (point == HeroSpawnPoint)
            {
                SpawnControlledPlayer();
                continue;
            }
            GameObject FootballObject = GetInstantiatedPlayer(game.playerPrefab, point.transform.position, ball.gameObject.transform.position);
            FootballPlayer footballPlayer = FootballObject.GetComponent<FootballPlayer>();
            footballPlayer.AtackComp.SetSkillLevel(30);
            footballPlayer.gameObject.AddComponent<AIController>();
            footballPlayer.controller = footballPlayer.gameObject.GetComponent<AIController>();


        }
    }

  private void SetEnemiesGate(ref FootballPlayer player)
    {
        float distanceToLeftGates;
        float distanceToRightGates;
        distanceToLeftGates = (game.GatesLeft.gameObject.transform.position - player.transform.position).magnitude;
        distanceToRightGates = (game.GatesRight.gameObject.transform.position - player.transform.position).magnitude;
        if (distanceToLeftGates > distanceToRightGates) player.EnemiesGates = game.GatesLeft;
        else player.EnemiesGates = game.GatesRight;
     //   Debug.Log("DistanceToLeft: " + distanceToLeftGates + " DistanceToRight: " + distanceToRightGates + " EnemiesGates: " + player.EnemiesGates.name);
        //Debug.Log(player.transform.position);
    }

    public void SpawnBall()
    {
        ball.transform.position = game.BallSpawnPoint.transform.position;
        PhysicHelper.StopAllPhysicForces(ball.rigidBody);
    }
}
