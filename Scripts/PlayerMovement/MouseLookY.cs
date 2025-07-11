using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookY : MonoBehaviour
{
    public float sensitivity = 3.0f;
    public float minimumDeg = -90.0f;
    public float maximumDeg = 90.0f;

    private float verticalRot = 0;

    // Update is called once per frame
    void Update()
    {
        verticalRot -= Input.GetAxis("Mouse Y") * sensitivity;
        verticalRot = Mathf.Clamp(verticalRot, minimumDeg, maximumDeg);

        float horizontalRot = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3 (verticalRot, horizontalRot, 0);
    }
}
