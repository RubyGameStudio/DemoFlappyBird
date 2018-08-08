using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup mainMenu, hud;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private float fadeTime = 1f;

    private float timer = 0f;
    private bool isCrossFadeStarted = false;

    public void OnGameStart()
    {
        isCrossFadeStarted = true;
    }

    private void Update()
    {
        if (isCrossFadeStarted)
        {
            timer += Time.deltaTime;
            mainMenu.alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);
            hud.alpha = Mathf.Lerp(0f, 1f, timer / fadeTime);
            if (timer > fadeTime)
            {
                isCrossFadeStarted = false;
                mainMenu.alpha = 0f;
                hud.alpha = 1f;
            }
        }
    }

    public void OnScoreChange()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
    }
}
