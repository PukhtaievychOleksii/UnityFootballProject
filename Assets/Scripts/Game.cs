using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //Must be attached to Camera
    public static Dictionary<string, Quaternion> RotetionDirections = new Dictionary<string, Quaternion>();
    public List<FootballPlayer> footballerList = new List<FootballPlayer>();
    public GameObject  ballObject;
    public PlayerController playerController;
    private Spawner spawner = new Spawner();
    // Start is called before the first frame update
    private void Awake()
    {

        RotetionDirections.Add("Up",Quaternion.Euler(0,270,0));
        RotetionDirections.Add("Down",Quaternion.Euler(0, 90, 0));
        RotetionDirections.Add("Left",Quaternion.Euler(0, 180, 0));
        RotetionDirections.Add("Right",Quaternion.Euler(0, 0, 0));
        RotetionDirections.Add("Down-Right", Quaternion.Euler(0, 45, 0));
        RotetionDirections.Add("Down-Left", Quaternion.Euler(0, 135, 0));
        RotetionDirections.Add("Up-Left", Quaternion.Euler(0, 225, 0));
        RotetionDirections.Add("Up-Right", Quaternion.Euler(0, 315, 0));


    }
    void Start()
    {
        spawner = GetComponent<Spawner>();
        spawner.SpawnPlayers();
        playerController = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
