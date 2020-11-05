using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DriblingType
{
    SlowKeep,
    FastHits
};
public class MovementComponent
{
    public Vector3 WorldVelocity;
    //speed values
    public float speed = 0;
    private float maxSpeed;
    private float minSpeed = 5;
    private float WalkingSpeed;
    private float RunningSpeed;
    private float targetSpeed = 0;
    //rotation values
    private Quaternion rotation;
    private float angularSpeed;//graduses per second
    private Quaternion targetRotation;
    private Vector3? destination;
    private int rotationCorrector = -1;
    //dribling values
    public bool IsDribling;
    public DriblingType? driblingType = null;
    //parameter's names
    private string RunningParamName;
    private string JumpingParamName;
    private string WalkingParamName;
    private FootballPlayer footballPlayer;//Use values from 1 to 10
    public float MoveSkill = 0;
    //BOOL VALUES
    public bool IsRunning;
    private bool IsNewRotate = true;
    //acceleration values
    private float accelerationValue;
    //Events
    public delegate void DirectionChange();
    public event DirectionChange DirectionChanged;
    
    public MovementComponent(FootballPlayer player, string RunningParamName, string JumpingParamName, string WalkingParamName,float MoveSkill)
    {
        footballPlayer = player;
        this.RunningParamName = RunningParamName;
        this.JumpingParamName = JumpingParamName;
        this.WalkingParamName = WalkingParamName;
        this.MoveSkill = MoveSkill / 10;

    }

    public void StartMoveComp()
    {
        destination = null;
       // m_animator.SetBool(m_RunningParamName, false);
        accelerationValue = MoveSkill / 10;
        maxSpeed = MoveSkill;
        angularSpeed = 360;
        RunningSpeed = MoveSkill;
        WalkingSpeed = RunningSpeed * 3 / 4;
       
        rotation = footballPlayer.PlayerObject.transform.rotation;
        WorldVelocity = footballPlayer.PlayerObject.transform.forward;
        targetRotation = footballPlayer.PlayerObject.transform.rotation;
        DirectionChanged += SetRotationCorrector;
    }

    // Update is called once per frame
    public void UpdateMove()
    {
        //NOT TO CHANGE THE ORDER
        /* TODO:may be ficha 
        if (ShouldChangeRotation() && IsNewRotate) OnDirectionChanged(); */
        CheckHeroStop();
        GoTo();
    //    Debug.Log(rotation.eulerAngles.y);

    }

    public void MoveToPoint(Vector3 destination)
    {
        this.destination = destination;
        footballPlayer.animator.SetBool(RunningParamName, true);
        WorldVelocity = (this.destination - footballPlayer.transform.position).Value.normalized;

        // footballer.transform.LookAt(destination);
        //footballer.transform.Translate(destination);
    }
    private void GoTo()
    {
        CorrectSpeed();
        RotateFootballer();
        footballPlayer.transform.position += WorldVelocity * speed / 60;

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
           footballPlayer.animator.SetBool(WalkingParamName, false);
          //  m_speed = 0;
            targetSpeed = 0;
            
        }
    }


    public void Jump()
    {
       footballPlayer.animator.SetTrigger(JumpingParamName);
        // m_animator.SetTrigger(m_JumpingParamName);
    }

    public void StopHero()
    {
        targetSpeed = 0;
        footballPlayer.animator.SetBool(WalkingParamName, false);
        footballPlayer.animator.SetBool(RunningParamName, false);
        IsNewRotate = true;//for
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
        if (!ShouldChangeRotation())
        {
            IsNewRotate = true;
            return;
        }
        
        float angleCanPass = angularSpeed / 60;
        float angleToPass = Math.Abs(rotation.eulerAngles.y - targetRotation.eulerAngles.y);
        if(angleToPass >= 180) angleToPass = 360 - angleToPass;
        if (angleCanPass > angleToPass)
        {
            angleCanPass = angleToPass;
        }
        rotation =Quaternion.Euler(0,rotation.eulerAngles.y + angleCanPass * rotationCorrector, 0);
        footballPlayer.transform.rotation = rotation;
        WorldVelocity = footballPlayer.transform.forward;        

    }
    private void StartWalking()
    {
        targetSpeed = WalkingSpeed;
        footballPlayer.animator.SetBool(WalkingParamName, true);
    }
    public void StartRunning()
    {
        targetSpeed = RunningSpeed;
        footballPlayer.animator.SetBool(RunningParamName, true);
        if (driblingType != null) driblingType = DriblingType.FastHits;
        IsRunning = true;
    }

   

    public void FinishRunning()
    {
        
        footballPlayer.animator.SetBool(RunningParamName, false);
        if (driblingType != null) driblingType = DriblingType.SlowKeep;
        IsRunning = false;
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
       // Debug.Log(angleBetween);

        if (angleBetween > footballPlayer.ApproximetlyMistakeValue && angleBetween < 360 - footballPlayer.ApproximetlyMistakeValue)
        {
            return true;
        }
        return false;
    }

    private void SetRotationTarget(float AxisVert,float AxisHor)
    {
        targetRotation = CalculateRotation(AxisVert, AxisHor);
       // Debug.Log(" TargetRotation:" + targetRotation.eulerAngles.y + " MyRotation:" + m_rotation.eulerAngles.y + "ObjectRotation:" + owner.transform.rotation.eulerAngles.y + " ShouldChangeRotation:" + ShouldChangeRotation());
    }
    private void SetSpeedByAxis(float AxisVert,float AxisHor)
    {
        if (AxisHor == 0 && AxisVert == 0) StopHero();
        else if (!ShouldChangeRotation() && targetSpeed < 7.5) StartWalking();
    }
    public void SetMovingDataByAxis(float AxisVert,float AxisHor)
    {
        SetRotationTarget(AxisVert, AxisHor);
        SetSpeedByAxis(AxisVert, AxisHor);
    }

    private void SetRotationCorrector()
    {
        float m_angle = rotation.eulerAngles.y;
        float target_angle = targetRotation.eulerAngles.y;
        float angleToPass = Mathf.Abs(m_angle - target_angle);
       /* if (m_angle > target_angle && angleToPass < 180) rotationCorrector = -1;
        if (m_angle > target_angle && angleToPass > 180) rotationCorrector = 1;
        if (m_angle < target_angle && angleToPass < 180) rotationCorrector = 1;
        
        if (m_angle < target_angle && angleToPass > 180) rotationCorrector = -1;*/
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
        if(DirectionChanged != null)
        {
            DirectionChanged();
            IsNewRotate = false;
           // Debug.Log("CurrentRotation" + m_rotation.eulerAngles.y + " TargetRotation:" + targetRotation.eulerAngles.y);
        }
    }
    public void SetTargetRotation(float angle)
    {
        targetRotation = Quaternion.Euler(0,angle,0);
    }
    public void SetSkillLevel(float skillLevel)
    {
        MoveSkill = skillLevel;
    }

    
}
