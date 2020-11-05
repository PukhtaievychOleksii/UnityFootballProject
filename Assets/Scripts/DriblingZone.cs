using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriblingZone : MonoBehaviour
{
    private FootballPlayer owner;
    private Ball ball;
    private float dribllingRadius = 0.1f;
    public GameObject driblingPoint;
    private BoxCollider zone;
    private float timeBettwenGatherTrials = 0;
    private float standartTimeBettwenGathersTrials = 1f;

    // Start is called before the first frame update
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
        //TODO:Drible();
        if(!IsITheBallKepper()) timeBettwenGatherTrials += Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball == null) return;
        if (IsBallWithoutKepper()) ball.ChangeKeeper(owner);
        SetTimeBettwenStealTrials();
        ball = this.ball;
        //Stop All Forces
        PhysicHelper.StopAllPhysicForces(ball.rigidBody/*ball.gameObject.GetComponent<Rigidbody>()*/);
        owner.ball.IsUnderForceAffect = false;
        SetDriblingType();    
        
    }
    private void OnTriggerStay(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            if (IsBallWithoutKepper() || IsSuitConditionsToSteal())

            {
                owner.DefenseComp.TryStealBall();
                timeBettwenGatherTrials = 0;
            }
        }
        

    }
    private void OnTriggerExit(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            if (ball.kepper == owner) ball.kepper = null;
            owner.ball = null;
            owner.MoveComp.driblingType = null;

        }
    }



    private void CheckBallInZone()
    {
        float distance = (owner.ball.transform.position - driblingPoint.transform.position).magnitude;
        if(distance > dribllingRadius)
        {
            owner.ball.transform.position = driblingPoint.transform.position;/*zone.transform.TransformPoint(zone.center) + owner.transform.forward * 0.2f + owner.transform.up * 0.1f;*/
        }
        
    }

    private void Drible()
    {

        if (AreValuesSet())
        {
            if (owner.MoveComp.driblingType == DriblingType.SlowKeep) CheckBallInZone();
            else if (owner.MoveComp.driblingType == DriblingType.FastHits)
            {
                owner.ball.HitBall(owner.transform.forward, 500);
            }
        }
    }

    public Vector3 GetDriblingPointPosition()
    {
        return driblingPoint.transform.position;
    }

    private void SetDriblingType()
    {
        if (owner.MoveComp.IsRunning) owner.MoveComp.driblingType = DriblingType.FastHits;
        else
        {
            owner.MoveComp.driblingType = DriblingType.SlowKeep;
        }
    }


    private bool IsBallWithoutKepper()
    {
        if (owner.ball != null && owner.ball.kepper == null) return true;
        return false;
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

    private bool IsITheBallKepper()
    {
        if (owner.ball == null || owner.ball.kepper == null || owner.ball.kepper != owner)
        {

            return false;
        }
        return true;
    }

    public void BallWasStolen()
    {
        timeBettwenGatherTrials = 0;
    }

    private bool IsSuitConditionsToSteal()
    {
        if (IsITheBallKepper()) return false;
        else if(owner.ball.kepper != null && owner.IsItMineOponent(owner.ball.kepper)) owner.currentOpponent = owner.ball.kepper;
        if (IsTimeBettwenGatherTrialBigEnough()) return true;
        return false;

    }
    private void SetTimeBettwenStealTrials()
    {
        if (owner.currentOpponent != null && owner.ball != null && owner.ball.kepper != null)
        {
            if (owner.ball.kepper != owner.currentOpponent) timeBettwenGatherTrials = standartTimeBettwenGathersTrials;
        }
    }

    private bool AreValuesSet()
    {
        if (owner.ball == null) return false;
        if (!PhysicHelper.IsOnGround(owner.gameObject)) return false;
        if (owner.ball.IsUnderForceAffect) return false;
        if (owner.ball.kepper != owner) return false;
        return true;
    }
}
