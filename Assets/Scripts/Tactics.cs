using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FieldPositionShortName
{
    LD1, LD2,
    RD1, RD2,
    LF1, LF2,
    RF1, RF2,
    GK1, GK2
}

public struct FieldPosition
{
    public string FullName;
    public FieldPositionShortName ShortName;
    public GameObject PositionPoint;
    public FieldPosition(FieldPositionShortName shortName, string fullName, GameObject positionPoint)
    {
        FullName = fullName;
        ShortName = shortName;
        PositionPoint = positionPoint;
    }

}
public class Tactics 
{
    public string Name;
    public List<FieldPosition> FieldPositions = new List<FieldPosition>();
    public List<GameObject> SpawnPoints;
    public Tactics(List<GameObject> spawnPoints,string name)
    {
        SpawnPoints = spawnPoints;
        Name = name;
    }

  

    

    public GameObject GetAppropriateSpawnPoint(string PointName)
    {
        GameObject RequiredSpawnPoint = new GameObject();
        foreach(GameObject spawnPoint in SpawnPoints)
        {
            if (spawnPoint.name == PointName) RequiredSpawnPoint = spawnPoint;
        }
        if (RequiredSpawnPoint == null) Debug.LogError("Mistake with spawnpoints name");
        return RequiredSpawnPoint;
    }

    public void AddFieldPosition(FieldPositionShortName shortName, string fullName)
    {
        FieldPositions.Add(new FieldPosition(shortName, fullName, GetAppropriateSpawnPoint(fullName)));
    }

    public FieldPosition GetAppropriateFieldPostion(FieldPositionShortName shortName)
    {
        FieldPosition RequiredFieldPosition = new FieldPosition();
        foreach(FieldPosition fieldposition in FieldPositions)
        {
            if (fieldposition.ShortName == shortName) RequiredFieldPosition = fieldposition;
        }
        return RequiredFieldPosition;
    }

}
