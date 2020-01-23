using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all 'functions' for that 'data' come from other
//scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]

public class TankAttack : MonoBehaviour
{
    public GameObject CannonBall;
    public GameObject GunBall;
    public TankData tankData;

    private void Awake()
    {
        tankData = this.gameObject.GetComponent<TankData>();
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

    //created Variable for CBControl as CBScript to link variable to Cannonball
    //set function to create copies of the prefab and set shooter variable inside of CBControl
    //this SHOULD let the script know not to destroy the object firing the prefab.
    //at least, from CBControl when everything is set up properly.
    public void FireCannon()
    {
        if ((tankData.tankCannonCoolD >= tankData.tankCannonFireR) && (tankData.tankCannonAmmoCurrent > 0))
        {
            GameObject cannonBallCopy = Instantiate(CannonBall, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            CannonBallControl cannonBallScript;
            cannonBallScript = cannonBallCopy.GetComponent<CannonBallControl>();
            cannonBallScript.shooter = this.gameObject;
            tankData.tankCannonCoolD = 0;
            tankData.tankCannonAmmoCurrent -= 1;
        }
    }

    public void FireGun()
    {
        if ((tankData.tankGunCoolD > .25) && (tankData.tankGunAmmoCurrent > 0))
        {
            GameObject gunBallCopy = Instantiate(GunBall, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            GunBallControl gunBallScript;
            gunBallScript = gunBallCopy.GetComponent<GunBallControl>();
            gunBallScript.shooter = this.gameObject;
            tankData.tankGunCoolD = 0;
            tankData.tankGunAmmoCurrent -= 1;
            //we 'can' take the "this." out of the code but I prefer to keep it in
            //because we cant take "target." out when referrencing other scripts and objects.
        }
    }
}
