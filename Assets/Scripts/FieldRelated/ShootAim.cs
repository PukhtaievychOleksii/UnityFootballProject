using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum Vertical_InGates_Position
    {
        Down,
        Medium,
        Up,
        PoGorobcyam
    }
    public enum Horizontal_InGates_Position
    {
        Left,
        Right,
        Center
    }
    
    public class ShootAim
    {
        public Horizontal_InGates_Position HorizontalPosition;
        public Vertical_InGates_Position VerticalPosition;
        public GameObject AimObject;

        public ShootAim(Horizontal_InGates_Position HorizontalPosition,Vertical_InGates_Position VerticalPosition,GameObject AimObject)
        {
            this.HorizontalPosition = HorizontalPosition;
            this.VerticalPosition = VerticalPosition;
            this.AimObject = AimObject;
        }
    }

