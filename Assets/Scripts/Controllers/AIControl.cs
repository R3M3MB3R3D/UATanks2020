using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ensuring that our code works by making sure everything that works
//together are always together.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]

public class AIControl: MonoBehaviour
{
    private TankMove motor;
    private TankData data;
    private TankAttack attack;

    void Start()
    {
        motor = gameObject.GetComponent<TankMove>();
        data = gameObject.GetComponent<TankData>();
        attack = gameObject.GetComponent<TankAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        motor.AIMove(data.forwardSpeed);
        motor.AIRotate(data.rotateSpeed);
        attack.FireCannon();
    }
}