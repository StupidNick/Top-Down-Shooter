using System.Collections;
using UnityEngine;



public class EnergyRangeWeapon : RangeWeapon
{
    public EnergyComponent Energy;
    public float EnergyToFire, MaxHeatLevel, HeatingPerShot;

    protected float _heatLevel = 0;
    void Start()
    {
        
    }


    protected override bool CheckCanShoot()
    {
        if(isReloading || !canShooting || Time.time < nextShootTimer || _heatLevel >= MaxHeatLevel) return false;

        if (Energy.GetEnergy() >= EnergyToFire)
        {
            return true;
        }
        return false;
    }


    protected override void Shoot()
    {
        base.Shoot();
        Energy.RemoveEnergy(EnergyToFire);
        _heatLevel += HeatingPerShot;
        Debug.Log("Heat level: " + _heatLevel);//Debug
    }


    protected override IEnumerator EndReload()
    {
        yield return new WaitForSeconds(ReloadTime);

        _heatLevel = 0;
        isReloading = false;
    }
}
