using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastTapScreen : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeedSeconds;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        gameManager.OnScorePctChanged += HandleScoreChanged;
    }
    private void HandleScoreChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }
    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
        foregroundImage.fillAmount = pct;
    }
}
