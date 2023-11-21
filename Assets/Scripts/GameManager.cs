using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    public static float score;
    public bool shouldCreateHitGraphic = false;
    public bool ShouldCreateHitGraphic { get { return shouldCreateHitGraphic; } }
    public float Score { get { return score; } }

    [Header("Game Menu")]
    public Button startAndEndGameButton;
    [SerializeField] TextMeshProUGUI uiText;
    public Canvas canvas;
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Timer")]
    public TimerController timerController;
    public Canvas timerCanvas;

    private void Start()
    {
        startAndEndGameButton.onClick.AddListener(StartGameWithDelay);
    }
    public void StartGameWithDelay()
    {
        Debug.Log("Game Started");
        StartCoroutine(GameStartWithDelay());
    }


    private IEnumerator GameStartWithDelay()
    {
        score = 0;
        scoreText.text = "";
        startAndEndGameButton.gameObject.SetActive(false);
        uiText.text = "GET READY";
        uiText.color = Color.green;
        yield return new WaitForSeconds(2f);
        uiText.text = "3";
        yield return new WaitForSeconds(1f);
        uiText.text = "2";
        yield return new WaitForSeconds(1f);
        uiText.text = "1";
        yield return new WaitForSeconds(1f);
        uiText.text = "GO!";
        Debug.Log("Game Started");
        canvas.gameObject.SetActive(false);
        timerCanvas.gameObject.SetActive(true);

        timerController.StartTimer();

        shouldCreateHitGraphic = true;
    }
    public void PlayerScored(float targetValue)
    {
        score += targetValue;
        Debug.Log(score);
    }

    public void OnTimerEnd()
    {
        Debug.Log("Timer Ended");
        timerCanvas.gameObject.SetActive(false);
        timerController.ResetTimer();

        canvas.gameObject.SetActive(true);
        uiText.text = "GAME OVER";
        scoreText.text = "Your score: " + score;
        scoreText.color = Color.green;
        startAndEndGameButton.gameObject.SetActive(true);
        //startAndEndGameButton.GetComponentInChildren<Text>().text = "TRY AGAIN";
    }
}
