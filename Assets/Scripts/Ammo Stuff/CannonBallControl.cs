using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallControl : MonoBehaviour
{
    //Timer and Force will be editable from the CannonBall
    //prefab, Shooter is automatically assigned from the 
    //TankAttack script and is necessary to calculate
    //damage dealt to target gameobjects.
    public int timer = 2;
    public int force = 2000;
    public GameObject shooter;

    void Start()
    {
        //the ", timer" part means that the CannonBall will
        //destroy itself after timer's duration has passed.
        //automatically gets -= time.deltaTime.
        Destroy(this.gameObject, timer);
        //This grabs the Rigidbody of the prefab CannonBall
        //and moves it forward at a rate of "force" above.
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }

    //Removed CollisionEnter and replaced it with TriggerEnter
    //"c" is the Collider of the object this CannonBall copy
    //interacts with.
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Equals("Environment"))
        {
            //destroys the Cannonball
            Destroy(this.gameObject);
        }

        //if collision target isnt shooter and isnt environment.
        if (shooter != c.gameObject && !c.gameObject.tag.Equals("Environment"))
        {
            //destroys the CannonBall
            Destroy(this.gameObject);
            //gets target health, and deals shooters damage values.
            c.gameObject.GetComponent<TankData>().tankCurrentLife -= shooter.GetComponent<TankData>().tankCannonDamage;
        }
    }
}