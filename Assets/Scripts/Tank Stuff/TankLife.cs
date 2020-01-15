using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all the 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]

public class TankLife : MonoBehaviour
{
    public TankData tankData;

    void Awake()
    {
        tankData = this.gameObject.GetComponent<TankData>();
    }

    void Update()
    {
        //This is so that we never have more health than we are
        //allowed to have, this can happen with the health pickup.
        if (tankData.tankCurrentLife > tankData.tankMaxLife)
        {
            tankData.tankCurrentLife = tankData.tankMaxLife;
        }

        //This is so that when the tank reaches 0 health it is
        //destroyed instead of remaining and causing other problems.
        if (tankData.tankCurrentLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
