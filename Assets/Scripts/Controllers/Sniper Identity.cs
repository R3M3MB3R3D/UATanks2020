using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperIdentity : MonoBehaviour
{
    public AIControl control;
    public TankData data;
    public TankMove move;
    public TankAttack attack;
    public Transform tf;

    public float stateEnterTime;

    //creating a list for AI functions
    public enum AIState { Attack, Wander, Flee, Rest }
    public AIState aiState = AIState.Wander;

    void Start()
    {
        control = GetComponent<AIControl>();
        data = GetComponent<TankData>();
        move = GetComponent<TankMove>();
        attack = GetComponent<TankAttack>();
        tf = GetComponent<Transform>();
    }

    void Update()
    {
        switch (aiState)
        {
            case AIState.Attack:
                SniperAttack();
                break;
            case AIState.Flee:
                SniperFlee();
                break;
            case AIState.Wander:
                SniperWander();
                break;
            case AIState.Rest:
                SniperRest();
                break;
        }
    }

    void SniperAttack()
    {
        if (control.playerIsInRange() == true)
        {
            move.RotateTowards(control.target.position, data.rotateSpeed);
            attack.FireCannon();
        }
        if (data.tankCurrentLife <= (data.tankMaxLife / 2))
        {
            ChangeState(AIState.Flee);
        }
        else
        {
            ChangeState(AIState.Rest);
        }
    }

    void SniperFlee()
    {
        control.Flee();
        if (control.CanMove(data.forwardSpeed) == false)
        {
            control.Avoid();
        }
        if (control.playerIsInRange() == false)
        {
            ChangeState(AIState.Rest);
        }
    }

    void SniperWander()
    {
        if (control.playerIsInRange() == false)
        {
            control.Wander();
            if (control.CanMove(data.forwardSpeed) == false)
            {
                control.Avoid();
            }
        }
        if (data.tankCurrentLife <= (data.tankMaxLife / 2))
        {
            ChangeState(AIState.Rest);
        }
        else
        {
            ChangeState(AIState.Attack);
        }
    }

    void SniperRest()
    {
        control.Rest();
        if (control.playerIsInRange() == true)
        {
            ChangeState(AIState.Flee);
        }
        if (data.tankCurrentLife >= (data.tankMaxLife * 0.80))
        {
            ChangeState(AIState.Wander);
        }
    }

    void ChangeState(AIState newState)
    {
        aiState = newState;
        stateEnterTime = Time.time;
    }
}
