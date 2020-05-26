using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent (typeof(FootballPlayer))]
public class PlayerController : Controller
{
   // public FootballPlayer footballer;
    public PlayerController(FootballPlayer player)
    {
        m_footballer = player;
    }
    
    void Start()
    {
        //  footballer = GetComponent<FootballPlayer>();
        if (m_footballer == null) Debug.LogError("No Footballer In PlayerController");
        
    }

    // Update is called once per frame
    void Update()
    {


        KeyboardInput();
    }

    private void KeyboardInput()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)) m_footballer.MoveComp.Jump();
        MoveFootballer(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.LeftShift) ) m_footballer.MoveComp.StartRunning();
        if (Input.GetKeyUp(KeyCode.LeftShift)) m_footballer.MoveComp.FinishRunning();
    
    }
    
    public void MoveFootballer(float AxisVert, float AxisHor)
    {
        m_footballer.MoveComp.SetMovingDataByAxis(AxisVert, AxisHor);
       
    }
    private bool MovingButtonsArePressed()
    {
        if (Input.GetKeyDown(KeyCode.W)) return true;
        if (Input.GetKeyDown(KeyCode.A)) return true;
        if (Input.GetKeyDown(KeyCode.S)) return true;
        if (Input.GetKeyDown(KeyCode.D)) return true;
        
        return false;
    }
}
