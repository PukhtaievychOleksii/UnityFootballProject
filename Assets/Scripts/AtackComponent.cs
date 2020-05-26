using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent 
{
    private FootballPlayer owner;
    private double m_AtackSkill;//Use Values from 1 to 10
    private double goalMistake;//number,according to which,player won't shoot in the aim
        // Start is called before the first frame update
    public AttackComponent(FootballPlayer player,int AtackSkill)
    {
        m_AtackSkill = AtackSkill / 10;
        owner = player;
    }

    public void StartAttackComp()
    {
        goalMistake = 1 - 0.1 * m_AtackSkill;
    }
    public void Pass(FootballPlayer receivingPlayer)
    {
     if(owner.m_ball != null)
        {
            Vector3 direction = (owner.transform.position - receivingPlayer.transform.position).normalized;
            float ForceCoef = PhysicHelper.CalculatePassForce(receivingPlayer.transform.position, owner.transform.position, owner.m_ball.rigidBody.mass);
            owner.m_ball.rigidBody.AddForce(direction * ForceCoef);
        }
    }

    
}
