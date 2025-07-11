using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float delay = 3f;

    void Start()
    {
        Invoke("DestroyObject", delay);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}