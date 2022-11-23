using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    public event Action Dead;
    public event Action<float> HealthChanged;
    public event Action<float> ArmorChanged;
    [SerializeField] private float  _health, _maxHealth, _armor, _maxArmor, _armorSavePercent;
    void Start()
    {
        // var player = gameObject.GetComponent<PlayerController>();
        // if (player != null)
        // {
        //     player.AddComponentToDeathList(this);
        // }
    }

    
    void Update()
    {
        
    }

    public void MakeDamage(float damage)
    {
        if (_armor <= 0)
        {
            RemoveHealth(damage);
            return;
        }
        float armorDamage = damage * _armorSavePercent;
        RemoveArmor(armorDamage);
        RemoveHealth(damage - armorDamage);
    }


    public void RestoreHealth(float health)
    {
        _health += health;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        HealthChanged?.Invoke(_health);
    }


    private void RemoveHealth(float health)
    {
        _health -= health;
        if (_health <= 0)
        {
            Death();
        }
        HealthChanged?.Invoke(_health);
    }


    private void RestoreArmor(float health)
    {
        _armor += health;
        if (_armor > _maxArmor)
        {
            _armor = _maxArmor;
        }
        ArmorChanged?.Invoke(_armor);
    }

    
    private void RemoveArmor(float health)
    {
        _armor -= health;
        if (_armor < 0)
        {
            RemoveHealth(-_armor);
            _armor = 0;
        }
        ArmorChanged?.Invoke(_armor);
    }


    private void Death()
    {
        Dead?.Invoke();
    }
}
