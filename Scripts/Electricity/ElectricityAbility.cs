using UnityEngine;
using UnityEngine.UI;

public class ElectricityAbility : Energy
{
    [Header("Settings")]
    [SerializeField] private GameObject EballPrefab;
    [SerializeField] public float ballCreationCost = 10;
    [SerializeField] private Vector3 spawnOffset = new Vector3(0, 0, 1);
    [SerializeField] private Slider energySlider;
    [SerializeField] public float energyRecoveryPerSecond = 10f;
    [SerializeField] public float energyTransferPerSecond = 20f;

    private Eball currentBall;
    private Camera currentCamera;

    void Update()
    {
        energySlider.value = getEnergy();

        if (Input.GetButton("Ability"))
        {
            if (currentBall == null)
            {
                CreateBall();
            }
            TransferEnergyToBall();
        }
        else
        {
            increaseEnergyBy(energyRecoveryPerSecond * Time.deltaTime);
            if (Input.GetButtonUp("Ability"))
            {
                ReleaseBall();
            }
        }
    }

    private void CreateBall()
    {
        if (decreaseEnergyBy(ballCreationCost))
        {
            if (currentCamera == null) currentCamera = gameObject.GetComponentInChildren<Camera>();
            GameObject ballObj = Instantiate(
                EballPrefab,
                currentCamera.transform.TransformPoint(spawnOffset),
                currentCamera.transform.rotation,
                currentCamera.transform
            );

            currentBall = ballObj.GetComponent<Eball>();
            currentBall.Initialize(ballCreationCost);
        }
        else
            increaseEnergyBy(ballCreationCost);
    }

    private void TransferEnergyToBall()
    {
        float energyToTransfer = energyTransferPerSecond * Time.deltaTime;
        if (decreaseEnergyBy(energyToTransfer))
        {
            if(!currentBall.increaseEnergyBy(energyToTransfer))
                increaseEnergyBy(energyToTransfer);
        }
    }

    private void ReleaseBall()
    {
        if (currentBall == null) return;

        currentBall.transform.SetParent(null);
        currentBall.ReleaseBall();
        currentBall = null;
    }
}