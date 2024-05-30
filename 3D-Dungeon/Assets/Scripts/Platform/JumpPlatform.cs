using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float jumpPlatformForce;
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normalVector =  collision.contacts[0].normal;
        float value = Vector3.Dot(normalVector, Vector3.up);
        // Vector3.Dot = º¤ÅÍ°£ ³»Àû
        if ( Mathf.Approximately(value,-1) )
        {
            Debug.Log("asdf");
            CharacterManager.Instance.Player.controller.Jump(jumpPlatformForce);
        }        
    }
}
