using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Ammo
{
    public int PistolAmmo;
    public int AutoRifleAmmo;
    public int ShotgunAmmo;
    public int MachineGunAmmo;
    public int SniperRifleAmmo;
}


public class ItemsComponent : MonoBehaviour
{
    private Ammo ammo;
//for tests
    public int defaultNumberOfAmmo;

    public void Start()
    {
        ammo.PistolAmmo = defaultNumberOfAmmo;
        ammo.AutoRifleAmmo = 0;
        ammo.ShotgunAmmo = defaultNumberOfAmmo;
        ammo.MachineGunAmmo = defaultNumberOfAmmo;
        ammo.SniperRifleAmmo = defaultNumberOfAmmo;
    }


    public int GetAmmo(TypeOfWeapon typeOfWeapon)
    {
        switch (typeOfWeapon)
        {
            case TypeOfWeapon.Pistol:
                // Debug.Log(ammo.PistolAmmo);
                // Debug.Log(typeOfWeapon);
                return ammo.PistolAmmo;
            case TypeOfWeapon.AutoRifle:
                return ammo.AutoRifleAmmo;
            case TypeOfWeapon.Shotgun:
                return ammo.ShotgunAmmo;
            case TypeOfWeapon.MachineGun:
                return ammo.MachineGunAmmo;
            case TypeOfWeapon.SniperRifle:
                return ammo.SniperRifleAmmo;
            default:
                return 0;
        }
    }
}
