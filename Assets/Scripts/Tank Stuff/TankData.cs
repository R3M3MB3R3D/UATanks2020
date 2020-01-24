using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //These scripts (and components) make use of the tanks' Data.
    //This script will be the one designers use to edit Data.
    public Transform tf;
    public TankMove move;
    public TankAttack attack;
    public TankLife life;

    //movement and rotation speed for tanks.
    public float forwardSpeed = 3;
    public float rotateSpeed = 150;

    //life and armor, armor is optional currently.
    public int tankMaxLife;
    public float tankCurrentLife;
    public float tankArmor;

    //Damage, ammo, tracking current ammo, and cooldown
    //for fire rates in cannon and gun fire function.
    public float tankGunDamage;
    public int tankGunAmmoMax;
    public float tankGunAmmoCurrent;

    public float tankCannonDamage;
    public float tankCannonAmmoMax;
    public float tankCannonAmmoCurrent;

    public int tankCannonFireR;
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
    public int tankScore;
    public int scoreValue;

    void Awake()
    {
        tf = GetComponent<Transform>();
        move = GetComponent<TankMove>();
        attack = GetComponent<TankAttack>();
        life = GetComponent<TankLife>();

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