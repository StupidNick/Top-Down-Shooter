using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRangeWeapon : RangeWeapon
{
    public EnergyComponent energyComponent;
    public float energyToFire, maxHeatLevel, heatingPerShot;

    protected float heatLevel = 0;
    void Start()
    {
        
    }


    protected override bool CheckCanShoot()
    {
        if(isReloading || !canShooting || Time.time < nextShootTimer || energyComponent == null || heatLevel >= maxHeatLevel) return false;

        if (energyComponent.GetEnergy() >= energyToFire)
        {
            return true;
        }
        
        return false;
    }


    protected override void Shoot()
    {
        base.Shoot();
        energyComponent.RemoveEnergy(energyToFire);
        heatLevel += heatingPerShot;
        Debug.Log("Heat level^ " + heatLevel);
    }


    protected override IEnumerator EndReload()
    {
        yield return new WaitForSeconds(reloadTime);

        heatLevel = 0;
        isReloading = false;
    }
}
