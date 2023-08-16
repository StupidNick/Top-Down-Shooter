using UnityEngine.Events;
using UnityEngine;
using System;


public class HealthComponent : MonoBehaviour
{
    public UnityEvent Dead;
    public UnityEvent<float> OnHealthChanged;
    public UnityEvent<float> ArmorChanged;
    [SerializeField] private float  _health, _maxHealth, _armor, _maxArmor, _armorSavePercent;
    void Start()
    {
        OnHealthChanged?.Invoke(_health);
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
        OnHealthChanged?.Invoke(_health);
    }


    private void RemoveHealth(float health)
    {
        _health -= health;
        OnHealthChanged?.Invoke(_health);
        if (_health <= 0)
        {
            Death();
        }
    }


    private void RestoreArmor(float health)
    {
        _armor += health;
        ArmorChanged?.Invoke(_armor);
        if (_armor > _maxArmor)
        {
            _armor = _maxArmor;
        }
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
        //Play death animation
        //Make death widget
        Dead?.Invoke();
    }
}
