using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeOfWeapon
{
    Pistol,
    AutoRifle,
    Shotgun,
    MachineGun,
    SniperRifle,
    SteelArms
}

public enum ShootingType
{
     single,
    burst, 
    auto
};

public enum ReloadType
{
    oneBulletReload,
    clipReload
};


public class BaseWeapon : MonoBehaviour
{
    public Transform spawnShotPoint;
    public ShootingType shootingType;
    public ReloadType reloadType;
    public TypeOfWeapon typeOfWeapon;
    public float fireDelay = 1;
    public float clipReloadTime = 3;
    public int MaxAmmoInTheClip = 5, numberOfShootInBurst;


    private int shootCounter = 0, ammoInTheClip;
    private bool canShooting = true, isReloading = false;
    public ItemsComponent itemsComponent;


    public BaseWeapon()
    {
        ammoInTheClip = MaxAmmoInTheClip;
    }


    void Start()
    {
    }
    

    public bool CheckCanShoot()
    {
        if(ammoInTheClip > 0 && canShooting && !isReloading)
        {
            return true;
        }
        return false;
    }


    public int GetAmmoInTheClip()
    {
        return ammoInTheClip;
    }


    public void StartShooting()
    {
        if(!CheckCanShoot()) return;
        
        if(shootingType == ShootingType.single && shootCounter < 1)
        {
            Shoot();
            return;
        }
        if(shootingType == ShootingType.burst && numberOfShootInBurst > shootCounter)
        {
            Shoot(); 
            return;
        }
        if(shootingType == ShootingType.auto)
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        Ray ray = new Ray(spawnShotPoint.position, spawnShotPoint.forward);
        RaycastHit hit;

        float shotDistance = 20;

        if(Physics.Raycast(ray, out hit, shotDistance))
        {
            shotDistance = hit.distance;
        }

        Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 2);

        shootCounter++;
        ammoInTheClip--;
        StopShooting();
        Invoke("ShootingCanStart", fireDelay);
    }


    public void StopShooting()
    {
        canShooting = false;
    }
    
    
    public void ShootingCanStart()
    {
        canShooting = true;
    }
    
    
    public void ResetShootingCounter()
    {
        shootCounter = 0;
    }


    public void StartReload()
    {
        int numbersOfAmmoInInventory = itemsComponent.GetAmmo(typeOfWeapon);
        if (numbersOfAmmoInInventory == 0) return;

        if (reloadType == ReloadType.clipReload)
        {
            StartCoroutine(EndClipReload(numbersOfAmmoInInventory));
        }
    }


    IEnumerator EndClipReload(int numbersOfAmmoInInventory)
    {
        // Debug.Log(ammoInTheClip);
        //get count of ammo from inventory and add thet to ammo in the clip
        
        yield return new WaitForSeconds(clipReloadTime);


        ammoInTheClip = 13;
        Debug.Log(ammoInTheClip);
    }
}
