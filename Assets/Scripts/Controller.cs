using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent (typeof(FootballPlayer))]
public class Controller : MonoBehaviour
{
    public GameObject ronaldo;
    private FootballPlayer footballer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        footballer = GetComponent<FootballPlayer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {


        KeyboardInput();
    }

    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            footballer.MoveToPoint(ronaldo.transform.position);
            Console.WriteLine("Agov");
        }
        if (Input.GetKeyDown(KeyCode.Space)) footballer.Jump();
          footballer.MoveVertical(Input.GetAxis("Vertical"));
          footballer.MoveHorizontal(Input.GetAxis("Horizontal"));
     //   if (Input.GetKeyDown(KeyCode.W)) footballer.MoveUp();
        //if (Input.GetKeyDown(KeyCode.D)) footballer.MoveRight();
         // if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.S) && Input.GetKeyU(KeyCode.D)) footballer.HeroStop();
    }

    private void OnDrawGizmos()
    {
        
    }
}
