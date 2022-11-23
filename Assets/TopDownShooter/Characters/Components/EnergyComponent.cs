using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnergyComponent : MonoBehaviour
{
    [SerializeField] private float _maxEnergyLevel;
    [SerializeField] private float energy;
    
    
    void Start()
    {
        energy = _maxEnergyLevel;
        // gameObject.GetComponent<PlayerController>().AddComponentToDeathList(this);
    }


    public void RemoveEnergy(float InEnergy)
    {
        energy -= InEnergy;
        if (energy < 0)
        {
            energy = 0;
        }
    }


    public void RestoreEnergy(float InEnergy)
    {
        energy += InEnergy;
        if (energy > _maxEnergyLevel)
        {
            energy = _maxEnergyLevel;
        }
    }


    public float Energy
    {
        get
        {
            return energy;
        }
    }
}
