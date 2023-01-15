using System.Collections;
using UnityEngine.UI;
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
    public Transform SpawnShotPoint;
    public ShootingType ShootingType;
    public float FireDelay = 1;
    public float Damage = 3;
    public int NnumberOfShootInBurst = 3;
    public float ReloadTime = 3;
    public Sprite Icon;


    //private variables
    protected AudioSource shotSound;
    protected bool isReloading = false;
    protected bool canShooting = true;
    protected float nextShootTimer;
    protected int shootCounter = 0;
    protected Transform Owner;
    protected int _ammoInTheClip;
    

    protected void Start()
    {
        shotSound = GetComponent<AudioSource>();
        Owner = transform.parent;
    }


    public int GetAmmoInTheClip()
    {
        return _ammoInTheClip;
    }
    

    protected virtual bool CheckCanShoot()
    {
        return false;
    }


    public void StartShooting()
    {
        if(!CheckCanShoot()) return;
        
        if(ShootingType == ShootingType.single && shootCounter < 1)
        {
            Shoot();
            return;
        }
        if(ShootingType == ShootingType.burst && NnumberOfShootInBurst > shootCounter)
        {
            Shoot(); 
            return;
        }
        if(ShootingType == ShootingType.auto)
        {
            Shoot();
        }
    }


    protected virtual void Shoot()
    {
        nextShootTimer = Time.time + FireDelay;
        Debug.Log("nextShootTimer: " + nextShootTimer);
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
