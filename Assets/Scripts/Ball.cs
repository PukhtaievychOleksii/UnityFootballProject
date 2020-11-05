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
    public FootballPlayer kepper;

    public event KeeperChanged OnKeeperChanged;
    // Start is called before the first frame update
    void Awake()
    {
        ballObject = this.GetComponent<GameObject>();
        rigidBody = GetComponent<Rigidbody>();
    }
    
   
    public void HitBall(Vector3 direction, float multiplayer)
    {
        IsUnderForceAffect = true;
        rigidBody.AddForce(direction * multiplayer);
    }
    void Update()
    {
       
    }
    public void ChangeKeeper(FootballPlayer player)
    {
        kepper = player;
        OnKeeperChanged();
            
    }

    public void MoveToImmediately(Vector3 Position)
    {
        ballObject.transform.position = Position;
    }



    
}
