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
    public TankData tankData;
    public GameObject GunBall;
    public GameObject CannonBall;

    private void Awake()
    {
        //Attaching scripts and objects.
        tankData = this.gameObject.GetComponent<TankData>();
        //We attach GunBall and CannonBall later.
    }

    void Update()
    {
        //This is so that we never have more cannon ammo than we
        //are allowed to carry, this can happen with the ammo pickup.
        if (tankData.tankCannonAmmoCurrent > tankData.tankCannonAmmoMax)
        {
            tankData.tankCannonAmmoCurrent = tankData.tankCannonAmmoMax;
        }
        //This is so that we never have more gun ammo than we
        //are allowed to carry, this can happen with the ammo pickup.
        if (tankData.tankGunAmmoCurrent > tankData.tankGunAmmoMax)
        {
            tankData.tankGunAmmoCurrent = tankData.tankGunAmmoMax;
        }
    }

    public void FireCannon()
    {
        //If we have finished "reloading" *and* If we have more than 1 CannonBall according to TankData.
        if ((tankData.tankCannonCoolD >= tankData.tankCannonFireR) && (tankData.tankCannonAmmoCurrent > 0))
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
            tankData.tankCannonCoolD = 0;
            //Account for the fired CannonBall.
            tankData.tankCannonAmmoCurrent -= 1;
        }
    }

    public void FireGun()
    {
        //If we have finished "reloading" *and* we have more than 1 GunBall according to TankData.
        if ((tankData.tankGunCoolD >= tankData.tankGunFireR) && (tankData.tankGunAmmoCurrent > 0))
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
            tankData.tankGunCoolD = 0;
            //Account for the fired GunBall
            tankData.tankGunAmmoCurrent -= 1;
        }
    }
}