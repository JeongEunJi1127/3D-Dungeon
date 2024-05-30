using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{
    public Image damageIndicatorImage;
    public float flashSpeed;

    private Coroutine coroutine;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamage += Flash;
        gameObject.SetActive(false);
    }

    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        damageIndicatorImage.enabled = true;
        damageIndicatorImage.color = new Color(1f, 105f / 255f, 105f / 255f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0.0f)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            damageIndicatorImage.color = new Color(1f, 105f / 255f, 105f / 255f, a);
            yield return null;
        }

        damageIndicatorImage.enabled = false;
    }
}
