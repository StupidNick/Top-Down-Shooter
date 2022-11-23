using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotRangeWeapon : RangeWeapon
{
    public int MaxAmmoInTheClip = 5;

    protected int _ammoInTheClip;
    void Start()
    {
        _ammoInTheClip = MaxAmmoInTheClip;
    }


    public int GetAmmoInTheClip()
    {
        return _ammoInTheClip;
    }


    protected override bool CheckCanShoot()
    {
        if(isReloading || !canShooting || Time.time < nextShootTimer || _ammoInTheClip <= 0) return false;
        return true;
    }


    protected override void Shoot()
    {
        base.Shoot();
        _ammoInTheClip--;
        Debug.Log("Ammo in the clip: " + _ammoInTheClip);//Debug
    }


    protected override IEnumerator EndReload()
    {
        yield return new WaitForSeconds(ReloadTime);

        _ammoInTheClip = MaxAmmoInTheClip;
        isReloading = false;
    }
}
