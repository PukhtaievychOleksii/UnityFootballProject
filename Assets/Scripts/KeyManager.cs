using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager 
{
    public KeyCode key;
    private Action actionVstart;//Doesn't get anything,
    private Action actionVfinish;
    private Action<float> actionF;//Get float data
    private Action<FootballPlayer> actionFP;
    public bool IsKeyPressed;
    private float pressionTime;
    public  KeyManager(KeyCode key,Action actionVstart)
    {
        this.key = key;
        this.actionVstart = actionVstart;
    }
   public  KeyManager(KeyCode key,Action<float> actionF)
    {
        this.key = key;
        this.actionF = actionF;
    }
    public KeyManager(KeyCode key,Action actionVstart,Action actionVfinish)
    {
        this.key = key;
        this.actionVstart = actionVstart;
        this.actionVfinish = actionVfinish;
    }
    public KeyManager(KeyCode key,Action<Type> action)
    {

    }
    public void KeyManagerUpdate()
    {
        CalculateTime();
        if (Input.GetKeyDown(key)) 
        {
            IsKeyPressed = true;
            actionVstart?.Invoke();

        }
        if (Input.GetKeyUp(key))
        {
            IsKeyPressed = false;
            actionF?.Invoke(pressionTime);
            actionVfinish?.Invoke();
            pressionTime = 0;
        }
    }

    private void CalculateTime()
    {
        if (IsKeyPressed) pressionTime += Time.deltaTime;
    }
}
