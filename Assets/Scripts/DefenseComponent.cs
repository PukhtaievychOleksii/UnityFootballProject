using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseComponent
{
    public float DefenseSkill;
    private List<FootballPlayer> CurrentOponents = new List<FootballPlayer>();
    private float DefenseRadius = 0;
    private FootballPlayer owner;
    private bool IsPressing = false;
    private FootballPlayer CurrentOponent;

    public DefenseComponent(FootballPlayer player,float DefaultDefendingRadius, float defenseskill)
    {
        owner = player;
        DefenseSkill = defenseskill;
        SetDefendingRadius(DefaultDefendingRadius);
    }

    public void UpdateDefComp()
    {
        
    }

   

    public void TryStealBall()
    {
        if (owner.IsBallKepper()) return;
        if (CanTakeTheBall()) owner.Ball.ChangeKeeper(owner);


    }

    private bool CanTakeTheBall()
    {
        if (!owner.IsBallKepper()) return true;

        float oponentDriblingLevel = owner.Ball.keeper.AtackComp.AtackSkill;
        float ownerDefenseLevel = owner.DefenseComp.DefenseSkill;
        float skillDifference = ownerDefenseLevel - oponentDriblingLevel;
        float successPercent = GetSuccessPercent(skillDifference);
        System.Random random = new System.Random();
        float randomValue = random.Next(30, 70);

        if (successPercent > randomValue)
        { owner.Ball.keeper.DriblingZone.BallWasStolen();
            return true;
        }
        return false;
    }



    private float GetSuccessPercent(float skillDifference)
    {
        float successPercent = 50 + skillDifference * 5;
        if (successPercent > 90) successPercent = 90;
        if (successPercent < 20) successPercent = 20;
        return successPercent;
    }

    public void SetSkillLevel(float skillLevel)
    {
        DefenseSkill = skillLevel;
    }
    private void SetDefendingRadius(float defaultDefendingRadius)
    {
        DefenseRadius = defaultDefendingRadius * DefenseSkill / 100;
    }

    public void AnaliseOponentsLocation()
    {
           foreach(FootballPlayer oponent in owner.VariableParams.Team.Oponents)
        {
            float distanceBetween = (owner.FootballerObject.transform.position - oponent.FootballerObject.transform.position).magnitude;
            if (distanceBetween < DefenseRadius) CurrentOponents.Add(oponent);
        }
    }
    public void ChooseOponentForPressing()
    {
        if (CurrentOponents.Count == 0) return;
        FootballPlayer OponentForPressing = CurrentOponents[0];
        int MaxDefenseWorth = 0;
        foreach(FootballPlayer Oponent in CurrentOponents)
        {
            int OponentDefenseWorth = CalculateDefenseWorth(Oponent);
            if (OponentDefenseWorth > MaxDefenseWorth) { MaxDefenseWorth = OponentDefenseWorth; OponentForPressing = Oponent; }
        }
        CurrentOponent = OponentForPressing;
    }

    private int CalculateDefenseWorth(FootballPlayer oponent)
    {
        int TotalDefenseWorth = 0;
        if (oponent.IsBallKepper()) TotalDefenseWorth += 2;
        else
        {
            if (IsOponentCloserToTheBall(oponent)) TotalDefenseWorth += 1;
        }
        if (IsOponentCloserToOurGates(oponent)) TotalDefenseWorth += 1;
        return TotalDefenseWorth;
    }

    private bool IsOponentCloserToTheBall(FootballPlayer oponent)
    {
        float m_distanceToTheBall = (owner.transform.position - owner.Ball.transform.position).magnitude;
        float oponentDistanceToTheBall = (oponent.transform.position - owner.Ball.transform.position).magnitude;
        if (oponentDistanceToTheBall < m_distanceToTheBall) return true;
        return false;
    }

    private bool IsOponentCloserToOurGates(FootballPlayer oponent)
    {
        float m_distanceToGates = (owner.transform.position - owner.VariableParams.Team.GatesToDefend.transform.position).magnitude;
        float oponentDistanceToGates = (oponent.transform.position - owner.VariableParams.Team.GatesToDefend.transform.position).magnitude;
        if (oponentDistanceToGates < m_distanceToGates) return true;
        return false;
    }

    public void DoDefenseActions()
    {
        if (CurrentOponent != null)
        {
            owner.MoveComp.RunToPoint(CurrentOponent.transform.position);
        }
        
    }
}
