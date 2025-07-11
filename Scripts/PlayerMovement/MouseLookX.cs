using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookX : MonoBehaviour
{
    private float horizontalRot;
    public float sensitivity = 500;
    // Update is called once per frame
    void Update()
    {
        horizontalRot = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        transform.Rotate(0, horizontalRot, 0);
    }
}
