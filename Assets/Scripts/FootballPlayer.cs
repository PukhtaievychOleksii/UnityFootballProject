using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class FootballPlayer : MonoBehaviour
{

    
  
    public MovementComponent MoveComp;
    public AttackComponent AttackComp;
    public DefenseComponent DefenseComp;
    public string RunningParamName;
    public string JumpingParamName;
    public string WalkingParamName;
    public Ball m_ball = null;
    public Controller m_controller;





    // Start is called before the first frame update

    void Start()
    {
        MoveComp = new MovementComponent(this, RunningParamName, JumpingParamName,WalkingParamName, GetComponent<Animator>(),80);
        DefenseComp = new DefenseComponent(this, 80);
        AttackComp = new AttackComponent(this, 80);
        MoveComp.StartMoveComp();
        AttackComp.StartAttackComp();
    }

 
    // Update is called once per frame
    void Update()
    {
        // if (m_destination != null)
        MoveComp.UpdateMove();
            
        
    }

    
}
