using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Making all other common tank scripts required
//considering that they have functions that
//take all their information from here anyway.
[RequireComponent(typeof(TankAttack))]
[RequireComponent(typeof(TankLife))]
[RequireComponent(typeof(TankMove))]

public class TankData : MonoBehaviour
{
    //These scripts (and components) make use of the tanks' Data.
    //This script will be the one designers use to edit Data.
    public TankAttack attack;
    public TankLife life;
    public TankMove move;
    public Transform tf;

    //movement and rotation speed for tanks.
    public float forwardSpeed;
    public float rotateSpeed;

    //life and armor, armor is optional currently.
    //Need a max and current value for life.
    public float tankCurrentLife;
    public float tankArmor;
    public int tankMaxLife;

    //The bool is for toggling between tankGun and
    //tankCannon fire modes.  We need current and max
    //ammo counts, along with damage, cooldowns, and 
    //fire rates for each weapon.
    public bool weaponFire = true;

    public float tankGunDamage;
    public float tankGunAmmoCurrent;
    public int tankGunAmmoMax;

    public float tankCannonDamage;
    public float tankCannonAmmoCurrent;
    public int tankCannonAmmoMax;

    public float tankCannonCoolD;
    public float tankGunCoolD;
    public float tankCannonFireR;
    public int tankGunFireR;

    //Creating variables for sound and hearing, vision and
    //seeing, so that AI tanks can "see" and "hear" other tanks
    //(and walls) so that they can interact with them a little 
    //bit better.
    public float noiseLevel;
    public int noiseDetect;
    public int sightDetect;
    public int sightFOV;

    //Creating variables for score and enemies destroyed,
    //in order to create a leaderboard and other objects.
    public float lives;
    public float tankScore;
    public int scoreValue;

    void Awake()
    {
        //'this.gameObject.' is not required, but it leaves
        //very little to the imagination as to what the
        //reference is for.  Here we attach those variables
        //to these scripts so that they will interact.
        tf = this.gameObject.GetComponent<Transform>();
        move = this.gameObject.GetComponent<TankMove>();
        attack = this.gameObject.GetComponent<TankAttack>();
        life = this.gameObject.GetComponent<TankLife>();

        forwardSpeed = 5;
        rotateSpeed = 200;

        tankCurrentLife = 100;
        tankMaxLife = 100;
        tankArmor = 1;

        tankGunDamage = 2;
        tankGunAmmoCurrent = 50;
        tankGunAmmoMax = 100;
        tankCannonFireR = 3;

        tankCannonDamage = 50;
        tankCannonAmmoCurrent = 5;
        tankCannonAmmoMax = 10;
        tankGunFireR = 1;

        noiseDetect = 25;
        sightDetect = 60;
        sightFOV = 75;
        scoreValue = 1;
    }

    void Update()
    {
        //These variables control how the tanks manage "reloading",
        //the cooldowns are what need to be changed in order to 
        //change firing rate of each weapon.
        tankCannonCoolD = tankCannonCoolD + Time.deltaTime;
        tankGunCoolD = tankGunCoolD + Time.deltaTime;

        //We don't need to calculate noise if it's negative.
        if (noiseLevel >= 0)
        {
            noiseLevel = noiseLevel - Time.deltaTime;
        }
    }
}