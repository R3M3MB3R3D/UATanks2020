using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankAttack))]
[RequireComponent(typeof(TankMove))]

public class AISample : MonoBehaviour
{
    //Creating variables to attach to scripts and objects.
    public TankData data;
    public TankAttack attack;
    public TankMove move;

    //Creating variables we will need for this function
    public float stateEnterTime;
    public int healthRegen;
    public int ammoRegen;
    public int restTime;

    //Creating lists we will need for this function.
    public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Rest}
    public AIState aiState = AIState.Chase;
    public enum Identity { Inky, Blinky, Pinky, Clyde }
    public Identity identity;

    void Awake()
    {
        //Attaching scripts and objects.
        data = GetComponent<TankData>();
        attack = GetComponent<TankAttack>();
        move = GetComponent<TankMove>();

        //Setting some variables.
        healthRegen = 1;
        ammoRegen = 1;
        restTime = 30;
    }

    void Update()
    {
        switch (aiState)
        {
            case AIState.Chase:
                Chase();
                if (data.tankCurrentLife < (data.tankMaxLife * .5))
                {
                    ChangeState(AIState.CheckForFlee);
                }
                else if (playerIsInRange())
                {
                    ChangeState(AIState.ChaseAndFire);
                }
                break;
            case AIState.ChaseAndFire:
                Chase();
                attack.FireCannon();
                if (data.tankCurrentLife < (data.tankMaxLife * .5))
                {
                    ChangeState(AIState.CheckForFlee);
                }
                else if (!playerIsInRange())
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.CheckForFlee:
                if (playerIsInRange())
                {
                    ChangeState(AIState.Flee);
                }
                else 
                {
                    ChangeState(AIState.Rest);
                }
                break;
            case AIState.Flee:
                Flee();
                if (Time.time >= stateEnterTime + restTime)
                {
                    ChangeState(AIState.CheckForFlee);
                }
                break;
            case AIState.Rest:
                if (playerIsInRange())
                {
                    ChangeState(AIState.Flee);
                }
                else if (Mathf.Approximately(data.tankCurrentLife , data.tankMaxLife))
                {
                    ChangeState(AIState.Chase);
                }
                break;
        }

        switch (identity)
        {
            case Identity.Inky:
                Inky();
                break;
            case Identity.Blinky:
                Blinky();
                break;
            case Identity.Pinky:
                Pinky();
                break;
            case Identity.Clyde:
                Clyde();
                break;
        }
    }

    private bool playerIsInRange()
    {
        return true;
    }

    void ChangeState(AIState newState)
    {
        aiState = newState;
        stateEnterTime = Time.time;
    }

    void Chase()
    {

    }

    void ChaseAndFire ()
    {

    }

    void CheckForFlee()
    {

    }

    void Flee()
    {

    }

    void Rest()
    {
        //When an AI goes into rest they can regenerate health
        //and ammo for an amount of time up to designers.
        data.tankCurrentLife += healthRegen * Time.deltaTime;
        data.tankGunAmmoCurrent += ammoRegen * Time.deltaTime;
    }

    void Inky()
    {

    }

    void Blinky()
    {

    }

    void Pinky()
    {

    }

    void Clyde()
    {

    }
}
