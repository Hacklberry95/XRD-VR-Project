using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] float gameTime;
    [SerializeField] Image timerImage;
    float maxGameTime;
    private bool timerActive = false;

    private void Awake() => maxGameTime = gameTime;
    public void StartTimer()
    {
        if (!timerActive)
        {
            StartCoroutine(UpdateTimer());
        }
    }

    private IEnumerator UpdateTimer()
    {
        timerActive = true;

        while (gameTime > 0)
        {
            gameTime -= Time.deltaTime;

            var updateTimerImageValue = gameTime / maxGameTime;

            timerImage.fillAmount = updateTimerImageValue;

            yield return null;
        }
        GameManager.Instance.OnTimerEnd();
        timerActive = false;
    }
    public void ResetTimer()
    {
        StopAllCoroutines();
        gameTime = maxGameTime;
        timerImage.fillAmount = 1f;
        timerActive = false;
    }
}