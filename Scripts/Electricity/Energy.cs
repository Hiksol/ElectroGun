using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float energyCapacity = 100f;
    [SerializeField] private float energy = -1;
    
    protected virtual void Start()
    { setupEnergy(); }
    
    protected void setupEnergy()
    {
        if (energy == -1)
            energy = energyCapacity;
    }
    
    public float getEnergy()
        { return energy; }
        
    public float getEnergyCapacity()
        { return energyCapacity; }
        
    public bool decreaseEnergyBy(float amount)
    { 
        if(energy > amount)
        {
            energy -= amount;
            return true;
        }
        return false;
    }
    
    public bool increaseEnergyBy(float amount) 
    {
        if(energy + amount < energyCapacity)
        {
            energy += amount;
            return true;
        }
        return false;
    }
    
    public void setEnergy(float amount)
        { energy = amount; }
}
