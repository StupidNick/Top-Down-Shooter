using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class WeaponIcon : MonoBehaviour
{
    public Image _weaponIcon;
    public void OnWeaponChanged(Sprite newIcon)
    {
        if (_weaponIcon == null) return;
        
        _weaponIcon.sprite = newIcon;
    }
}
