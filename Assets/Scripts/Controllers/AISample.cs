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
    public int gunRegen;
    public int cannonRegen;
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
        healthRegen = 10;
        gunRegen = 3;
        cannonRegen = 1;
        restTime = 5;
    }

    void Update()
    {
        switch (aiState)
        {
            case AIState.Chase:
                //Execute function, 'Chase' is described below.
                Chase();
                //Check if lower than half max heath.
                if (data.tankCurrentLife < (data.tankMaxLife * .5))
                {
                    //ChangeState is a function below
                    ChangeState(AIState.CheckForFlee);
                }
                // TODO: implement 'sight' and 'hearing' so the AI can check this.
                else if (playerIsInRange())
                {
                    //ChageState also tracks when the AI switches state.
                    ChangeState(AIState.ChaseAndFire);
                }
                break;

            case AIState.ChaseAndFire:
                Chase();
                attack.FireCannon();
                //Check if lower than half max health.
                if (data.tankCurrentLife < (data.tankMaxLife * .5))
                {
                    ChangeState(AIState.CheckForFlee);
                }
                //If 'player' is NOT in range.
                else if (!playerIsInRange())
                {
                    ChangeState(AIState.Chase);
                }
                break;

            case AIState.CheckForFlee:
                //If 'player' IS in range.
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
                //When the time between states has passed.
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
                //Mathf.Approximately compares the 2 integers and allows for answers
                //that are not exactly the same, but close enough to be called good.
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
        data.tankGunAmmoCurrent += gunRegen * Time.deltaTime;
        data.tankCannonAmmoCurrent += cannonRegen * Time.deltaTime;
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