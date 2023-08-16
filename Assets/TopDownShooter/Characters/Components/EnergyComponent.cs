using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System;

public class EnergyComponent : MonoBehaviour
{
    public UnityEvent<float> OnEnergyChanged;
    [SerializeField] private float _maxEnergyLevel;
    [SerializeField] private float _energy;
    
    
    void Start()
    {
        _energy = _maxEnergyLevel;
        OnEnergyChanged?.Invoke(_energy);
        // gameObject.GetComponent<PlayerController>().AddComponentToDeathList(this);
    }


    public void RemoveEnergy(float InEnergy)
    {
        _energy -= InEnergy;
        Debug.Log("Energy: " + _energy);
        if (_energy < 0)
        {
            _energy = 0;
        }
        OnEnergyChanged?.Invoke(_energy);
    }


    public void RestoreEnergy(float InEnergy)
    {
        _energy += InEnergy;
        if (_energy > _maxEnergyLevel)
        {
            _energy = _maxEnergyLevel;
        }
        OnEnergyChanged?.Invoke(_energy);
    }


    public float Energy
    {
        get
        {
            return _energy;
        }
    }
}
