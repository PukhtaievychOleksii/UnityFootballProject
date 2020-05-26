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
    public Vector3 m_WorldVelocity;
    //speed values
    public float m_speed = 0;
    private float maxSpeed;
    private float minSpeed = 5;
    private float WalkingSpeed;
    private float RunningSpeed;
    private float targetSpeed = 0;
    //rotation values
    private Quaternion m_rotation;
    private float m_angularSpeed;//graduses per second
    private Quaternion targetRotation;
    private Vector3? m_destination;
    private Animator m_animator;
    private int rotationCorrector = -1;
    //dribling values
    public bool IsDribling;
    public DriblingType? driblingType = null;
    //parameter's names
    private string m_RunningParamName;
    private string m_JumpingParamName;
    private string m_WalkingParamName;
    private FootballPlayer owner;//Use values from 1 to 10
    public float m_MoveSkill = 0;
    //BOOL VALUES
    public bool IsRunning;
    private bool IsNewRotate = true;
    //acceleration values
    private float accelerationValue;
    //Events
    public delegate void DirectionChange();
    public event DirectionChange DirectionChanged;
    
    public MovementComponent(FootballPlayer player, string RunningParamName, string JumpingParamName, string WalkingParamName, Animator animator, float MoveSkill)
    {
        owner = player;
        m_RunningParamName = RunningParamName;
        m_JumpingParamName = JumpingParamName;
        m_animator = animator;
        m_WalkingParamName = WalkingParamName;
        m_MoveSkill = MoveSkill / 10;

    }

    public void StartMoveComp()
    {
        m_destination = null;
        m_animator.SetBool(m_RunningParamName, false);
        accelerationValue = m_MoveSkill / 10;
        maxSpeed = m_MoveSkill;
        m_angularSpeed = 360;
        RunningSpeed = m_MoveSkill;
        WalkingSpeed = RunningSpeed * 3 / 4;
        m_rotation = owner.transform.rotation;
        m_WorldVelocity = owner.transform.forward;
        targetRotation = owner.transform.rotation;
        DirectionChanged += CheckRotationCorrector;
    }

    // Update is called once per frame
    public void UpdateMove()
    {
        //NOT TO CHANGE THE ORDER
        if (ShouldChangeRotation() && IsNewRotate) OnDirectionChanged();
        CheckHeroStop();
        GoTo();

    }

    public void MoveToPoint(Vector3 destination)
    {
        m_destination = destination;
        m_animator.SetBool(m_RunningParamName, true);
        m_WorldVelocity = (m_destination - owner.transform.position).Value.normalized;

        // footballer.transform.LookAt(destination);
        //footballer.transform.Translate(destination);
    }
    private void GoTo()
    {
        CorrectSpeed();
        RotateFootballer();
        owner.transform.position += m_WorldVelocity * m_speed / 60;

    }


    public Quaternion CalculateRotation(float AxisVert, float AxisHor)
    {

      //  Debug.Log("Vert:" + AxisVert + " Hor:" + AxisHor);
        if (AxisVert < 0 && AxisHor > 0)
        {
           return Game.RotetionDirections["Down-Right"];

        }
        if (AxisVert < 0 && AxisHor < 0)
        {
            return Game.RotetionDirections["Down-Left"];
        }
        if (AxisVert > 0 && AxisHor > 0)
        {
            return Game.RotetionDirections["Up-Right"];
        }
        if (AxisVert > 0 && AxisHor < 0)
        {
            return Game.RotetionDirections["Up-Left"];
        }
        if (AxisVert > 0)
        {
            return Game.RotetionDirections["Up"];
        }
        if (AxisVert < 0)
        {
            return Game.RotetionDirections["Down"];

        }
        if (AxisHor > 0)
        {
            return Game.RotetionDirections["Right"];
        }
        if (AxisHor < 0)
        {
           return Game.RotetionDirections["Left"];

        }
        return targetRotation;
    }



   
   
    public void CheckHeroStop()
    {
        if (targetSpeed < 0.1)
        {
            m_animator.SetBool(m_WalkingParamName, false);
          //  m_speed = 0;
            targetSpeed = 0;
        }
    }


    public void Jump()
    {
        m_animator.SetTrigger(m_JumpingParamName);
        // m_animator.SetTrigger(m_JumpingParamName);
    }

    public void StopHero()
    {
        targetSpeed = 0;
        m_animator.SetBool(m_WalkingParamName, false);
        m_animator.SetBool(m_RunningParamName, false);
        IsNewRotate = true;//for
    }
  private void CorrectSpeed()
    {
        Accelerate();
        if (driblingType != null) m_speed = m_speed * 3 / 4;
        if (m_speed > maxSpeed) m_speed = maxSpeed;
        if (m_speed < minSpeed && targetSpeed > 1) m_speed = minSpeed;
    }

    private void Accelerate()
    {
        if (targetSpeed < 5) m_speed = targetSpeed;//Momental acceleration on low speeds
        if (targetSpeed > minSpeed && m_speed < minSpeed) m_speed = minSpeed;
        int corrector = 1;
        if (m_speed > targetSpeed) corrector = -1;
        if (ShouldChangeSpeed())    m_speed = m_speed + accelerationValue * corrector;
        
        
    }

    private void RotateFootballer()
    {

        if (!ShouldChangeRotation())
        {
            IsNewRotate = true;
            return;
        }
        m_rotation =Quaternion.Euler(0, m_rotation.eulerAngles.y + m_angularSpeed / 60 * rotationCorrector, 0);
        owner.transform.rotation = m_rotation;
        m_WorldVelocity = owner.transform.forward;        

    }
    private void StartWalking()
    {
        targetSpeed = WalkingSpeed;
        m_animator.SetBool(m_WalkingParamName, true);
    }
    public void StartRunning()
    {
        targetSpeed = RunningSpeed;
        m_animator.SetBool(m_RunningParamName, true);
        if (driblingType != null) driblingType = DriblingType.FastHits;
        IsRunning = true;
    }

   

    public void FinishRunning()
    {
        
        m_animator.SetBool(m_RunningParamName, false);
        if (driblingType != null) driblingType = DriblingType.SlowKeep;
        IsRunning = false;
        StartWalking();
    }

  
    private bool ShouldChangeSpeed()
    {
        if (Mathf.Abs(targetSpeed - m_speed) > 0.1 && targetSpeed > minSpeed) return true;
        return false;
    }
    private bool ShouldChangeRotation()
    {
        float angleBetween = Mathf.Abs(m_rotation.eulerAngles.y - targetRotation.eulerAngles.y);
        if (angleBetween > 2 && angleBetween < 358) return true;
        return false;
    }

    private void SetRotationTarget(float AxisVert,float AxisHor)
    {
        targetRotation = CalculateRotation(AxisVert, AxisHor);
       // Debug.Log(" TargetRotation:" + targetRotation.eulerAngles.y + " MyRotation:" + targetRotation.eulerAngles.y + "ObjectRotation:" + owner.transform.rotation.eulerAngles.y);
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

    private void CheckRotationCorrector()
    {
        float m_angle = m_rotation.eulerAngles.y;
        float target_angle = targetRotation.eulerAngles.y;
        float angleToPass = Mathf.Abs(m_angle - target_angle);
        if (m_angle > target_angle && angleToPass < 180) rotationCorrector = -1;
        if (m_angle > target_angle && angleToPass > 180) rotationCorrector = 1;
        if (m_angle < target_angle && angleToPass < 180) rotationCorrector = 1;
        if (m_angle < target_angle && angleToPass > 180) rotationCorrector = -1;
    }
    public void OnDirectionChanged()
    {
        if(DirectionChanged != null)
        {
            DirectionChanged();
            IsNewRotate = false;
            Debug.Log("CurrentRotation" + m_rotation.eulerAngles.y + " TargetRotation:" + targetRotation.eulerAngles.y);
        }
    }
}
