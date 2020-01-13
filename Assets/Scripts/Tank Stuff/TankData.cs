using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //These scripts (and components) make use of the tanks' Data.
    //This script will be the one designers use to edit Data.
    public Transform tf;
    public TankMove move;
    public TankAttack attack;
    public TankLife life;

    //movement and rotation speed for tanks.
    public float forwardSpeed = 3;
    public float rotateSpeed = 150;
}