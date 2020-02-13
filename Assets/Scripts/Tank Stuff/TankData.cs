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
<<<<<<< HEAD
    //Here is where we ensure that we have variables to attach.
    public Transform tf;
    public TankMove move;
=======
    //Here is where we ensure that the scripts attach to the object.
>>>>>>> master
    public TankAttack attack;
    public TankLife life;
    public TankMove move;
    public Transform tf;

    //movement and rotation speed for tanks.
<<<<<<< HEAD
    public int forwardSpeed = 3;
    public int rotateSpeed = 150;
=======
    public float forwardSpeed;
    public float rotateSpeed;
>>>>>>> master

    //life and armor, armor is optional currently.
    //Need a max and current value for life.
    public float tankCurrentLife;
<<<<<<< HEAD
    public int tankArmor;
=======
    public float tankArmor;
    public int tankMaxLife;
>>>>>>> master

    //The bool is for toggling between tankGun and
    //tankCannon fire modes.  We need current and max
    //ammo counts, along with damage, cooldowns, and 
    //fire rates for each weapon.
    public bool weaponFire = true;

    public float tankGunDamage;
    public float tankGunAmmoCurrent;
    public int tankGunAmmoMax;

    public float tankCannonDamage;
<<<<<<< HEAD
    public int tankCannonAmmoMax;
=======
>>>>>>> master
    public float tankCannonAmmoCurrent;
    public int tankCannonAmmoMax;

    public float tankCannonCoolD;
    public float tankGunCoolD;
    public int tankCannonFireR;
    public int tankGunFireR;

    //Creating variables for sound and hearing, vision and
    //seeing, so that AI tanks can "see" and "hear" other tanks
    //(and walls) so that they can interact with them a little 
    //bit better.
    public float noiseLevel;
    public float hitBox;

    //Creating variables for score and enemies destroyed,
    //in order to create a leaderboard and other objects.
<<<<<<< HEAD
    public int lives;
=======
    public float lives;
>>>>>>> master
    public float tankScore;
    public int scoreValue;

    void Awake()
    {
<<<<<<< HEAD
        //'this.gameObject.' is not required, but it leaves
        //very little to the imagination as to what the
        //reference is for.  Here we attach those variables
        //to these scripts so that they will interact.
        tf = this.gameObject.GetComponent<Transform>();
        move = this.gameObject.GetComponent<TankMove>();
        attack = this.gameObject.GetComponent<TankAttack>();
        life = this.gameObject.GetComponent<TankLife>();
=======
        //'this.gameObject' is not required, and will not be
        //used past this script: tf - GetComponent<Transform>();
        //is all that is needed to attach scripts to objects.
        attack = this.gameObject.GetComponent<TankAttack>();
        life = this.gameObject.GetComponent<TankLife>();
        move = this.gameObject.GetComponent<TankMove>();
        tf = this.gameObject.GetComponent<Transform>();
>>>>>>> master

        forwardSpeed = 5;
        rotateSpeed = 100;

        tankMaxLife = 150;
        tankCurrentLife = 100;
        tankArmor = 0;

        tankGunDamage = 10;
        tankGunAmmoMax = 100;
        tankGunAmmoCurrent = 50;
        tankCannonDamage = 40;
        tankCannonAmmoMax = 10;
        tankCannonAmmoCurrent = 10;

        tankCannonFireR = 3;
        tankGunFireR = 1;

        lives = 3;
        tankScore = 0;
        scoreValue = 1;
    }

    void Update()
    {
        //These variables control how the tanks manage "reloading",
        //the cooldowns are what need to be changed in order to 
        //change firing rate of each weapon.
        tankCannonCoolD = tankCannonCoolD + Time.deltaTime;
        tankGunCoolD = tankGunCoolD + Time.deltaTime;
    }
}