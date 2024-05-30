using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float jumpPlatformForce;
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normalVector =  collision.contacts[0].normal;
        float value = Vector3.Dot(normalVector, Vector3.up);
        // Vector3.Dot = 벡터간 내적
        if ( Mathf.Approximately(value,-1) )
        {
            CharacterManager.Instance.Player.controller.Jump(jumpPlatformForce);
        }        
    }
}
