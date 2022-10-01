using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotRangeWeapon : RangeWeapon
{
    public int maxAmmoInTheClip = 5;

    protected int ammoInTheClip;
    void Start()
    {
        ammoInTheClip = maxAmmoInTheClip;
    }


    public int GetAmmoInTheClip()
    {
        return ammoInTheClip;
    }


    protected override bool CheckCanShoot()
    {
        if(isReloading || !canShooting || Time.time < nextShootTimer || ammoInTheClip <= 0) return false;
        return true;
    }


    protected override void Shoot()
    {
        base.Shoot();
        ammoInTheClip--;
        Debug.Log("Ammo in the clip: " + ammoInTheClip);
    }


    protected override IEnumerator EndReload()
    {
        yield return new WaitForSeconds(reloadTime);

        ammoInTheClip = maxAmmoInTheClip;
        isReloading = false;
    }
}
