using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;

    Condition Health { get { return uiCondition.health; } }
    Condition Hunger { get { return uiCondition.hunger; } }
    Condition Stamina { get { return uiCondition.stamina; } }

    private void Update()
    {
        Hunger.Subtract(Hunger.passiveValue * Time.deltaTime);  
        Stamina.Add(Stamina.passiveValue * Time.deltaTime);

        UpdateHealthForHunger();
    }

    public void TakeDamage(int damage)
    {
        Health.Subtract(damage);
    }

    public void Heal(float amount)
    {
        Health.Add(amount);
    }

    public void Eat(float amount)
    {
        Hunger.Add(amount);
    }

    public void Die()
    {

    }

    private void UpdateHealthForHunger()
    {
        if(Hunger.curValue <= 0)
        {
            Hunger.curValue = 0;
            Health.Subtract(Health.passiveValue * Time.deltaTime);
        }
        if(Health.curValue <= 0)
        {
            Die();
        }
    }
}
