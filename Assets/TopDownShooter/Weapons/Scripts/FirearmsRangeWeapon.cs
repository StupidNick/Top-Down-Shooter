using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmsRangeWeapon : RangeWeapon
{
    public int MaxAmmoInTheClip = 5;
    [SerializeField] private Rigidbody _bulletShell;
    [SerializeField] private Transform _bulletSpawnPosition;

    protected void Start()
    {
        base.Start();
        _ammoInTheClip = MaxAmmoInTheClip;
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
        SpawnBulletShell();

        Debug.Log("Ammo in the clip: " + _ammoInTheClip);//Debug
    }


    protected override IEnumerator EndReload()
    {
        yield return new WaitForSeconds(ReloadTime);

        _ammoInTheClip = MaxAmmoInTheClip;
        isReloading = false;
    }


    private void SpawnBulletShell()
    {
        var shell = Instantiate(_bulletShell, _bulletSpawnPosition.position, Quaternion.identity);

        shell.AddForce(_bulletSpawnPosition.forward * Random.Range(15f, 20f));
        shell.GetComponent<BulletShell>().Initialize(_bulletSpawnPosition.forward, _bulletSpawnPosition.rotation);
         
    }
}
