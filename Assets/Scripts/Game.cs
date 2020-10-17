using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldPosition
{
    LD1,  LD2,
    RD1,  RD2,
    LF1,  LF2,
    RF1,  RF2,
    GK1,  GK2
}

public enum Team
{
    Chelsea,
    Liverpool,
    Dynamo,
    Borrussia_Dortmund
}
public class Game : MonoBehaviour
{
    //Must be attached to Camera
    public static Dictionary<string, Quaternion> RotationDirections = new Dictionary<string, Quaternion>();
    public Dictionary<string, FootballTeam> Teams = new Dictionary<string, FootballTeam>();
    public List<FootballPlayer> footballers = new List<FootballPlayer>();
    public Ball  ball;
    public GameObject playerPrefab;
    public GameObject BallSpawnPoint;
    public PlayerController playerController;
    public GameObject Field;
    public static float  StandartYPosition;
    public Gates GatesLeft;//Right or Left From Camera
    public Gates GatesRight;
    public Factory factory;
    public StateMachine StateMachine;

    // Start is called before the first frame upda
    private void Awake()
    {

        RotationDirections.Add("Up",Quaternion.Euler(0,270,0));
        RotationDirections.Add("Down",Quaternion.Euler(0, 90, 0));
        RotationDirections.Add("Left",Quaternion.Euler(0, 180, 0));
        RotationDirections.Add("Right",Quaternion.Euler(0, 0, 0));
        RotationDirections.Add("Down-Right", Quaternion.Euler(0, 45, 0));
        RotationDirections.Add("Down-Left", Quaternion.Euler(0, 135, 0));
        RotationDirections.Add("Up-Left", Quaternion.Euler(0, 225, 0));
        RotationDirections.Add("Up-Right", Quaternion.Euler(0, 315, 0));
        factory = new Factory(this);

    }
    void Start()
    {
     //   spawner = GetComponent<Spawner>();
       // spawner.SpawnPlayers();
        playerController = GetComponent<PlayerController>();
        StandartYPosition = Field.transform.position.y;
      //  factory.AddFootballer(playerPrefab, FieldPosition.LF1, Team.Liverpool,playerController);
        StateMachine = new StateMachine(this);
        ball.OnKeeperChanged += StateMachine.SetStates;
    }

    // Update is called once per frame
    void Update()
    {
       // StateMachine.SetStates(); 
    }

    

   
}
