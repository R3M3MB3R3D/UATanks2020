using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ensuring that our code works by making sure everything that works
//together are always together.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]
[RequireComponent(typeof(TankAttack))]
[RequireComponent(typeof(ChaseFlee))]
[RequireComponent(typeof(Patrol))]

public class AIControl : MonoBehaviour
{
    //attaching scripts together with their components.
    private TankMove motor;
    private TankData data;
    private TankAttack attack;
    private Transform tf;
    private ChaseFlee chaseFlee;
    private Patrol patrol;

    void Start()
    {
        motor = gameObject.GetComponent<TankMove>();
        data = gameObject.GetComponent<TankData>();
        attack = gameObject.GetComponent<TankAttack>();
    }

    private void Update()
    {
        
    }
}