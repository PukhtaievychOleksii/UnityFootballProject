using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Transform transform;
    public  Transform RotationPointTransform;
    public bool IsUnderKeepin = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
        transform = GetComponent<Transform>();
    }

    public void PushBall(Vector3 direction)
    {
        rigidBody.AddForce(direction.normalized * 500);
    }
    void Update()
    {
        
    }
}
