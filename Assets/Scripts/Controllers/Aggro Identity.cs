using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]
[RequireComponent(typeof(TankAttack))]
[RequireComponent(typeof(AIControl))]

public class AggroIdentity : MonoBehaviour
{
    public AIControl control;
    public TankData data;
    public TankMove move;
    public TankAttack attack;
    public Transform tf;

    public float stateEnterTime;

    //creating a list for AI functions
    public enum AIState {  Attack, Wander }
    public AIState aiState = AIState.Wander;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<AIControl>();
        data = GetComponent<TankData>();
        move = GetComponent<TankMove>();
        attack = GetComponent<TankAttack>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (aiState)
        {
            case AIState.Attack:
                AggroAttack();
                break;
            case AIState.Wander:
                AggroWander();
                break;
        }
    }

    void AggroAttack()
    {
        if (control.playerIsInRange() == true)
        {
            control.Attack();
            if (control.CanMove(data.forwardSpeed) == false)
            {
                control.Avoid();
            }
        }
        else
        {
            ChangeState(AIState.Wander);
        }
    }

    void AggroWander()
    {
        if (control.playerIsInRange() == false)
        {
            control.Wander();
            if (control.CanMove(data.forwardSpeed) == false)
            {
                control.Avoid();
            }
        }
        else
        {
            ChangeState(AIState.Attack);
        }
    }

    void ChangeState(AIState newState)
    {
        aiState = newState;
        stateEnterTime = Time.time;
    }
}