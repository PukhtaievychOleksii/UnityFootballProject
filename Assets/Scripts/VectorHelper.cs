using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public   class  VectorHelper:MonoBehaviour 
{
    public GameObject player;
    public static float standartPlayerYPos; 
    // Start is called before the first frame update
    void Start()
    {
       
        standartPlayerYPos = player.transform.position.y;        
    }

    // Update is called once per frame
    

    
}
