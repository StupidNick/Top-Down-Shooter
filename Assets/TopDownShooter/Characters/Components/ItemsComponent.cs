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
    [SerializeField]
    private BaseWeaponComponent[] WeaponsArray; 
    [SerializeField]
    private Transform WeaponPoint; 
    [SerializeField]
    private Transform HideWeaponPoint; 
    SlotWeaponEnum CurrentWeaponSlot = SlotWeaponEnum.EmptySlot;
    // ConsumebleStruct[] ConsumebleArray;
    int CurrentConsumebleIndex;


    public void Start()
    {
    }


    public void StartShootFromCurrentWeapon()
    {
        if (GetCurrentWeapon() == null) return;
        
        GetCurrentWeapon().StartShooting();
    }
    
    public void ResetShootCounterForCurrentWeapon()
    {
        if (GetCurrentWeapon() == null) return;
        
        GetCurrentWeapon().ResetShootingCounter();
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
        return GetWeaponBySlot(CurrentWeaponSlot);
    }


    public void ChangeWeapon(SlotWeaponEnum inSlot)
    {
        if (CurrentWeaponSlot != SlotWeaponEnum.EmptySlot)
        {
            if (CurrentWeaponSlot == inSlot)
            {
                HideWeapon(GetCurrentWeapon());
                return;
            }
            HideWeapon(GetCurrentWeapon());
        }
        
        RaiseWeapon(inSlot);
    }


    private void HideWeapon(BaseWeaponComponent weapon)
    {
        if (weapon == null) return;
        
        weapon.transform.position = HideWeaponPoint.position;
        weapon.enabled = false;
        CurrentWeaponSlot = SlotWeaponEnum.EmptySlot;
    }
    
    
    private void RaiseWeapon(SlotWeaponEnum inSlot)
    {
        BaseWeaponComponent weapon = GetWeaponBySlot(inSlot);
        if (weapon == null) return;
        
        weapon.transform.position = WeaponPoint.position;
        weapon.enabled = true;
        CurrentWeaponSlot = inSlot;
        Debug.Log("Now active slot is: " + inSlot);//Debug
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
                return null;
            default:
                return null;
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
                        return CurrentWeaponSlot;
                }
            }
            counter++;
        }
        return CurrentWeaponSlot;
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
