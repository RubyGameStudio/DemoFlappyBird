using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverUIHandler : MonoBehaviour
{
    [SerializeField]
    private float fadeInTime = 1f;

    private float timer = 0f;
    private bool isFadeStarted = false;

    private CanvasGroup canvasGroup;

    public void OnGameOver()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        isFadeStarted = true;
    }

    private void Update()
    {
        if (isFadeStarted)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeInTime);

            if (timer > fadeInTime)
            {
                isFadeStarted = false;
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
            }
        }
    }

    public void OnTapScreen()
    {
        GameManager.Instance.RestartGame();
    }
}
