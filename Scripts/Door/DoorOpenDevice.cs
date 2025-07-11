using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] Vector3 dPos;
    [SerializeField] float speed = 1.0f;

    private bool open;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    public void Operate()
    {
        if (open)
        {
            targetPosition -=dPos;
        }
        else
        {
            targetPosition += dPos;
        }
        open = !open;
    }

    public void Activate()
    {
        if (!open)
        {
            targetPosition += dPos;
            open = true;
        }
    }

    public void Deactivate()
    {
        if (open)
        {
            targetPosition -= dPos;
            open = false;
        }
    }
}