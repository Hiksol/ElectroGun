using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPadForce = 110;

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<FPSInput>().applyJumpForce(jumpPadForce);
    }
}