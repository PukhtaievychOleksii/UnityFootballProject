using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
   public  Dictionary<string, AtackTactic> AtackTactics =new Dictionary<string, AtackTactic>();

   public List<AtackTactic> AtackTactic;
   public  Dictionary<string, DefenseTactic> DefenseTactics;
    void Start()
    {

        AddFirstAtackTactic(); 
    }

    private void AddFirstAtackTactic()
    {
        AtackTactic atackTactic = new AtackTactic(SpawnPoints,"4-3-3");
        atackTactic.AddFieldPosition(FieldPositionShortName.LF1, "LeftForward1");
        atackTactic.AddFieldPosition(FieldPositionShortName.GK2, "GoalKeeper2");
        AtackTactics.Add(atackTactic.Name, atackTactic);
    }

    void Update()
    {
        
    }
}
