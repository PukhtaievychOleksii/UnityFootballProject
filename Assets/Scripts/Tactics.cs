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
public class Tactics : MonoBehaviour
{
    public GameObject BallSpawnPoint;
    public List<FieldPosition> FieldPositions = new List<FieldPosition>();
    public List<GameObject> SpawnPoints;
    void Start()
    {
        if (SpawnPoints == null) Debug.LogError("SpawnPoints are not set");
        SetFieldPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetFieldPositions()
        //TODO:change Dictionary on Array with GetApropriateFieldPosition
    {
        AddFieldPosition(FieldPositions,FieldPositionShortName.LF1,"LeftForward1");
        
    }

    private GameObject GetAppropriateSpawnPoint(string PointName)
    {
        GameObject RequiredSpawnPoint = new GameObject();
        foreach(GameObject spawnPoint in SpawnPoints)
        {
            if (spawnPoint.name == PointName) RequiredSpawnPoint = spawnPoint;
        }
        if (RequiredSpawnPoint == null) Debug.LogError("Mistake with spawnpoints name");
        return RequiredSpawnPoint;
    }

    private void AddFieldPosition(List<FieldPosition> fieldPositions,FieldPositionShortName shortName, string fullName)
    {
        fieldPositions.Add(new FieldPosition(shortName, fullName, GetAppropriateSpawnPoint(fullName)));
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
