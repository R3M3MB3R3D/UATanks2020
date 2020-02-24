using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerControl : MonoBehaviour
{
    public List<PowerUp> powerups;
    public TankData data;

    void Start()
    {
        data = GetComponent<TankData>();
        powerups = new List<PowerUp>();
    }

    void Update()
    {
        List<PowerUp> expiredPowers = new List<PowerUp>();
        foreach (PowerUp power in powerups)
        {
            power.duration -= Time.deltaTime;
            if (power.duration <= 0)
            {
                expiredPowers.Add(power);
            }
        }

        foreach (PowerUp power in expiredPowers)
        {
            power.Deactivate(data);
            powerups.Remove(power);
        }
    }

    public void AddPower(PowerUp power)
    {
        power.Activate(data);
        if (!power.isPermanent)
        {
            powerups.Add(power);
        }
    }
}