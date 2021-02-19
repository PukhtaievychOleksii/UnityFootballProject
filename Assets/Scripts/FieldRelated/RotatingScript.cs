using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingScript : MonoBehaviour
{
    Transform transform;
    private const float AngleToAdd = 1f / 60;
    public float XRotatingCoef = 0f;
    public float YRotatingCoef = 0f;
    public float ZRotatingCoef = 0f;
    public float RotationSpeedAmplifier = 1;
    public bool EnableXRotation;
    public bool EnableZRotation;
    public bool EnableYRotation;
    public 

    void Start()
    {
      transform = GetComponent<Transform>();
      
      if(EnableXRotation && XRotatingCoef == 0)  XRotatingCoef = Random.Range(-1,1);
      if(EnableYRotation && YRotatingCoef == 0)  YRotatingCoef = Random.Range(-1,1);
      if(EnableZRotation && ZRotatingCoef == 0)  ZRotatingCoef = Random.Range(-1,1);
    }

    // Update is called once per frame
    void Update()
    {
        float NewXRotation = transform.rotation.eulerAngles.x + XRotatingCoef * AngleToAdd * RotationSpeedAmplifier;
        float NewYRotation = transform.rotation.eulerAngles.y + YRotatingCoef * AngleToAdd * RotationSpeedAmplifier;
        float NewZRotation = transform.rotation.eulerAngles.z + ZRotatingCoef * AngleToAdd * RotationSpeedAmplifier;
        transform.rotation = Quaternion.Euler(NewXRotation, NewYRotation, NewZRotation);
        
         
            
    }
}
