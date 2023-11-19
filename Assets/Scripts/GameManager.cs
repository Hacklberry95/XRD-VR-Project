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

    [Header("UI")]
    public Button startGameButton;
    [SerializeField] TextMeshProUGUI uiText;
    public Canvas canvas;

    [Header("Timer")]
    public TimerController timerController;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartGameWithDelay);
    }
    public void StartGameWithDelay()
    {
        StartCoroutine(GameStartWithDelay());
    }


    private IEnumerator GameStartWithDelay()
    {
        startGameButton.gameObject.SetActive(false);
        uiText.text = "GET READY";
        uiText.color = Color.red;
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


        timerController.StartTimer();


        shouldCreateHitGraphic = true;
    }
    public void PlayerScored(float targetValue)
    {
        score = score + targetValue;
    }
    //public void GameStart()
    //{
    //    Debug.Log("Game Started");
    //    shouldCreateHitGraphic = true;
    //}
}
