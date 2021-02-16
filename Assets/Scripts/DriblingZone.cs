using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriblingZone : MonoBehaviour
{
    private FootballPlayer owner;
    private const float FastKeepMultiplier = 650f;
    private float dribllingRadius = 0.1f;
    public GameObject driblingPoint;
    private BoxCollider zone;
    private float timeBettwenGatherTrials = 0;
    private float standartTimeBettwenGathersTrials = 1f;

    void Start()
    {
        zone = GetComponent<BoxCollider>();
    }
    public void SetOwner(FootballPlayer footballPlayer)
    {
        owner = footballPlayer;
    }
    private void Update()
    {
        if (owner.IsBallKepper() && !owner.Ball.IsUnderForceAffect)
        {
            Drible();
        }
        else 
        {
            timeBettwenGatherTrials += Time.deltaTime;
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        Ball ball = other.gameObject.GetComponentInParent<Ball>();
        if (ball == null) return;
        if (ball.IsWithoutKeeper()) ball.ChangeKeeper(owner);
        //Stop All Forces
        PhysicHelper.StopAllPhysicForces(ball.rigidBody/*ball.gameObject.GetComponent<Rigidbody>()*/);
        PhysicHelper.StopAllPhysicForces(owner.FootballerObject.GetComponent<Rigidbody>());
        owner.Ball.IsUnderForceAffect = false;
        SetDriblingType();    
        
    }
    private void OnTriggerStay(Collider other)
    {
        Ball ball = other.GetComponentInParent<Ball>();
        if (ball != null)
        {
            if (ball.IsWithoutKeeper() || AreSuitConditionsToSteal())

            {
                owner.DefenseComp.TryStealBall();
                timeBettwenGatherTrials = 0;
            }
        }
        

    }
    private void OnTriggerExit(Collider other)
    {
        Ball ball = other.GetComponentInParent<Ball>();
        if (ball != null)
        {
            //  TODO : problem wright here
            if (ball.keeper == owner) ball.keeper = null;
            owner.MoveComp.driblingType = null;

        }
    }



    private void KeepBallInDribleZone()
    {
        float distance = (owner.Ball.transform.position - driblingPoint.transform.position).magnitude;
        if(distance > dribllingRadius)
        {
            owner.Ball.ShiftToPoint(driblingPoint.transform.position);
           // owner.Ball.transform.position = driblingPoint.transform.position;/*zone.transform.TransformPoint(zone.center) + owner.transform.forward * 0.2f + owner.transform.up * 0.1f;*/
        }
        
    }

    private void Drible()
    {
        if (owner.MoveComp.driblingType == DriblingType.SlowKeep)
        {
            KeepBallInDribleZone();
        }
        else if (owner.MoveComp.driblingType == DriblingType.FastKeep)
        {
            owner.Ball.HitBall(owner.transform.forward, FastKeepMultiplier);
        }
        
    }

    public Vector3 GetDriblingPointPosition()
    {
        return driblingPoint.transform.position;
    }

    private void SetDriblingType()
    {
        if (owner.MoveComp.IsRunning) owner.MoveComp.driblingType = DriblingType.FastKeep;
        else
        {
            owner.MoveComp.driblingType = DriblingType.SlowKeep;
        }
    }


  

    private bool IsTimeBettwenGatherTrialBigEnough()//Here,because there is no Update in DefenseComponentClass
    {
        if (timeBettwenGatherTrials > standartTimeBettwenGathersTrials)
        {
            timeBettwenGatherTrials = 0;
            return true;
        }
        return false;
    }

 

    public void BallWasStolen()
    {
        timeBettwenGatherTrials = 0;
    }

    private bool AreSuitConditionsToSteal()
    {
        if ( IsTimeBettwenGatherTrialBigEnough()) return true;
        return false;

    }


    

  
}
