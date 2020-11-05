using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Game : MonoBehaviour
{
    //Must be attached to Camera
    public static Dictionary<string, Quaternion> RotationDirections = new Dictionary<string, Quaternion>();
    public Dictionary<string, FootballTeam> Teams = new Dictionary<string, FootballTeam>();
    public List<FootballPlayer> footballers = new List<FootballPlayer>();
    public List<Controller> Controllers = new List<Controller>();
    public Ball  Ball;
    public GameObject BallPrefab;
    public GameObject PlayerPrefab;
    public PlayerController playerController;
    public GameObject Field;
    public Spawner Spawner;
    public static float  StandartYPosition;
    public Gates GatesLeft;//Right or Left From Camera
    public Gates GatesRight;
    public Factory factory;
    public StateMachine StateMachine;
    public Tactics tactics;
    [SerializeField]
    public FootballerTextData footballerTextData;
    public GameObject ControllerCenter;

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
        this.gameObject.AddComponent<Spawner>();
        Spawner = this.gameObject.GetComponent<Spawner>();
        tactics = GetComponent<Tactics>();
     //  footballerTextData = GetComponent<FootballerTextData>();

        SpawnObjects();
       
        playerController = GetComponent<PlayerController>();
        StandartYPosition = Field.transform.position.y;
        StateMachine = new StateMachine(this);
        Ball.OnKeeperChanged += StateMachine.SetStates;

    }

    // Update is called once per frame
    void Update()
    {
       // StateMachine.SetStates(); 
    }
    private void SpawnObjects()
    {
        Spawner.SpawnBall(new SpawnData(BallPrefab, tactics.BallSpawnPoint.transform.position));
        Spawner.SpawnFootballer(new SpawnData(PlayerPrefab,tactics.GetAppropriateFieldPostion(FieldPositionShortName.LF1).PositionPoint.transform.position));  
    }

   



}
