using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]
[RequireComponent(typeof(TankAttack))]
[RequireComponent(typeof(AIControl))]

public class GuardIdentity : MonoBehaviour
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
                GuardAttack();
                break;
            case AIState.Wander:
                GuardWander();
                break;
            case AIState.Flee:
                GuardFlee();
                break;
            case AIState.Rest:
                GuardRest();
                break;
        }
    }

    void GuardAttack()
    {
        if (control.playerIsInRange() == true)
        {
            control.Attack();
            if (control.CanMove(data.forwardSpeed) == false)
            {
                control.Avoid();
            }
        }
        if (data.tankCurrentLife <= (data.tankMaxLife / 2))
        {
            ChangeState(AIState.Flee);
        }
        else
        {
            ChangeState(AIState.Wander);
        }
    }

    void GuardWander()
    {
        if (control.playerIsInRange() == false)
        {
            control.Wander();
            if (control.CanMove(data.forwardSpeed) == false)
            {
                control.Avoid();
            }
        }
        if (data.tankCurrentLife <= (data.tankMaxLife / 3))
        {
            ChangeState(AIState.Rest);
        }
        else
        {
            ChangeState(AIState.Attack);
        }
    }

    void GuardFlee()
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

    void GuardRest()
    {
        control.Rest();
        if (control.playerIsInRange() == true)
        {
            ChangeState(AIState.Flee);
        }
        if (data.tankCurrentLife >= (data.tankMaxLife * 0.50))
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
