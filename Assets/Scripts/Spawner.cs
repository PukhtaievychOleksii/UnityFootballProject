using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> SpawnPointsList;
    public GameObject HeroSpawnPoint;
    private Game game;
    public GameObject playerPrefab;
    private GameObject ballObject;
    
    void Start()
    {
        game = GetComponent<Game>();
        game.playerController = GetComponent<PlayerController>();
        ballObject = game.ballObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FootballPlayer InstantiatePlayer(GameObject prefabFootballer, Vector3 position,Vector3 LookAtPoint)
    {
        GameObject playerObject = Instantiate(prefabFootballer, position, Quaternion.identity);
        playerObject.transform.LookAt(LookAtPoint);
        FootballPlayer footPlayer = prefabFootballer.GetComponent<FootballPlayer>();
        game.footballerList.Add(footPlayer);
        return footPlayer;

    }

    public void SpawnPlayers()
    {
        SpawnControlledPlayer();
        SpawAIPlayers();
    }

    private void SpawnControlledPlayer()
    {
        FootballPlayer footballPlayer = InstantiatePlayer(playerPrefab, HeroSpawnPoint.transform.position,ballObject.transform.position);
        footballPlayer.m_controller = game.playerController;
        game.playerController.m_footballer = footballPlayer;
    }

    private void SpawAIPlayers()
    {
        foreach (GameObject point in SpawnPointsList)
        {
            FootballPlayer footballPlayer = InstantiatePlayer(playerPrefab, point.transform.position,ballObject.transform.position);
            footballPlayer.m_controller = new AIController(footballPlayer);
        }
    }
}
