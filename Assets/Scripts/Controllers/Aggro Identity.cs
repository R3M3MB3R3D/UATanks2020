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
    public enum AIState { Chase, Attack, CheckForFlee, Flee, Rest, Powerup, Avoid }
    public AIState aiState = AIState.Rest;

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
            case AIState.Chase:
                control.Chase();
                //Check if lower than half max health.
                if (data.tankCurrentLife < (data.tankMaxLife * .5))
                {
                    ChangeState(AIState.CheckForFlee);
                }
                //If 'player' is NOT in range.
                else if (!control.playerIsInRange())
                {
                    ChangeState(AIState.Chase);
                }
                else if (control.CanMove(data.forwardSpeed) == false)
                {
                    ChangeState(AIState.Avoid);
                }
                break;
            case AIState.Attack:
                control.Chase();
                attack.FireCannon();
                //Check if lower than half max heath.
                if (data.tankCurrentLife < (data.tankMaxLife * .5))
                {
                    //ChangeState is a function below
                    ChangeState(AIState.CheckForFlee);
                }
                // TODO: implement 'sight' and 'hearing' so the AI can check this.
                else if (control.playerIsInRange())
                {
                    //ChageState also tracks when the AI switches state.
                    ChangeState(AIState.Attack);
                }
                else if (control.CanMove(data.forwardSpeed) == false)
                {
                    ChangeState(AIState.Avoid);
                }
                break;
            case AIState.Avoid:
                control.Avoid();
                if (control.playerIsInRange())
                {
                    ChangeState(AIState.Chase);
                }
                else
                {
                    ChangeState(AIState.Attack);
                }
                break;
        }
    }

    void ChangeState(AIState newState)
    {
        aiState = newState;
        stateEnterTime = Time.time;
    }
}
