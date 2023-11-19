using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] float gameTime;
    [SerializeField] Image timerImage;
    float maxGameTime;

    private void Awake() => maxGameTime = gameTime;
    public void StartTimer()
    {
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (gameTime > 0)
        {
            gameTime -= Time.deltaTime;

            var updateTimerImageValue = gameTime / maxGameTime;

            timerImage.fillAmount = updateTimerImageValue;

            yield return null;
        }

        // Timer is done, perform any actions you need to when the timer is finished
        Debug.Log("Timer Done!");
    }

}
