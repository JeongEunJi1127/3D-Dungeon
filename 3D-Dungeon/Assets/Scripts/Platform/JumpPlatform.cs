using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float jumpPlatformForce;
    private void OnCollisionEnter(Collision collision)
    {
        CharacterManager.Instance.Player.controller.Jump(jumpPlatformForce);
    }
}
