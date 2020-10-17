using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseComponent
{
    public float DefenseSkill;
    private FootballPlayer owner;
    private bool IsPressing = false;
    private FootballPlayer currentOponent;

    public DefenseComponent(FootballPlayer player, float defenseskill)
    {
        owner = player;

        DefenseSkill = defenseskill;
    }

    public void UpdateDefComp()
    {
        if (IsPressing) AnaliseOponentandChooseTheDifferenceWay();
    }
    public void TryStealBall()
    {
        if (owner.ball == null || owner.ball.kepper == owner) return;
        if (CanTakeTheBall()) owner.ball.ChangeKeeper(owner);


    }

    private bool CanTakeTheBall()
    {
        if (owner.ball.kepper == null) return true;

        float oponentDriblingLevel = owner.ball.kepper.AtackComp.AtackSkill;
        float ownerDefenseLevel = owner.DefenseComp.DefenseSkill;
        float skillDifference = ownerDefenseLevel - oponentDriblingLevel;
        float successPercent = GetSuccessPercent(skillDifference);//TODO:Normalno OGRanicit
        System.Random random = new System.Random();
        float randomValue = random.Next(0, 100);

        if (successPercent > randomValue)
        { owner.ball.kepper.driblingZone.BallWasStolen();
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

    public void StartPressing(FootballPlayer oponent)
    {
        IsPressing = true;
        currentOponent = oponent;
    }
    public void StopPressing()
    {
        IsPressing = false;
        currentOponent = null;
    }
    private void AnaliseOponentandChooseTheDifferenceWay()
    {
        if (currentOponent.IsBallKepper()) PressOponent();
        else OvertakeOponent();
    }
    //TODO:Create normal fabrika
    private void OvertakeOponent()
    {
        throw new NotImplementedException();
    }

    private void PressOponent()
    {
        throw new NotImplementedException();
    }
}
