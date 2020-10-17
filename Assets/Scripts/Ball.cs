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
    public Transform transform;
    public  Transform RotationPointTransform;
    public bool IsUnderForceAffect = false;
    public FootballPlayer kepper = null;

    public event KeeperChanged OnKeeperChanged;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
        transform = GetComponent<Transform>();
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



    
}
