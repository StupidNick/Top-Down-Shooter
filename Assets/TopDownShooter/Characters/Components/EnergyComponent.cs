using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyComponent : MonoBehaviour
{
    public float initialEnergyLevel;
    float energy;
    
    
    void Start()
    {
        energy = initialEnergyLevel;
    }


    public void RemoveEnergy(float InEnergy)
    {
        energy -= InEnergy;
        Debug.Log("Energy level: " + energy);
    }


    public void RestoreEnergy(float InEnergy)
    {
        energy += InEnergy;
    }


    public float GetEnergy()
    {
        return energy;
    }
}
