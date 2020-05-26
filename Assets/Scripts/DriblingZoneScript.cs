using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriblingZoneScript : MonoBehaviour
{
    private FootballPlayer owner;
    private float dribllingRadius = 0.2f;
    private BoxCollider zone;
    private bool IsBallPushed = false;
    private MovementComponent footballer_MoveComp;
    // Start is called before the first frame update
    void Start()
    {
        owner = GetComponentInParent<FootballPlayer>();
        footballer_MoveComp = owner.MoveComp;
        zone = GetComponent<BoxCollider>();
        
        
    }

    private void Update()
    {
        if (owner.m_ball == null) return;
        if (owner.MoveComp.driblingType == DriblingType.SlowKeep) CheckBallInZone();
        if (owner.MoveComp.driblingType == DriblingType.FastHits && !IsBallPushed)
        {
            owner.m_ball.PushBall(owner.transform.forward);
            IsBallPushed = true;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            // owner.MoveComp.KeepBall(ball);
            owner.m_ball = ball;
            if (owner.MoveComp.IsRunning) owner.MoveComp.driblingType = DriblingType.FastHits;
            else
            {
                owner.MoveComp.driblingType = DriblingType.SlowKeep;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            owner.m_ball = null;
            owner.MoveComp.driblingType = null;
            IsBallPushed = false;

        }
    }

    private void CheckBallInZone()
    {
        float distance = (owner.m_ball.transform.position - zone.transform.position).magnitude;
        if(distance > dribllingRadius)
        {
            owner.m_ball.transform.position = zone.transform.TransformPoint(zone.center) + owner.transform.forward * 0.2f;
        }
        
    }


}
