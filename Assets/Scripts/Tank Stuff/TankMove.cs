using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TankMove : MonoBehaviour
{
    //Creating CharacterController and Transform components,
    //The Transform is a small optimization of code for the functions.
    private CharacterController characterController;
    private Transform tf;

    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {

    }

    public void Move(float speed)
    {
        Vector3 speedVector = tf.forward * speed;

        characterController.SimpleMove(speedVector);
    }

    public void Rotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * speed * Time.deltaTime;

        tf.Rotate(rotateVector, relativeTo: Space.Self);
    }
}