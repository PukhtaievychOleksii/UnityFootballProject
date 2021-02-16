using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TacticsManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> TacticsPrefabVariations;
    public List<string> FieldPositionNames;
    public Game Game;
    
    void Awake()
    {
        
        Game = GetComponent<Game>();
    }



    public Tactic GetTacticObject(string Name, FieldSide fieldSide)
    {
        GameObject TacticPrefab = null;
        GameObject AppropriateTactiSpawnPoint = null;

        foreach (GameObject prefab in TacticsPrefabVariations)
        {
            if (prefab.name == Name) TacticPrefab = prefab;
        }
        if (TacticPrefab == null) { Debug.LogError("WrongTacticName"); }

        AppropriateTactiSpawnPoint = GetAppropriateSpawnPoint(fieldSide, ref TacticPrefab);

        GameObject TacticObject = Instantiate(TacticPrefab, AppropriateTactiSpawnPoint.transform.position,TacticPrefab.transform.rotation);
        Tactic tactic = new Tactic(TacticObject, Name,fieldSide);
        return tactic;

    }

  private GameObject GetAppropriateSpawnPoint(FieldSide fieldSide,ref GameObject TacticPrefab)
    {
        GameObject appropriateTactiSpawnPoint = null;
        if (fieldSide == FieldSide.Left)
        {
            appropriateTactiSpawnPoint = Game.Field.transform.Find("LeftTacticsSpawnPoint").gameObject;
            TacticPrefab.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else
        {
            appropriateTactiSpawnPoint = Game.Field.transform.Find("RightTacticsSpawnPoint").gameObject;
            // TacticPrefab.transform.Rotate(0, 180,0);
            TacticPrefab.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        return appropriateTactiSpawnPoint;
    }
}
