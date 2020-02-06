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
    //Here is where we ensure that we have variables to attach.
    public Transform tf;
    public TankMove move;
    public TankAttack attack;
    public TankLife life;

    //movement and rotation speed for tanks.
    public int forwardSpeed = 3;
    public int rotateSpeed = 150;

    //life and armor, armor is optional currently.
    //Need a max and current value for life.
    public int tankMaxLife;
    public float tankCurrentLife;
    public int tankArmor;

    //The bool is for toggling between tankGun and
    //tankCannon fire modes.  We need current and max
    //ammo counts, along with damage, cooldowns, and 
    //fire rates for each weapon.
    public bool weaponFire = true;

    public float tankGunDamage;
    public int tankGunAmmoMax;
    public float tankGunAmmoCurrent;

    public float tankCannonDamage;
    public int tankCannonAmmoMax;
    public float tankCannonAmmoCurrent;

    public int tankCannonFireR;
    public int tankGunFireR;
    public float tankCannonCoolD;
    public float tankGunCoolD;

    //Creating variables for sound and hearing, vision and
    //seeing, so that AI tanks can "see" and "hear" other tanks
    //(and walls) so that they can interact with them a little 
    //bit better.
    public float noiseLevel;
    public float hitBox;

    //Creating variables for score and enemies destroyed,
    //in order to create a leaderboard and other objects.
    public int lives;
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