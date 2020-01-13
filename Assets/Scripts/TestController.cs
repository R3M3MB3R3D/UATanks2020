using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ensuring that our code works by making sure everything that works
//together are always together.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]

public class TestController : MonoBehaviour
{
    private TankMove motor;
    private TankData data;

    void Start()
    {
        motor = gameObject.GetComponent<TankMove>();
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        motor.Move(data.forwardSpeed);
        motor.Rotate(data.rotateSpeed);
    }
}
