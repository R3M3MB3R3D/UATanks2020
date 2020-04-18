using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]

public class TankAttack : MonoBehaviour
{
    //Creating variables to attach scripts and objects.
    public TankData data;
    public GameObject GunBall;
    public GameObject CannonBall;
    public AudioClip TankCannonSound;
    public AudioClip TankGunSound;
    private Transform tf;

    private void Awake()
    {
        //Attaching scripts and objects, note that
        //'this.gameObject' is not needed.
        data = GetComponent<TankData>();
        tf = gameObject.GetComponent<Transform>();
        //We attach GunBall and CannonBall later.
    }

    void Update()
    {
        //This is so that we never have more cannon ammo than we
        //are allowed to carry, this can happen with the ammo pickup.
        if (data.tankCannonAmmoCurrent > data.tankCannonAmmoMax)
        {
            data.tankCannonAmmoCurrent = data.tankCannonAmmoMax;
        }

        //This is so that we never have more gun ammo than we
        //are allowed to carry, this can happen with the ammo pickup.
        if (data.tankGunAmmoCurrent > data.tankGunAmmoMax)
        {
            data.tankGunAmmoCurrent = data.tankGunAmmoMax;
        }
    }

    public void FireCannon()
    {
        //If we have finished "reloading" *and* If we have more than 1 CannonBall according to 'data'.
        // '&&' is for "and", both parts must be true, you can use '||' for "or", only one must be true.
        if ((data.tankCannonCoolD >= data.tankCannonFireR) && (data.tankCannonAmmoCurrent > 0))
        {
            //Create a CannonBall, on our position, as an object.
            GameObject cannonBallCopy = Instantiate(CannonBall, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            //Create a variable here that we can attach to the newly instantiated CannonBall.
            CannonBallControl cannonBallScript;
            //attach that new script, object, and variable together.
            cannonBallScript = cannonBallCopy.GetComponent<CannonBallControl>();
            //Set 'shooter' inside of the new object to be this object.
            cannonBallScript.shooter = this.gameObject;
            //Begin "reloading".
            data.tankCannonCoolD = 0;
            //Account for the fired CannonBall.
            //instead of saying 'data.tankCannon = data.tankCannon - 1;'
            //we can say '-=' to deincrement, and '+=' to increment.
            data.tankCannonAmmoCurrent -= 1;
            //Firing a cannon makes a lot of noise.
            data.noiseLevel = 20;
            //And plays this audioclip
            AudioSource.PlayClipAtPoint(TankCannonSound, tf.position, 1.0f);
        }
    }

    public void FireGun()
    {
        //If we have finished "reloading" *and* we have more than 1 GunBall according to 'data'.
        if ((data.tankGunCoolD >= data.tankGunFireR) && (data.tankGunAmmoCurrent > 0))
        {
            //Create a GunBall, on our position, as an object.
            GameObject gunBallCopy = Instantiate(GunBall, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            //create a variable here that we can attach to the newly instantiated GunBall.
            GunBallControl gunBallScript;
            //Attach that new script, object, and variable together.
            gunBallScript = gunBallCopy.GetComponent<GunBallControl>();
            //Set shooter inside of the new object to be this object.
            gunBallScript.shooter = this.gameObject;
            //Begin "reloading".
            data.tankGunCoolD = 0;
            //Account for the fired GunBall.
            data.tankGunAmmoCurrent -= 1;
            //Firing a gun makes a lot of noise.
            data.noiseLevel = 10;
            //And plays this audioclip.
            AudioSource.PlayClipAtPoint(TankGunSound, tf.position, 1.0f);
        }
    }
}