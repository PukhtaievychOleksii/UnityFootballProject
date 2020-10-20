using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent (typeof(FootballPlayer))]
public class PlayerController : Controller
{
    // public FootballPlayer footballer;
    private Game game;
    public List<KeyManager> ActiveKeyList = new List<KeyManager>();
    
    private void Awake()
    {
       
    }
    void Start()
    {
        //  footballer = GetComponent<FootballPlayer>();
        if (!IsM_FootballerFilled()) Debug.LogError("No Footballer In PlayerController");
        game = GetComponent<Game>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyManager key_manager in ActiveKeyList) key_manager.KeyManagerUpdate();
        KeyboardInput();
        SetKeyManagers();//In update because in Start MoveComponent = null

    }

    private void KeyboardInput()
    {
        
        MoveFootballer(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.J)) m_footballer.AtackComp.PassBall(GetTheClosestTeamMate());
    }
    
    public void MoveFootballer(float AxisVert, float AxisHor)
    {
        if (!IsM_FootballerFilled()) return;
        m_footballer.MoveComp.SetMovingDataByAxis(AxisVert, AxisHor);
       
    }
   
    private FootballPlayer GetTheClosestTeamMate()
    {
        FootballPlayer thatPlayer = new FootballPlayer();
        float distance = 1000;
        foreach(FootballPlayer footballer in game.footballers)
        {
            float our_distance = (m_footballer.transform.position - footballer.transform.position).magnitude;

            if (footballer != m_footballer && our_distance < distance )
            {
                distance = our_distance;
                thatPlayer = footballer;
            }
        }
        return thatPlayer;
    }

   

    
    private void SetKeyManagers()
    {
       
        if (!IsM_FootballerFilled() || ActiveKeyList.Count > 0) return;
      
        AddKeyManager(new KeyManager(KeyCode.Space, m_footballer.MoveComp.Jump));
        AddKeyManager(new KeyManager(KeyCode.LeftShift, m_footballer.MoveComp.StartRunning, m_footballer.MoveComp.FinishRunning));
        AddKeyManager(new KeyManager(KeyCode.G, m_footballer.AtackComp.Shoot));
    }

    public void ResetKeyManagersForNewFootballer()
    {
        ActiveKeyList.Clear();
        SetKeyManagers();
    }

    private void AddKeyManager(KeyManager keymanager)
    {
        ActiveKeyList.Add(keymanager);
    }
    
    }

