using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance ?? (instance = FindObjectOfType<GameManager>()); } }

    [SerializeField]
    private UIHandler uIHandler;

    [SerializeField]
    private BirdController birdController;

    [SerializeField]
    private GameOverUIHandler gameOverUIHandler;
    [SerializeField]
    private PipeFactory pipeFactory;

    public bool IsGameStarted = false, IsGameOver = false;

    public int Score = 0;

    public float GameSpeed = 2f;

    [SerializeField]
    private float minGameSpeed = 2f, maxGameSpeed = 5f;
    [SerializeField]
    private int maxScoreToMaxSpeed = 50;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsGameStarted)
            {
                uIHandler.OnGameStart();
                birdController.OnGameStart();
                pipeFactory.StartGeneration();
                IsGameStarted = true;
            }
        }
    }

    public void OnGameOver()
    {
        gameOverUIHandler.OnGameOver();
        IsGameOver = true;
        pipeFactory.StopGeneration();
    }

    public void ScoreChange()
    {
        Score++;
        uIHandler.OnScoreChange();
        GameSpeed = Mathf.Lerp(minGameSpeed, maxGameSpeed, Score / (float)maxScoreToMaxSpeed);
    }

    public void RestartGame()
    {
        instance = null;
        SceneManager.LoadScene("MainScene");
    }
}
