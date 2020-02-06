using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]
[RequireComponent(typeof(TankAttack))]

public class AIControl : MonoBehaviour
{
    //Creating variables to attach to scripts and objects.
    public TankMove move;
    public TankData data;
    public TankAttack attack;
    public Transform tf;

    void Awake()
    {
        //Attaching scripts and objects.
        move = gameObject.GetComponent<TankMove>();
        data = gameObject.GetComponent<TankData>();
        attack = gameObject.GetComponent<TankAttack>();
    }

    private void Update()
    {
        
    }
}