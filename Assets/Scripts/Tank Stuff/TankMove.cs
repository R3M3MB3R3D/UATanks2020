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

    //Creating a new function for AI movement until I clean up
    //code and simplify everything.
    public void AIMove(float speed)
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

    //Same thing with the AI rotation, until I clean up the code
    //and simplifying things later.
    public void AIRotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * speed * Time.deltaTime;

        tf.Rotate(rotateVector, relativeTo: Space.Self);
    }
}