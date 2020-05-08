using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class FootballPlayer : MonoBehaviour
{

    
    public Vector3 m_WorldVelocity;
    public float m_speed = 0;
    private Vector3? m_destination;
    private Animator m_animator;
    public string RunningParamName;
    public string JumpingParamName;
    private bool IsMoving = false;






    // Start is called before the first frame update

    void Start()
    {
        
        
        m_destination = null;
        m_animator = GetComponent<Animator>();
        m_animator.SetBool(RunningParamName, false);
       
        
        
        
    }

    public void MoveToPoint(Vector3 destination)
    {
        m_destination = destination;
        m_animator.SetBool(RunningParamName, true);
        m_WorldVelocity = (m_destination - transform.position).Value.normalized;

        // footballer.transform.LookAt(destination);
        //footballer.transform.Translate(destination);


    }
    // Update is called once per frame
    void Update()
    {
       // if (m_destination != null)
        GoTo();
        CheckHeroStop();
            
        
    }

    private void GoTo()
    {
        //Vector3 localVelocity = transform.InverseTransformPoint(m_WorldVelocity);
        
        transform.position += m_WorldVelocity * m_speed / 60;
        /*   float distance = Vector3.Distance(transform.position, m_destination.Value);
           if (distance > 3)
               transform.position += m_velocity * m_speed / 60;
           else
           {
               m_velocity = new Vector3(0, 0, 0);
               m_animator.SetBool(RunningParamName, false);
           }*/
    }

    public void MoveVertical(float AxisVert)
    {
        
        m_speed =Mathf.Abs(AxisVert * 5);
        if(Mathf.Abs(AxisVert) >= 0.5)
        {
            m_animator.SetBool(RunningParamName, true);
            if (AxisVert > 0)
            {
                transform.rotation = Game.RotetionDirections["Down"];
                m_WorldVelocity = (transform.forward).normalized;
            }
            if (AxisVert < 0)
            {
                transform.rotation = Game.RotetionDirections["Up"];
                m_WorldVelocity = (transform.forward).normalized;

            }
        }
        
        
    }

    public void MoveHorizontal(float AxisHor)
    {
        if (m_speed == 0)
        {
            m_speed = Mathf.Abs(AxisHor * 5);
        }
        if (Mathf.Abs(AxisHor) > 0)
        {
            m_animator.SetBool(RunningParamName, true);
            if (AxisHor > 0)
            {
                transform.rotation = Game.RotetionDirections["Right"];
                m_WorldVelocity = (transform.forward).normalized;
            }
            if (AxisHor < 0)
            {
                transform.rotation = Game.RotetionDirections["Left"];
                m_WorldVelocity = (transform.forward).normalized;

            }
        }

    }
    

    public void CheckHeroStop()
    {
        if (m_speed < 1)
        {
            m_animator.SetBool(RunningParamName, false);
            m_speed = 0;
        }
    }


    public void Jump()
    {
        m_animator.SetTrigger(JumpingParamName);
        m_animator.SetTrigger(JumpingParamName);
    }
}
