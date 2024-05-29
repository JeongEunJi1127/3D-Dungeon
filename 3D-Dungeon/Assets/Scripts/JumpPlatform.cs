using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float jumpPlatformForce;
    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce(Vector3.up * jumpPlatformForce, ForceMode.Impulse);
    }
}
