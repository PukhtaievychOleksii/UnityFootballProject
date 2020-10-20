using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class FootballPlayer : MonoBehaviour
{

    
  
    public MovementComponent MoveComp;
    public AttackComponent AtackComp;
    public DefenseComponent DefenseComp;
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
    public FieldPosition PlayerFieldPosition;
    public States CurrentState = States.Defending;



    // Start is called before the first frame update

    void Awake()
    {
        MoveComp = new MovementComponent(this, RunningParamName, JumpingParamName,WalkingParamName,80);
        animator = GetComponent<Animator>();
        DefenseComp = new DefenseComponent(this, 80);
        AtackComp = new AttackComponent(this, 80);
        MoveComp.StartMoveComp();
        AtackComp.StartAttackComp();
        driblingZone = GetComponentInChildren<DriblingZone>();
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
}
