using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{

    //Creating CharacterController and Transform components,
    //The Transform is a small optimization of code for the functions.
    private CharacterController characterController;
    private Transform tf;

    //Attaching the characterController and the Transform components,
    //making sure that they are used when commands come in.
    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
    }

    //Referencing the Function "Move" should originate from
    //InputControl or AIControl.  As it stands anyway.
    public void Move(float speed)
    {
        Vector3 speedVector = tf.forward * speed;

        characterController.SimpleMove(speedVector);
    }

    //Referencing the Function "Rotate" should originate from
    //InputControl or AIControl.  As it stands anyway.
    public void Rotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * speed * Time.deltaTime;

        tf.Rotate(rotateVector, relativeTo: Space.Self);
    }

    public bool RotateTowards(Vector3 target, float speed)
    {
        //creating a variable to see the difference between you and your target.
        Vector3 vectorToTarget = target - tf.position;
        
        //Quaternions allow for smoother actions, especially from AI targets.
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);

        //if our rotation is equal to our target's, then we dont need to rotate towards anymore.
        if (targetRotation == tf.rotation)
        {
            return false;
        }
        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, speed * Time.deltaTime);
        return true;
    }
}