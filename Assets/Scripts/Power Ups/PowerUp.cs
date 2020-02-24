using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PowerUp
{
    public float speedMod;
    public float healthMod;
    public float gunMod;
    public float cannonMod;
    public float fireRateMod;

    public float duration;
    public bool isPermanent;

    public void Activate(TankData target)
    {
        target.forwardSpeed += speedMod;
        target.tankCurrentLife += healthMod;
        target.tankGunDamage += gunMod;
        target.tankCannonDamage += cannonMod;
        target.tankCannonFireR -= fireRateMod;
    }

    public void Deactivate(TankData target)
    {
        target.forwardSpeed -= speedMod;
        target.tankCurrentLife -= healthMod;
        target.tankGunDamage -= gunMod;
        target.tankCannonDamage -= cannonMod;
        target.tankCannonFireR += fireRateMod;
    }
}
