using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class FootballPlayer : MonoBehaviour
{

    
  
    public MovementComponent MoveComp;
    public AttackComponent AtackComp;
    public DefenseComponent DefenseComp;
    public GameObject PlayerModel;
    public GameObject PlayerObject;
    public string RunningParamName;
    public string JumpingParamName;
    public string WalkingParamName;
    public string ShootingParamName;
    public Animator animator;
    public Ball ball = null;
    public Controller controller;
    public Gates EnemiesGates;
    public DriblingZone driblingZone;
    public FootballPlayer currentOpponent = null;
    public float ApproximetlyMistakeValue = 2;//use to check whether angels are equal
    public FootballTeam Team;
    public FieldPosition FieldPosition;
    public States CurrentState = States.Defending;



    public FootballPlayer()
    {
    }
    void Start()
    {
        SetFootballerComponents();

    }
    private  void StartComponents()
    {
  
        MoveComp.StartMoveComp();
        AtackComp.StartAttackComp();
    }

    private void SetComponents()
    {
        MoveComp = new MovementComponent(this, RunningParamName, JumpingParamName, WalkingParamName, 80);
        DefenseComp = new DefenseComponent(this, 80);
        AtackComp = new AttackComponent(this, 80);
    }

 
    // Update is called once per frame
    void Update()
    {
        // if (m_destination != null)
        MoveComp.UpdateMove();
        DefenseComp.UpdateDefComp();
        AtackComp.UpdateAtackComp();
    }

    public bool IsBallKepper()
    {
        if (ball == null) return false;
        if (ball.kepper != this) return false;
        return true;
    }
    public void SetParamNames(FootballerTextData footballerTextData)
    {
        RunningParamName = footballerTextData.RunningParamName;
        JumpingParamName = footballerTextData.JumpingParamName;
        WalkingParamName = footballerTextData.WalkingParamName;
        ShootingParamName =footballerTextData.ShootingParamName;

    }
    private void SetDriblingZone()
    {
        driblingZone = PlayerObject.GetComponent<DriblingZone>();
       driblingZone.SetOwner(this);
    }

    private void SetAnimator()
    {
        animator = PlayerObject.GetComponent<Animator>();
    }

    public void SetFootballerComponents()
    {
  
        SetDriblingZone();
        SetAnimator();
        SetComponents();
        StartComponents();
    }

    public void SetInitialData(GameObject playerModel,FieldPosition fieldPosition)
    {
        PlayerModel = playerModel;
        FieldPosition = fieldPosition;

    }

    public void SetFootballerObject(GameObject gameObject)
    {
        PlayerObject = gameObject;
    }
    public void SetFielsdPosition(FieldPosition fieldPosition)
    {
        FieldPosition = fieldPosition;
    }

    public bool IsItMineOponent(FootballPlayer footballPlayer)
    {
        if (Team == footballPlayer.Team) return false;
        return true;
    }



}
