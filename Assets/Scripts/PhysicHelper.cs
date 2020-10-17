using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public  static class PhysicHelper
{
    public static float StandartXRotation = 0;
    public static float StandartZRotation = 0;
    public static float CalculatePassForce(Vector3 target, Vector3 currentPos,float ball_mass)
    {
        float distance = (target - currentPos).magnitude;
        float passTime = GetPassTime(distance);
        float aceleration;
        float force;

       
        aceleration = 2 * distance / passTime / passTime;
        force = aceleration * ball_mass;
        return force;
    }
    public static void LookAtByY(GameObject gameObject, Vector3 target)
    {
        gameObject.transform.LookAt(target);
        gameObject.transform.rotation = new Quaternion(PhysicHelper.StandartXRotation, gameObject.transform.rotation.y, PhysicHelper.StandartZRotation, gameObject.transform.rotation.w);
    }

    public static float GetAngleBettwenPlayers(GameObject mainPlayer,GameObject secondPlayer)
    {
        Vector3 vectorA = secondPlayer.transform.position - mainPlayer.transform.position;
        Vector3 vectorB =(mainPlayer.transform.forward).normalized;
        Vector3 vectorC = vectorA + vectorB;
        float sideA = vectorA.magnitude;
        float sideB = vectorB.magnitude;
        float sideC = vectorC.magnitude;
        float CosX = (sideA * sideA + sideB * sideB - sideC * sideC) / (2 * sideA * sideB);
        float angle = Mathf.Acos(CosX);
        return angle;
    }

    public static bool IsOnGround(GameObject gameObject)
    {
        if (gameObject.transform.position.y - Game.StandartYPosition < 0.6) return true;
        return false;
    }

    private static float GetPassTime(float distance)
    {
        if (distance < 5) return 1;
        if (distance > 15) return 5;
        return 3;
    }

    public static void StopAllPhysicForces( Rigidbody rigidBody)
    {
        rigidBody.isKinematic = true;
        rigidBody.isKinematic = false;
    }
   
}