using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]

public class InputControl : MonoBehaviour
{
    //Creating variables to attach to scripts and objects
    private TankData data;
    private TankMove move;
    private TankAttack attack;
    
    //Creating lists we will need for this function.
    public enum InputScheme { WASD, IJKL };
    public InputScheme input;
    //ToDo: Make it so that the control scheme can be changed by the player.

    void Awake()
    {
        //Attaching scripts and objects.
        data = gameObject.GetComponent<TankData>();
        move = gameObject.GetComponent<TankMove>();
        attack = gameObject.GetComponent<TankAttack>();
    }

    private void Update()
    {
        switch (input)
        {
            case InputScheme.WASD:
                //When "W" is pressed.
                if (Input.GetKey(KeyCode.W))
                {
                    //Move function called, Object moves forward.
                    move.Move(data.forwardSpeed);
                }
                //When "S" is pressed.
                else if (Input.GetKey(KeyCode.S))
                {
                    //Move function called, Object moves back.
                    move.Move(-data.forwardSpeed);
                }
                //When neither is being pressed, audio should stop.
                else
                {
                    move.tankNoiseSource.Stop();
                }
                //When "D" is pressed.
                if (Input.GetKey(KeyCode.D))
                {
                    //Rotate function called, Object turns right.
                    move.Rotate(data.rotateSpeed);
                }
                //When "A" is pressed.
                if (Input.GetKey(KeyCode.A))
                {
                    //Rotate function called, Object turns left.
                    move.Rotate(-data.rotateSpeed);
                }
                //When "Q" is pressed once.
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //If weaponFire is True.
                    if (data.weaponFire == true)
                    {
                        //FireCannon function called.
                        attack.FireCannon();
                    }
                    //If weaponFire is False.
                    else
                    {
                        //FireGun function called.
                        attack.FireGun();
                    }
                }
                //When "E" is pressed once.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //change weaponFire to the opposite of what it is now (t/f).
                    data.weaponFire = !data.weaponFire;
                }
                break;

            case InputScheme.IJKL:
                //When "I" is pressed.
                if (Input.GetKey(KeyCode.I))
                {
                    //Move function called, Object moves forward.
                    move.Move(data.forwardSpeed);
                }
                //When "K" is pressed.
                else if (Input.GetKey(KeyCode.K))
                {
                    //Move function called, Object moves back.
                    move.Move(-data.forwardSpeed);
                }
                //When neither is being pressed, audio should stop.
                else
                {
                    move.tankNoiseSource.Stop();
                }
                //When "L" is pressed.
                if (Input.GetKey(KeyCode.L))
                {
                    //Rotate function called, Object turns right.
                    move.Rotate(data.rotateSpeed);
                }
                //When "J" is pressed.
                if (Input.GetKey(KeyCode.J))
                {
                    //Rotate function called, Object turns left.
                    move.Rotate(-data.rotateSpeed);
                }
                //When "U" is pressed once.
                if (Input.GetKeyDown(KeyCode.U))
                {
                    //If weaponFire is True.
                    if (data.weaponFire == true)
                    {
                        //FireCannon function called.
                        attack.FireCannon();
                    }
                    //If weaponFire is False.
                    else
                    {
                        //FireGun function called.
                        attack.FireGun();
                    }
                }
                //When "O" is pressed once.
                if (Input.GetKeyDown(KeyCode.O))
                {
                    //change weaponFire to the opposite of what it is now (t/f).
                    data.weaponFire = !data.weaponFire;
                }
                break;
        }
    }
}