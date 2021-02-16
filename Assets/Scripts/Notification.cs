using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
   public  float LifeExpectancy;
   public  float LifeTime = 0;
   public Text Text;
   public GameObject TextObject;

    public void SetNotificationParams(string notisficationText, float lifeExpectancy)
    {
        Text = TextObject.GetComponent<Text>();
        Text.text = notisficationText;
        LifeExpectancy = lifeExpectancy;
    }

    
    public void Start()
    {
        
    }
    public void Update()
    {
        LifeTime += Time.deltaTime;
        ChangeColorByTime();
    }
    private void ChangeColorByTime()
    {
        float AColorParam = 1 - LifeTime / LifeExpectancy;
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, AColorParam); 
    }

    private void CheckDestroy()
    {
      //  if(LifeTime > LifeExpectancy) TextObject.gameObject
    }

}
