using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnData
{
    public GameObject Prefab;
    public Vector3 Position;
    public SpawnData(GameObject prefab,Vector3 position)
    {
        Prefab = prefab;
        Position = position;
    }        

}

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

    

    private void SpawnControlledPlayer()
    {
        GameObject footballObject = GetAndInstantiatePlayer(game.PlayerPrefab, HeroSpawnPoint.transform.position,ball.gameObject.transform.position);
   
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
    private void SpawnPlayers()
    {
        foreach(FootballPlayer footballer in game.footballers)
        {
            footballer.PlayerObject =  Instantiate(footballer.PlayerModel, footballer.FieldPosition.PositionPoint.transform.position, Quaternion.identity);
        }
    }

  

    public void SpawnBall(SpawnData spawnData)
    {
        GameObject ballObject = Instantiate(spawnData.Prefab, spawnData.Position, Quaternion.identity);
        ballObject.AddComponent<Ball>();
        Ball ball = ballObject.GetComponent<Ball>();
        PhysicHelper.StopAllPhysicForces(ball.rigidBody);
        game.Ball = ball;

    }

    public void SpawnFootballer(SpawnData spawnData)
    {
        GameObject footballerObject = Instantiate(spawnData.Prefab, spawnData.Position,Quaternion.identity);
        footballerObject.AddComponent<FootballPlayer>();
        FootballPlayer footballPlayer = footballerObject.GetComponent<FootballPlayer>();
        footballPlayer.SetFootballerObject(footballerObject);
        SetEnemiesGate(ref footballPlayer);
        footballPlayer.SetParamNames(game.footballerTextData);
        game.footballers.Add(footballPlayer);
    }

    private void SetEnemiesGate(ref FootballPlayer player)
    {
        float distanceToLeftGates;
        float distanceToRightGates;
        distanceToLeftGates = (game.GatesLeft.gameObject.transform.position - player.FieldPosition.PositionPoint.transform.position).magnitude;
        distanceToRightGates = (game.GatesRight.gameObject.transform.position - player.FieldPosition.PositionPoint.transform.position).magnitude;
        if (distanceToLeftGates > distanceToRightGates) player.EnemiesGates = game.GatesLeft;
        else player.EnemiesGates = game.GatesRight;
        //   Debug.Log("DistanceToLeft: " + distanceToLeftGates + " DistanceToRight: " + distanceToRightGates + " EnemiesGates: " + player.EnemiesGates.name);
        //Debug.Log(player.transform.position);
    }
}
