using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider), typeof(CapsuleCollider))]
public class Eball : Energy
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem Electricity;
    [SerializeField] private ParticleSystem Lightnings;

    [Header("Settings")]
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private float energyToSizeRatio = 1f;
    [SerializeField] private float energyLossPerSecond = 5f;
    [SerializeField] private float maxTransferDistance = 5f;

    private SphereCollider sphereCollider;
    private Collider recipientCollider;
    private Rigidbody rb;
    private float initialPositionX;
    private float initialEnergy;
    private float initialSpeed;
    private float currentSpeed;
    private bool isReleased = false;

    protected override void Start()
    {
        base.Start();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        UpdateSize();
    }

    public void Initialize(float energy)
    {
        setEnergy(energy);
        initialEnergy = energy;
        initialPositionX = transform.localPosition.x;
        UpdateSize();
    }

    void Update()
    {
        if (!isReleased)
        {
            UpdateSize();
            return;
        }

        // Particle movement and processing
        if (!decreaseEnergyBy(energyLossPerSecond * Time.deltaTime))
            Destroy(gameObject);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        UpdateParticles();
        UpdateSize();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Weapon") && !collision.gameObject.CompareTag("Player"))
        {
            currentSpeed = 0f;
            rb.isKinematic = true;
        }
    }

    private void UpdateSize()
    {
        float size = getEnergy() * energyToSizeRatio;
        transform.localScale = Vector3.one * size;

        // Updating the particles
        var electricityMain = Electricity.main;
        electricityMain.startSize = size;
        if (!isReleased)
            transform.localPosition = new Vector3(initialPositionX - size / 3, transform.localPosition.y, transform.localPosition.z);
    }

    private void UpdateParticles()
    {
        // Particle update logic (can be modified)
        if (Lightnings.particleCount > 0)
        {
            var main = Lightnings.main;
            main.startLifetime = getEnergy() / initialEnergy;
        }
    }
    public void ReleaseBall()
    {
        isReleased = true;
        currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, getEnergy() / initialEnergy);
        initialSpeed = currentSpeed;
        rb.isKinematic = false;
    }

    private void OnTriggerStay(Collider other)
    {
        Energy energyRecipient = other.GetComponent<Energy>();
        if (energyRecipient == null) return;
        float distance = Vector3.Distance(transform.position, other.transform.position);
        float transferRate = Mathf.InverseLerp(maxTransferDistance, 0, distance);
        float energyToTransfer = getEnergy() * transferRate * Time.deltaTime;
        if (decreaseEnergyBy(energyToTransfer))
        {
            if (!energyRecipient.increaseEnergyBy(energyToTransfer))
            {
                increaseEnergyBy(energyToTransfer);
                if(energyRecipient.getEnergy() < energyRecipient.getEnergyCapacity())
                {
                    float energyTransfer = energyRecipient.getEnergyCapacity() - energyRecipient.getEnergy();
                    energyRecipient.increaseEnergyBy(energyTransfer);
                    decreaseEnergyBy(energyTransfer);
                }
                return;
            }
            currentSpeed = currentSpeed * transferRate;
            recipientCollider = other;
            UpdateSize();
        }
        else
        {
            energyRecipient.setEnergy(energyRecipient.getEnergy() + getEnergy());
            decreaseEnergyBy(getEnergy());
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(recipientCollider == other && currentSpeed != 0f)
            currentSpeed = initialSpeed;
    }
}
