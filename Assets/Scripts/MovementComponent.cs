using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DriblingType
{
    SlowKeep,
    FastKeep
};
public class MovementComponent
{
    //constants
    private const float  DistanceForDestinating = (float)1.1;
    public Vector3 WorldVelocity;
    //speed values
    public float speed = 0;
    private float maxSpeed;
    private float minSpeed = 5;
    private float WalkingSpeed;
    private float RunningSpeed;
    private float targetSpeed = 0;
    private Vector3 DestinationPoint;
    private Vector3 NullVector = new Vector3(0, 0, 0);
    private bool GotToDestination = true;
    //rotation values
    private Quaternion rotation = Quaternion.Euler(0,0,0);
    private float angularSpeed;//graduses per second
    private Quaternion targetRotation;
    private int rotationCorrector = -1;
    //dribling values
    public bool IsDribling;
    public DriblingType? driblingType = null;
    //parameter's names
    private string RunningParamName;
    private string JumpingParamName;
    private string WalkingParamName;
    private FootballPlayer owner;//Use values from 1 to 10
    public float MoveSkill = 0;
    //BOOL VALUES
    public bool IsRunning;
    //acceleration values
    private float accelerationValue;
    //Events
    public delegate void DirectionChange();
    public event DirectionChange DirectionChanged;

    
    
    public MovementComponent(FootballPlayer player, string RunningParamName, string JumpingParamName, string WalkingParamName,float MoveSkill)
    {
        owner = player;
        this.RunningParamName = RunningParamName;
        this.JumpingParamName = JumpingParamName;
        this.WalkingParamName = WalkingParamName;
        this.MoveSkill = MoveSkill / 10;

    }

    public void StartMoveComp()
    {
        DestinationPoint = NullVector;
       // m_animator.SetBool(m_RunningParamName, false);
        accelerationValue = MoveSkill / 10;
        maxSpeed = MoveSkill;
        angularSpeed = 360;
        RunningSpeed = MoveSkill;
        WalkingSpeed = RunningSpeed * 3 / 4;
       
        rotation = owner.FootballerObject.transform.rotation;
        WorldVelocity = owner.FootballerObject.transform.forward;
        targetRotation = owner.FootballerObject.transform.rotation;
        DirectionChanged += SetRotationCorrector;
    }

    public void UpdateMove()
    {
        CheckHeroStop();
        CheckWayToDestination();
        GoTo();
    }

    private void CheckWayToDestination()
    {
        if (GotToDestination) return;
        float distance = (DestinationPoint - owner.transform.position).magnitude;
        if (distance <= DistanceForDestinating)
        {
            GotToDestination = true;
            FinishRunning();
        }
    }

    private void GoTo()
    {
        CorrectSpeed();
        RotateFootballer();
       /* if(!ShouldChangeRotation())*/ owner.transform.position += WorldVelocity * speed / 60;//rotate than run

    }


    public Quaternion CalculateRotation(float AxisVert, float AxisHor)
    {

      //  Debug.Log("Vert:" + AxisVert + " Hor:" + AxisHor);
        if (AxisVert < 0 && AxisHor > 0)
        {
           return Game.RotationDirections["Down-Right"];

        }
        if (AxisVert < 0 && AxisHor < 0)
        {
            return Game.RotationDirections["Down-Left"];
        }
        if (AxisVert > 0 && AxisHor > 0)
        {
            return Game.RotationDirections["Up-Right"];
        }
        if (AxisVert > 0 && AxisHor < 0)
        {
            return Game.RotationDirections["Up-Left"];
        }
        if (AxisVert > 0)
        {
            return Game.RotationDirections["Up"];
        }
        if (AxisVert < 0)
        {
            return Game.RotationDirections["Down"];

        }
        if (AxisHor > 0)
        {
            return Game.RotationDirections["Right"];
        }
        if (AxisHor < 0)
        {
           return Game.RotationDirections["Left"];

        }
        return targetRotation;
    }



   
   
    public void CheckHeroStop()
    {
        if (targetSpeed < 0.1 && speed != 0)
        {
           owner.animator.SetBool(WalkingParamName, false);
          //  m_speed = 0;
            targetSpeed = 0;
            
        }
    }


    public void Jump()
    {
       owner.animator.SetTrigger(JumpingParamName);
    }

    public void StopHero()
    {
        targetSpeed = 0;
        owner.animator.SetBool(WalkingParamName, false);
        owner.animator.SetBool(RunningParamName, false);
    }
  private void CorrectSpeed()
    {
        Accelerate();
        if (driblingType != null) speed = speed * 3 / 4;
        if (speed > maxSpeed) speed = maxSpeed;
        if (speed < minSpeed && targetSpeed > 1) speed = minSpeed;
    }

    private void Accelerate()
    {
        if (targetSpeed < 5) speed = targetSpeed;//Momental acceleration on low speeds
        if (targetSpeed > minSpeed && speed < minSpeed) speed = minSpeed;
        int corrector = 1;
        if (speed > targetSpeed) corrector = -1;
       // Debug.Log("MySpeed: " + m_speed);

        if (ShouldChangeSpeed())    speed = speed + accelerationValue * corrector;
        
        
    }

    private void RotateFootballer()
    {
        SetRotationCorrector();


        if (!ShouldChangeRotation()) return;
        
        
        float angleCanPass = angularSpeed / 60;
        float angleToPass = Math.Abs(rotation.eulerAngles.y - targetRotation.eulerAngles.y);
        if(angleToPass >= 180) angleToPass = 360 - angleToPass;
        if (angleCanPass > angleToPass)
        {
            angleCanPass = angleToPass;
        }
        rotation =Quaternion.Euler(0,rotation.eulerAngles.y + angleCanPass * rotationCorrector, 0);
        owner.transform.rotation = rotation;
        WorldVelocity = owner.transform.forward;        

    }
    private void StartWalking()
    {
        targetSpeed = WalkingSpeed;
        owner.animator.SetBool(WalkingParamName, true);
    }
    private void FinishWalking()
    {
        owner.animator.SetBool(WalkingParamName, false);
        targetSpeed = 0;

    }
    public void StartRunning()
    {
        targetSpeed = RunningSpeed;
        owner.animator.SetBool(RunningParamName, true);
        if (driblingType != null) driblingType = DriblingType.FastKeep;
        IsRunning = true;
    }

   

    public void FinishRunning()
    {
        
        owner.animator.SetBool(RunningParamName, false);
        if (driblingType != null) driblingType = DriblingType.SlowKeep;
        IsRunning = false;
        // FinishWalking();
        StartWalking();
    }

  
    private bool ShouldChangeSpeed()
    {
        if (Mathf.Abs(targetSpeed - speed) > 0.1 && targetSpeed > minSpeed) return true;
        return false;
    }
    private bool ShouldChangeRotation()
    {
        float angleBetween = Mathf.Abs(rotation.eulerAngles.y - targetRotation.eulerAngles.y);

        if (angleBetween > owner.ApproximetlyAngleMistakeValue && angleBetween < 360 - owner.ApproximetlyAngleMistakeValue)
        {
            return true;
        }
        return false;
    }

    private void SetRotationTargetByAxis(float AxisVert,float AxisHor)
    {
        targetRotation = CalculateRotation(AxisVert, AxisHor);
    }
    private void SetSpeedByAxis(float AxisVert,float AxisHor)
    {
        if (AxisHor == 0 && AxisVert == 0) StopHero();
        else if (!ShouldChangeRotation() && targetSpeed < 7.5) StartWalking();
    }
    public void SetMovingDataByAxis(float AxisVert,float AxisHor)
    {
        SetRotationTargetByAxis(AxisVert, AxisHor);
        SetSpeedByAxis(AxisVert, AxisHor);
    }

    private void SetRotationCorrector()
    {
        float m_angle = rotation.eulerAngles.y;
        float target_angle = targetRotation.eulerAngles.y;
        float angleToPass = Mathf.Abs(m_angle - target_angle);
   
        if(m_angle > target_angle)
        {
            if (angleToPass < 180) rotationCorrector = -1;
            else rotationCorrector = 1;
        }
        else
        {
            if (angleToPass < 180) rotationCorrector = 1;
            else rotationCorrector = -1;
        }
        
    }
    public void OnDirectionChanged()
    {
        DirectionChanged?.Invoke();
    }
    
    public void SetSkillLevel(float skillLevel)
    {
        MoveSkill = skillLevel;
    }

    public void RunToPoint(Vector3 point)
    {
        if (DestinationPoint == point || !GotToDestination) return;
        LookAtPoint(point);
        StartRunning();
        DestinationPoint = point;
        GotToDestination = false;
    }

    public void LookAtPoint(Vector3 point)
    {
        Vector3 mainPoint = owner.transform.position;
        Vector3 sidePoint = point;
        targetRotation = Quaternion.LookRotation(sidePoint - mainPoint);
        
    }

  

}
