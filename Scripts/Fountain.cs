using UnityEngine;

public class FountainController : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterParticles;

    void Start()
    {
        waterParticles.Stop();
    }

    public void Activate()
    {
        if (waterParticles != null && !waterParticles.isPlaying)
        {
            waterParticles.Play();
        }
    }

    public void Deactivate()
    {
        if (waterParticles != null && waterParticles.isPlaying)
        {
            waterParticles.Stop();
        }
    }
}