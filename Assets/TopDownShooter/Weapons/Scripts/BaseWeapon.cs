using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShootingType
{
    single,
    burst, 
    auto
};


public class BaseWeaponComponent : MonoBehaviour
{
    //public variables
    public Transform spawnShotPoint;
    public ShootingType shootingType;
    // public EnergyComponent energyComponent; To EnergyRangeComponent
    public float fireDelay = 1;
    public int numberOfShootInBurst = 3;


    //private variables
    protected AudioSource shotSound;
    protected bool isReloading = false;
    protected bool canShooting = true;
    protected float nextShootTimer;
    protected int shootCounter = 0;
    

    void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }
    

    protected virtual bool CheckCanShoot()
    {
        return false;
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


    protected virtual void Shoot()
    {
        nextShootTimer = Time.time + fireDelay;
        shootCounter++;
        // shotSound.Play();
    }


    public void StopShooting()
    {
        canShooting = false;
    }
    
    
    public void ResetShootingCounter()
    {
        shootCounter = 0;
    }


    public void StartReload()
    {
        isReloading = true;
        StartCoroutine(EndReload());
    }


    protected virtual IEnumerator EndReload()
    {
        yield return new WaitForSeconds(0);
    }
}
