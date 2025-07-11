using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterLanding : MonoBehaviour
{
    public GameObject mainPropeller;
    public GameObject tailPropeller;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float radius;
    public Vector3 center;
    private float startAngleDegrees;
    public float angleDegrees;
    public float targetAngleDegrees;
    public Vector3 finalTarget;
    private Vector3 target = new Vector3(40,10,30);
    private bool count = true;

    void Start()
    {
        startAngleDegrees = angleDegrees;
    }
    
    void Update()
    {
        if (angleDegrees <= targetAngleDegrees || Vector3.Distance(transform.position, finalTarget) > 0.1f)
        {
            if (Vector3.Distance(transform.position, target) < 0.1f)
                target = newTarget();

            // Calculating the direction to the target point
            Vector3 direction = target - transform.position;
            direction.y = 0;

            // Turning the object in the direction of the target point
            if (direction != Vector3.zero)
            {
                float zRotationAngle = Mathf.Min(angleDegrees - startAngleDegrees, targetAngleDegrees - angleDegrees) / 2;
                Quaternion zRotation = Quaternion.Euler(0, 0, zRotationAngle);
                Quaternion targetRotation = Quaternion.LookRotation(direction) * zRotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Moving the object in the direction of the target point
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            if(count)
            {
                mainPropeller.GetComponent<Rotation>().isEnable = false;
                tailPropeller.GetComponent<Rotation>().isEnable = false;
                count = false;
            }
        }
            
    }

    public void Operate()
    {
        if (mainPropeller.GetComponent<Rotation>().isEnable)
        {
            mainPropeller.GetComponent<Rotation>().isEnable = false;
            tailPropeller.GetComponent<Rotation>().isEnable = false;
        }
        else
        {
            mainPropeller.GetComponent<Rotation>().isEnable = true;
            tailPropeller.GetComponent<Rotation>().isEnable = true;
        }
    }

    Vector3 newTarget()
    {
        if (angleDegrees <= targetAngleDegrees)
        {
            float angleRadians = angleDegrees * Mathf.Deg2Rad;
            angleDegrees += 1;
            return new Vector3(
                center.x + radius * Mathf.Cos(angleRadians),
                transform.position.y,
                center.z + radius * Mathf.Sin(angleRadians)
            );
        }
        else
        {
            return finalTarget;
        }
    }
}
