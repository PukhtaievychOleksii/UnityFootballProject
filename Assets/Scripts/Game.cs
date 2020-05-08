using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static Dictionary<string, Quaternion> RotetionDirections = new Dictionary<string, Quaternion>();
    // Start is called before the first frame update
    private void Awake()
    {

        RotetionDirections.Add("Up",Quaternion.Euler(0,90,0));
        RotetionDirections.Add("Down",Quaternion.Euler(0, 270, 0));
        RotetionDirections.Add("Left",Quaternion.Euler(0, 180, 0));
        RotetionDirections.Add("Right",Quaternion.Euler(0, 0, 0));
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
