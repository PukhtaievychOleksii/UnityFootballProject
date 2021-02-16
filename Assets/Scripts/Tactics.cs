using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FieldSide
{
    Left,
    Right
}

public struct FieldPosition
{
    public string FullName;
    public GameObject DefensePoint;
    public GameObject AtackPoint;
    public FieldPosition(string fullName, GameObject positionPoint)
    {
        FullName = fullName;
        DefensePoint = positionPoint;
        AtackPoint = positionPoint.transform.GetChild(0).gameObject;
    }
    

}
public class Tactic 
{
    public string Name;
    public List<FieldPosition> FieldPositions = new List<FieldPosition>();
    
    public GameObject TacticObject;
    public FieldSide FieldSide;

    public Tactic(GameObject tacticObject,string name,FieldSide fieldSide)
   {
        TacticObject = tacticObject;
        Name = name;
        FieldSide = fieldSide;
        SetAllFieldPositionsAvailable();
    }

  

    private void SetAllFieldPositionsAvailable()
    {
    

        for(int i = 0;i < TacticObject.transform.childCount; i++)
        {
            GameObject PositionPoint = TacticObject.transform.GetChild(i).gameObject;
            string positionName = PositionPoint.name;
            FieldPosition fieldPosition = new FieldPosition(positionName, PositionPoint);
            FieldPositions.Add(fieldPosition);
        }
        CheckFieldPositionPresence();
    }
    private void CheckFieldPositionPresence()
    {
        if (FieldPositions.Count < 5) Debug.LogError("Something went wrong with positionSetting,maybe with names.");
    }

    public FieldPosition GetFieldPosition(string positionName)
    {
        FieldPosition fieldPosition = new FieldPosition();
        foreach (FieldPosition position in FieldPositions)
        {
            if (position.FullName == positionName) { fieldPosition = position; break; }
        }
        return fieldPosition;
    }

}
