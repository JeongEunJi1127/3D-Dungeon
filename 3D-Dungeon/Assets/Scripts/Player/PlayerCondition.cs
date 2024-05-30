using System;
using System.Collections;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;
    public Action OnTakeDamage;

    [Header("Die")]
    public GameObject dieCanvas;
    public bool gameEnd = false;

    [Header("GameClear")]
    public GameObject winCanvas;

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
        OnTakeDamage?.Invoke();
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
        dieCanvas.SetActive(true);
        gameEnd = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Win()
    {
        winCanvas.SetActive(true);
        gameEnd = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool UseStamina(float amount)
    {
        if (Stamina.curValue - amount < 0)
        {
            return false;
        }
        Stamina.Subtract(amount);
        return true;
    }
    
    public float GetStamina()
    {
        return Stamina.curValue;
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
