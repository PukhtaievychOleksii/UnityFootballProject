using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogPanel : MonoBehaviour
{
   public  List<Notification> Notifications = new List<Notification>();
    public GameObject TextPrefab;
    private RectTransform PrefabRectTransform;
    public Transform DefaultNotificationTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        PrefabRectTransform = TextPrefab.GetComponent<RectTransform>();
      //  AddGoalNotification(new ScoreGoalParams(),5);
     //   AddGoalNotification("Salah", "Barca");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGoalNotification(ScoreGoalParams goalParams,float LifeExpectancy)
    {
        //TODO : Fix that shit
        string ScorerName = goalParams.ScoredPlayer?.VariableParams.Name ?? "Alexey Pukhtayevich";
        string ScoredTeamInName = goalParams.ScoredInGatesTeam?.TeamParameters.Name ?? "Dyrka";
        string NotificationText = ScorerName + " scored a goal to " + ScoredTeamInName;
        Notification notification = this.gameObject.AddComponent<Notification>();
        Notifications.Add(notification);
        SpawnNotification(TextPrefab,notification);
        notification.SetNotificationParams(NotificationText, LifeExpectancy);
        
        
    }

    private Transform GetAppropriateTransform(Notification notification)
    {
        int TextGameObjectsAbove = 0;
        Transform Transform = DefaultNotificationTransform;
        foreach(Notification notif in Notifications)
        {
            if (notif == notification) break;
            if (notif.TextObject != null) TextGameObjectsAbove -= 1;
        }
        DefaultNotificationTransform.position = new Vector3(DefaultNotificationTransform.position.x, DefaultNotificationTransform.position.y + PrefabRectTransform.rect.height * TextGameObjectsAbove,DefaultNotificationTransform.position.z);
        return Transform; 
    }

    public void SpawnNotification(GameObject TextPrefab,Notification notification)
    {
        notification.TextObject = Instantiate(TextPrefab, GetAppropriateTransform(notification));
    }
}
