using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.AI;
using System;

public delegate void KeeperChanged();

public class Ball : MonoBehaviour
{
    public Rigidbody rigidBody;
    public GameObject ballObject;
    public bool IsUnderForceAffect = false;
    public FootballPlayer keeper = null;
    public FootballPlayer LastKeeper  = null;

    public event KeeperChanged OnKeeperChanged;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    
   
    public void HitBall(Vector3 direction, float multiplayer)
    {
        IsUnderForceAffect = true;
        rigidBody.AddForce(direction * multiplayer);
    }
    
    public void ChangeKeeper(FootballPlayer player)
    {
        keeper = player;
        if (keeper != null)
        {
            LastKeeper = keeper;
        }
        OnKeeperChanged();
            
    }
    public bool IsWithoutKeeper()
    {
        if (keeper == null) return true;
        return false;
    }

    public void MoveToImmediately(Vector3 Position)
    {
        ballObject.transform.position = Position;
    }

    public void ShiftToPoint(Vector3 point)
    {
        Vector3 Velocity = point - ballObject.transform.position;
        ballObject.transform.position += Velocity * 50 / 60; 
    }


    
}
