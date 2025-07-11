using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public bool isEnable = true;
    public float rotationSpeed = 1200f;
    public float currentRotationSpeed = 0;
    public float changeSpeed = 200f;

    // Update is called once per frame
    void Update()
    {
        if (isEnable)
        {
            if (currentRotationSpeed < rotationSpeed)
            {
                currentRotationSpeed += changeSpeed * Time.deltaTime;
            }
            transform.Rotate(0, currentRotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            if (currentRotationSpeed > 0)
            {
                currentRotationSpeed -= changeSpeed * Time.deltaTime;
                transform.Rotate(0, currentRotationSpeed * Time.deltaTime, 0);
            }
            else
            {
                currentRotationSpeed = 0;
            }
        }
    }
}
