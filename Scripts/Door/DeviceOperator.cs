using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeviceOperator : MonoBehaviour
{
    public float radius = 2.0f;
    private float distance;

    void Start()
    {
        distance = radius - 0.2f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E was pressed.");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * distance, radius);
            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 hitPosition = hitCollider.transform.position;
                hitPosition.y = transform.position.y;
                Vector3 direction = hitPosition - transform.position;
                if (Vector3.Dot(transform.forward, direction.normalized) > .5f)
                {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
