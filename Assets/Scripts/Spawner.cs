using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnFootballerData
{
    public GameObject Prefab;
    public FieldPosition FieldPosition;
    public SpawnFootballerData(GameObject prefab,FieldPosition fieldPosition)
    {
        Prefab = prefab;
        FieldPosition = fieldPosition;
    }        

}

public struct SpawnData
{
    public GameObject Prefab;
    public Vector3 Position;
    public SpawnData(GameObject prefab, Vector3 position)
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
    
    void Awake()
    {
        game = GetComponent<Game>();
    }

    public void SpawnBall(SpawnData spawnData)
    {
        GameObject ballObject = Instantiate(spawnData.Prefab, spawnData.Position, Quaternion.identity);
        Ball ball = ballObject.AddComponent<Ball>();
        ball.ballObject = ballObject;
        PhysicHelper.StopAllPhysicForces(ball.rigidBody);
        game.Ball = ball;

    }

    public FootballPlayer SpawnFootballer(GameObject playerPrefab,FieldPosition fieldPosition)
    {
        GameObject footballerObject = Instantiate(playerPrefab,fieldPosition.DefensePoint.transform.position,Quaternion.identity);
        FootballPlayer footballPlayer = footballerObject.AddComponent<FootballPlayer>();
        footballPlayer.SetFootballerObject(footballerObject);
        return footballPlayer;
    }

  
}
