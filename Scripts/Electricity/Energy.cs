using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float energyCapacity = 100f;
    [SerializeField] private float energy = -1;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        setupEnergy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        //energy = 0;
        return false;
    }
    public bool increaseEnergyBy(float amount) 
    {
        if(energy + amount < energyCapacity)
        {
            energy += amount;
            return true;
        }
        //energy = energyCapacity;
        return false;
    }
    public void setEnergy(float amount)
        { energy = amount; }
}
