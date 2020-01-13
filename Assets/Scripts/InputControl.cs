using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public enum InputScheme { WASD, IJKL };
    public InputScheme input = InputScheme.WASD;

    private TankData data;
    private TankMove move;

    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        move = gameObject.GetComponent<TankMove>();
    }

    private void Update()
    {
        switch (input)
        {
            case InputScheme.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    move.Move(data.forwardSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    move.Move(-data.forwardSpeed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    move.Rotate(data.rotateSpeed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    move.Rotate(-data.rotateSpeed);
                }
                break;
        }
    }
}