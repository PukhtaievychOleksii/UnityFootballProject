using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum ShootHigh
{
    PoGorobcyam,
    Up,
    Medium,
    Down
};
public class AttackComponent 
{
    private FootballPlayer owner;
    public float AtackSkill;//Use Values from 1 to 100
    private double goalMistake;//number,according to which,player won't shoot in the aim
    private int mistakeAngle = 15;
        // Start is called before the first frame update
    public AttackComponent(FootballPlayer player,int AtackSkill)
    {

        this.AtackSkill = AtackSkill;
        owner = player;
    }
    public void UpdateAtackComp()
    {

    }
    public void StartAttackComp()
    {
        CalculateGoalMistake();
    }
 
    public void PassBall(FootballPlayer receivingPlayer)
    {
        if (owner.ball == null) return;
        
        Vector3 direction = (receivingPlayer.transform.position - owner.driblingZone.GetDriblingPointPosition()).normalized;
        float ForceCoef = PhysicHelper.CalculatePassForce(receivingPlayer.transform.position, owner.transform.position, owner.ball.rigidBody.mass);
            //  PhysicHelper.LookAtByY(owner.gameObject, receivingPlayer.transform.position);
          //  float angle = PhysicHelper.GetAngleBettwenPlayers(owner.gameObject, receivingPlayer.gameObject);
            //owner.MoveComp.SetTargetRotation(angle);
        PhysicHelper.LookAtByY(owner.gameObject, receivingPlayer.gameObject.transform.position);
        owner.ball.HitBall(direction,ForceCoef * 500);
        if(owner.controller is PlayerController)  SwapConntrollers(receivingPlayer);
    }

    public void Shoot(float pressTime)
    {
        if (owner.ball == null) return;
        Vertical_InGates_Position shootHighness =  CalculateShootHigh(pressTime);
        ShootAim shootAim = GetShootAim(shootHighness,owner.transform.rotation);
        Vector3 shootDirection = shootAim.AimObject.transform.position - owner.ball.transform.position;
        UseGoalMistake(ref shootDirection);
        // owner.m_animator.SetTrigger(owner.ShootingParamName);
        owner.ball.HitBall(shootDirection,150);
        
    }
  
    private Vertical_InGates_Position CalculateShootHigh(float pressTime)
    {
      //  Debug.Log(pressTime);

        if (pressTime > 2) return Vertical_InGates_Position.PoGorobcyam;
        if (pressTime > 1) return Vertical_InGates_Position.Up;
        if (pressTime > 0.5) return Vertical_InGates_Position.Medium;
        return Vertical_InGates_Position.Down;
           
    }
   
    private ShootAim GetShootAim(Vertical_InGates_Position verticalAimPosition, Quaternion targetRotation)
    {
        ShootAim shootAim;
        Horizontal_InGates_Position horizontalAimPosition = Horizontal_InGates_Position.Center;
        Vector3 quaternionEulerAngles = targetRotation.eulerAngles;
        float rotationAngle = quaternionEulerAngles.y;

        if (owner.EnemiesGates.name == "GatesLeft")
        {
            if (Math.Abs(rotationAngle - Game.RotationDirections["Up"].eulerAngles.y) < owner.ApproximetlyMistakeValue || Math.Abs(rotationAngle - Game.RotationDirections["Up-Left"].eulerAngles.y) < owner.ApproximetlyMistakeValue) horizontalAimPosition = Horizontal_InGates_Position.Right;
            if (Math.Abs(rotationAngle - Game.RotationDirections["Left"].eulerAngles.y) < owner.ApproximetlyMistakeValue) horizontalAimPosition = Horizontal_InGates_Position.Center;
            if (Math.Abs(rotationAngle - Game.RotationDirections["Down-Left"].eulerAngles.y) < owner.ApproximetlyMistakeValue || Math.Abs(rotationAngle - Game.RotationDirections["Down"].eulerAngles.y) < owner.ApproximetlyMistakeValue)
            {
                horizontalAimPosition = Horizontal_InGates_Position.Left;
            }
        }
        if(owner.EnemiesGates.name == "GatesRight")
        {
            if (Math.Abs(rotationAngle - Game.RotationDirections["Up"].eulerAngles.y) < owner.ApproximetlyMistakeValue || Math.Abs(rotationAngle - Game.RotationDirections["Up-Right"].eulerAngles.y) < owner.ApproximetlyMistakeValue) horizontalAimPosition = Horizontal_InGates_Position.Left;
            if (Math.Abs(rotationAngle - Game.RotationDirections["Right"].eulerAngles.y) < owner.ApproximetlyMistakeValue) horizontalAimPosition = Horizontal_InGates_Position.Center;
            if (Math.Abs(rotationAngle - Game.RotationDirections["Down-Right"].eulerAngles.y) < owner.ApproximetlyMistakeValue || Math.Abs(rotationAngle - Game.RotationDirections["Down"].eulerAngles.y) < owner.ApproximetlyMistakeValue) horizontalAimPosition = Horizontal_InGates_Position.Right;
        }
        shootAim = owner.EnemiesGates.GetApropriateShootAim(verticalAimPosition, horizontalAimPosition);
        float distanceToAim = (shootAim.AimObject.transform.position - owner.ball.transform.position).magnitude;
       // if (distanceToAim > 15) shootAim = owner.EnemiesGates.GetApropriateShootAim(Vertical_InGates_Position.PoGorobcyam, Horizontal_InGates_Position.Center);
        return shootAim;
    }

    public void SetSkillLevel(float skillLevel)
    {
        if (skillLevel > 100) skillLevel = 100;
        if(skillLevel < 50) skillLevel = 50;
        AtackSkill = skillLevel;
        CalculateGoalMistake();
    }
    private void UseGoalMistake(ref Vector3 shootDirection)
    {
        Quaternion mistakeQuaternion;
        System.Random random = new System.Random();
        float ZAngle = random.Next(0,360);
        float YAngle = 0;
        float XAngle = (float)goalMistake * mistakeAngle;
        mistakeQuaternion = Quaternion.Euler(XAngle, YAngle, ZAngle);
        Debug.Log(AtackSkill);
        Debug.Log(goalMistake);
        Debug.Log(XAngle);
        /*shootDirection.x *= mistakeQuaternion.x;
        shootDirection.y *= mistakeQuaternion.y;
        shootDirection.z *= mistakeQuaternion.z;*/
        shootDirection = mistakeQuaternion * shootDirection;
     }

    private void CalculateGoalMistake()
    {
        goalMistake = 1 - (AtackSkill - 50) * 2 / 100;

    }

    private void SwapConntrollers(FootballPlayer secondPlayer)
    {
        PlayerController initialPlayerController = (PlayerController)owner.controller;
        initialPlayerController.m_footballer = secondPlayer;
        secondPlayer.controller = initialPlayerController;
        initialPlayerController.ResetKeyManagersForNewFootballer();

        owner.MoveComp.StopHero();
        owner.controller = new AIController(owner);
       
    }
}
