using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    //creating lists for Input schemes and controls,
    //we will have WASD and IJKL style controls.
    public enum InputScheme { WASD, IJKL };
    public InputScheme input = InputScheme.WASD;

    //creating variables
    private TankData data;
    private TankMove move;
    private TankAttack attack;

    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        move = gameObject.GetComponent<TankMove>();
        attack = gameObject.GetComponent<TankAttack>();
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
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (data.weaponFire == true)
                    {
                        attack.FireCannon();
                    }
                    else
                    {
                        attack.FireGun();
                    }
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    data.weaponFire = !data.weaponFire;
                }
                break;
        }

        switch (input)
        {
            case InputScheme.IJKL:
                if (Input.GetKey(KeyCode.I))
                {
                    move.Move(data.forwardSpeed);
                }
                if (Input.GetKey(KeyCode.K))
                {
                    move.Move(-data.forwardSpeed);
                }
                if (Input.GetKey(KeyCode.J))
                {
                    move.Rotate(data.rotateSpeed);
                }
                if (Input.GetKey(KeyCode.L))
                {
                    move.Rotate(-data.rotateSpeed);
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    if (data.weaponFire == true)
                    {
                        attack.FireCannon();
                    }
                    else
                    {
                        attack.FireGun();
                    }
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    data.weaponFire = !data.weaponFire;
                }
                break;
        }
    }
}