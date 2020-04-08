using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PowerUp
{
    public float speedMod = 5;
    public float healthMod = 50;
    public float gunMod = 50;
    public float cannonMod = 5;
    public float fireRateMod = 1;

    public float duration = 30.0f;
    public bool isPermanent;

    public enum PowerType { Speed, Health, Gun, Cannon, FireR }
    public PowerType powerType;

    public void Activate(TankData target)
    {
        powerType = (PowerType)Random.Range(0, System.Enum.GetNames(typeof(PowerType)).Length);
        switch (powerType)
        {
            case PowerType.Speed:
                target.forwardSpeed += speedMod;
                isPermanent = false;
                break;
            case PowerType.Health:
                target.tankCurrentLife += healthMod;
                isPermanent = true;
                break;
            case PowerType.Gun:
                target.tankGunDamage += gunMod;
                isPermanent = true;
                break;
            case PowerType.Cannon:
                target.tankCannonDamage += cannonMod;
                isPermanent = true;
                break;
            case PowerType.FireR:
                target.tankCannonFireR -= fireRateMod;
                isPermanent = false;
                break;
        }
    }

    public void Deactivate(TankData target)
    {
        switch (powerType)
        {
            case PowerType.Speed:
                target.forwardSpeed -= speedMod;
                break;
            case PowerType.Health:
                break;
            case PowerType.Gun:
                break;
            case PowerType.Cannon:
                break;
            case PowerType.FireR:
                target.tankCannonFireR += fireRateMod;
                break;
        }
    }
}