using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ScoreGoalParams
{
    public FootballPlayer ScoredPlayer;
   public  float GoalTime;
   public  FootballTeam  ScoredInGatesTeam;
    public ScoreGoalParams(FootballPlayer scoredPlayer,float goalTime,FootballTeam scoredInGatesTeam)
    {
        ScoredPlayer = scoredPlayer;
        GoalTime = goalTime;
        ScoredInGatesTeam = scoredInGatesTeam;
    }

}
public class Game : MonoBehaviour
{
    //Must be attached to Camera
    public static Dictionary<string, Quaternion> RotationDirections = new Dictionary<string, Quaternion>();
    public Dictionary<string, FootballTeam> Teams = new Dictionary<string, FootballTeam>();
    public List<FootballPlayer> Footballers = new List<FootballPlayer>();
    public List<Controller> Controllers = new List<Controller>();
    public List<ScoreGoalParams> Goals = new List<ScoreGoalParams>();
    public Ball  Ball;
    public GameObject BallPrefab;
    public GameObject PlayerPrefab;
    public GameObject Field;
    public GameObject BallSpawnPoint;
    public Spawner Spawner;
    public static float  StandartYPosition;
    public Gates GatesLeft;//Right or Left From Camera
    public Gates GatesRight;
    public Factory factory;
    public StateMachine StateMachine;
    public TacticsManager TacticsManager;
    public GameObject ControllerCenter;
    //[SerializeField]
    public FootballerConstantData footballerConstantData;
    public UIManager UIManager;
    

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
        TacticsManager = GetComponent<TacticsManager>();
       footballerConstantData = GetComponent<FootballerConstantData>();

        SetObjects();       
        StandartYPosition = Field.transform.position.y;
        StateMachine = new StateMachine(this);
        Ball.OnKeeperChanged += StateMachine.SetStates;

    }

   
    private void SetTeamMembers()
    {
        Spawner.SpawnBall(new SpawnData(BallPrefab, BallSpawnPoint.transform.position));
        LoadFootballer(PlayerPrefab,"Leo",Teams["Liverpool"],"LeftForward",ControllerType.PlayerController,100,100,100);
       LoadFootballer(PlayerPrefab,"CRISH",Teams["Liverpool"], "RightForward",ControllerType.AIController, 100, 100, 100);
        LoadFootballer(PlayerPrefab,"LEWA",Teams["Liverpool"], "RightDefender", ControllerType.AIController, 100, 100, 100);
        LoadFootballer(PlayerPrefab,"VanD", Teams["Liverpool"], "LeftDefender", ControllerType.AIController, 100, 100, 100);
        LoadFootballer(PlayerPrefab,"MUll", Teams["Liverpool"], "GoalKeeper", ControllerType.AIController, 100, 100, 100);
        LoadFootballer(PlayerPrefab,"Neymar", Teams["Chelsea"], "LeftForward", ControllerType.AIController, 80, 80, 50);
        LoadFootballer(PlayerPrefab,"Neur",Teams["Chelsea"], "RightForward", ControllerType.AIController,80,80,50);
        LoadFootballer(PlayerPrefab,"Salah", Teams["Chelsea"], "RightDefender", ControllerType.AIController, 80, 80, 50);
        LoadFootballer(PlayerPrefab,"Modr",Teams["Chelsea"], "LeftDefender", ControllerType.AIController, 80, 80, 50);
        LoadFootballer(PlayerPrefab,"Tsygan",Teams["Chelsea"], "GoalKeeper", ControllerType.AIController, 80, 80, 50);
        
        FootballPlayer checkPlayer = Footballers[0];

    }

    private void LoadFootballer(GameObject playerPrefab,string FootballerName,FootballTeam team,string fieldPositionName,ControllerType controllerType,float defenseSkill,float atackSkill,float moveSkill)
    {
        FieldPosition FieldPosition = team.Tactic.GetFieldPosition(fieldPositionName);
        FootballPlayer footballPlayer = Spawner.SpawnFootballer(playerPrefab,FieldPosition);
        factory.AddFootballer(footballPlayer,new FootballerVariableParams(FootballerName,team,controllerType,FieldPosition,defenseSkill,atackSkill,moveSkill),footballerConstantData,Ball);
    }

    private void SetObjects()
    {
        SetTeams();
        SetTeamMembers();

    }

    private void SetTeams()
    {
        Image image = null;
        TeamParams teamParamsOne = new TeamParams("Liverpool", "LIV", Color.red, image);
        factory.AddTeam(teamParamsOne, TacticsManager.GetTacticObject("1-2-2",FieldSide.Right));
        factory.AddTeam(new TeamParams("Chelsea", "CHL", Color.blue, image), TacticsManager.GetTacticObject("1-2-2", FieldSide.Left));
     
    }

    public void OnGoal()
    {
        Ball.MoveToImmediately(BallSpawnPoint.transform.position);
        PhysicHelper.StopAllPhysicForces(Ball.rigidBody);
        if(Goals.Count > 0) UIManager.LogPanel.AddGoalNotification(Goals[Goals.Count - 1],5);
    }


}
