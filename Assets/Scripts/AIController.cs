using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
   /* public AIController(FootballPlayer player)
    {
        m_footballer = player;
    }*/
    // Start is called before the first frame update
    void Start()
    {
        m_footballer = gameObject.GetComponent<FootballPlayer>();  
    }

    // Update is called once per frame
    void Update()
    {
       // m_footballer.DefenseComp.GatherBall();
        
    }
    public AIController(FootballPlayer footballer)
    {
        m_footballer = footballer;
    }
    public AIController() { 
    
    }
}
