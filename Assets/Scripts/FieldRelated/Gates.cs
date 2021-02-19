using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public GameObject gateObject;
    public GameObject UpLeftShootAim;
    public GameObject UpCenterShootAim;
    public GameObject UpRightShootAim;
    public GameObject MediumLeftShootAim;
    public GameObject MediumCenterShootAim;
    public GameObject MediumRightShootAim;
    public GameObject DownRightShootAim;
    public GameObject DownCenterShootAim;
    public GameObject DownLeftShootAim;
    public GameObject PoGorobcyamShootAim;
    public List<ShootAim> ShootAims = new List<ShootAim>();
   



    void Start()
    {
        gateObject = gameObject;
        RegisterShootAim(UpCenterShootAim,Vertical_InGates_Position.Up,Horizontal_InGates_Position.Center);
        RegisterShootAim(UpLeftShootAim,Vertical_InGates_Position.Up,Horizontal_InGates_Position.Left);
        RegisterShootAim(UpRightShootAim,Vertical_InGates_Position.Up,Horizontal_InGates_Position.Right);
        RegisterShootAim(MediumCenterShootAim,Vertical_InGates_Position.Medium,Horizontal_InGates_Position.Center);
        RegisterShootAim(MediumLeftShootAim,Vertical_InGates_Position.Medium,Horizontal_InGates_Position.Left);
        RegisterShootAim(MediumRightShootAim,Vertical_InGates_Position.Medium,Horizontal_InGates_Position.Right);
        RegisterShootAim(DownCenterShootAim,Vertical_InGates_Position.Down,Horizontal_InGates_Position.Center);
        RegisterShootAim(DownLeftShootAim,Vertical_InGates_Position.Down,Horizontal_InGates_Position.Left);
        RegisterShootAim(DownRightShootAim,Vertical_InGates_Position.Down,Horizontal_InGates_Position.Right);
        RegisterShootAim(PoGorobcyamShootAim,Vertical_InGates_Position.PoGorobcyam,Horizontal_InGates_Position.Center);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void RegisterShootAim(GameObject AimObject,Vertical_InGates_Position verticalPosition, Horizontal_InGates_Position horizontalPosition)
    {
        ShootAim shootAim = new ShootAim(horizontalPosition, verticalPosition, AimObject);
        ShootAims.Add(shootAim);
    }

    public ShootAim GetApropriateShootAim(Vertical_InGates_Position verticalAimPosition,Horizontal_InGates_Position horizontalGatesPosition)
    {
        ShootAim shootAim = ShootAims[0];
        foreach(ShootAim aim in ShootAims)
        {
            if (aim.HorizontalPosition == horizontalGatesPosition && aim.VerticalPosition == verticalAimPosition) shootAim = aim;
        }
        return shootAim;
    }
}
