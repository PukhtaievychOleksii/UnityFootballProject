using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{ 
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //controlledFootballer.DefenseComp.AnaliseOponentsLocation();
        //controlledFootballer.DefenseComp.ChooseOponentForPressing();
        //controlledFootballer.DefenseComp.DoDefenseActions(); 
    }
    public AIController(FootballPlayer footballer)
    {
        controlledFootballer = footballer;
    }
    
}
