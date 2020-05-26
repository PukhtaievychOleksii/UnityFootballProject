using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicHelper
{

    public static float CalculatePassForce(Vector3 target, Vector3 currentPos,float ball_mass)
    {
        float distance = (target - currentPos).magnitude;
        float passTime = 3;
        float aceleration;
        float force;

        if (distance < 5) passTime = 1;
        if (distance > 15) passTime = 5;
        aceleration = 2 * distance / passTime / passTime;
        force = aceleration * ball_mass;
        return force;
    }
}