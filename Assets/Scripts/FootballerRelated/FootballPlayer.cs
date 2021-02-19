using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Animator))]
public struct FootballerVariableParams {
   public string Name;
   public FootballTeam Team;
   public ControllerType ControllerType;
   public  FieldPosition FieldPosition;
   public float DefenseSkill;
   public float AtackSkill;
   public  float MoveSkill;
    public FootballerVariableParams(string name,FootballTeam team,ControllerType controllerType,FieldPosition fieldPosition,float defenseSkill,float atackSkill,float moveSkill)
    {
        Name = name;
        Team = team;
        FieldPosition = fieldPosition;
        ControllerType = controllerType;
        DefenseSkill = defenseSkill;
        AtackSkill = atackSkill;
        MoveSkill = moveSkill;
    }

}

public class FootballPlayer : MonoBehaviour
{


    public FootballerVariableParams VariableParams;
    public MovementComponent MoveComp;
    public AttackComponent AtackComp;
    public DefenseComponent DefenseComp;
    public GameObject FootballerObject;
    public FootballerConstantData ConstantData;
    public Animator animator;
    public Ball Ball = null;
    public Controller controller;
    public DriblingZone DriblingZone;
    public float ApproximetlyAngleMistakeValue = 2;//use to check whether angels are equal
    public States CurrentState = States.Defending;


  
    
    private  void StartComponents()
    {
  
        MoveComp.StartMoveComp();
        AtackComp.StartAttackComp();
    }

    private void SetSkillComponents(float DefenseSkill,float AtackSkill,float MoveSkill)
    {
        MoveComp = new MovementComponent(this, ConstantData.RunningParamName,ConstantData.JumpingParamName,ConstantData.WalkingParamName,MoveSkill);
        DefenseComp = new DefenseComponent(this,FootballerConstantData.DefaultDefendingRadius,DefenseSkill);
        AtackComp = new AttackComponent(this,AtackSkill);
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
        if (Ball == null || Ball.keeper == null|| Ball.keeper != this) return false;
        return true;
    }
    public void SetInitialParameters(FootballerConstantData footballerConstantData)
    {
        ConstantData = footballerConstantData;

    }
    private void SetDriblingZone()
    {
        DriblingZone = FootballerObject.GetComponent<DriblingZone>();
       DriblingZone.SetOwner(this);
    }

    private void SetAnimator()
    {
        animator = FootballerObject.GetComponent<Animator>();
    }

    public void SetFootballerComponents()
    {
  
        SetDriblingZone();
        SetAnimator();
        StartComponents();
    }


    public void SetFootballerObject(GameObject gameObject)
    {
        FootballerObject = gameObject;
    }

    public bool IsItMineOponent(FootballPlayer footballPlayer)
    {
        if (VariableParams.Team == footballPlayer.VariableParams.Team) return false;
        return true;
    }

    public void SetFieldPosition(FieldPosition fieldPosition)
    {
       VariableParams.FieldPosition = fieldPosition;
    }

    public FootballPlayer GetTheClosestTeamMate()
    {
        FootballPlayer TheClosestTeamMate = new FootballPlayer();
        float distance = 1000;
        foreach (FootballPlayer footballer in VariableParams.Team.Members)
        {
            if (footballer == this) continue;
            float our_distance = (FootballerObject.transform.position - footballer.transform.position).magnitude;

            if (our_distance < distance)
            {
                distance = our_distance;
                TheClosestTeamMate = footballer;
            }
        }
        return TheClosestTeamMate;
    }

  

    public void SetVariableParams(FootballerVariableParams variableParams)
    {
        VariableParams = variableParams;
        variableParams.Team.AddTeamMember(this);
        SetSkillComponents(variableParams.DefenseSkill, variableParams.AtackSkill, variableParams.MoveSkill);
    }


}
