using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    public int trapDamage;

    private void OnTriggerEnter(Collider other)
    {
        CharacterManager.Instance.Player.condition.TakeDamage(trapDamage);
    }
}
