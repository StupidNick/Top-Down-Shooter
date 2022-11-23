using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SlotWeaponEnum
{
    FirstSlot,
    SecondSlot,
    ThirdSlot,
    EmptySlot
}


public struct ConsumebleStruct
{
    // BaseConsumeble ConsumableComponent;
    int ConsumableCounter;
}

public class ItemsComponent : MonoBehaviour
{
    public BaseWeaponComponent[] WeaponsArray; 
    SlotWeaponEnum CurrentWeapon = SlotWeaponEnum.FirstSlot;
    ConsumebleStruct[] ConsumebleArray;
    int CurrentConsumebleIndex;


    public void Start()
    {
    }


    public void StartShootFromCurrentWeapon()
    {
        GetCurrentWeapon().StartShooting();
    }


    public void AddWeapon(BaseWeaponComponent newWeapon)
    {
        if (!SetWeaponInSlot(GetFreeSlot(), newWeapon))
        {
            Destroy(GetWeaponBySlot(SlotWeaponEnum.FirstSlot));
            SetWeaponInSlot(SlotWeaponEnum.FirstSlot, newWeapon);
        }
        newWeapon.transform.SetParent(transform);
    }


    public BaseWeaponComponent GetCurrentWeapon()
    {
        return GetWeaponBySlot(CurrentWeapon);
    }


    public BaseWeaponComponent GetWeaponBySlot(SlotWeaponEnum inSlot)
    {
        switch (inSlot)
        {
            case SlotWeaponEnum.FirstSlot:
                return WeaponsArray[0];
            case SlotWeaponEnum.SecondSlot:
                return WeaponsArray[1];
            case SlotWeaponEnum.ThirdSlot:
                return WeaponsArray[2];
            case SlotWeaponEnum.EmptySlot:
                return WeaponsArray[3];
            default:
                return WeaponsArray[0];
        }
    }
    
    
    public SlotWeaponEnum GetFreeSlot()
    {
        int counter = 0;
        foreach (var Weapon in WeaponsArray)
        {
            if (Weapon == null)
            {
                switch (counter)
                {
                    case 0:
                        return SlotWeaponEnum.FirstSlot;
                    case 1:
                        return SlotWeaponEnum.SecondSlot;
                    case 2:
                        return SlotWeaponEnum.ThirdSlot;
                    default:
                        return CurrentWeapon;
                }
            }
            counter++;
        }
        return CurrentWeapon;
    }
    

    public bool SetWeaponInSlot(SlotWeaponEnum inSlot, BaseWeaponComponent inWeapon)
    {
        switch (inSlot)
        {
            case SlotWeaponEnum.FirstSlot:
                WeaponsArray[0] = inWeapon;
                return true;
            case SlotWeaponEnum.SecondSlot:
                WeaponsArray[1] = inWeapon;
                return true;
            case SlotWeaponEnum.ThirdSlot:
                WeaponsArray[2] = inWeapon;
                return true;
            default:
                return false;
        }
    }
}
