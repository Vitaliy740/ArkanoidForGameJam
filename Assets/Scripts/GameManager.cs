using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text _healthAmmountText;
    [SerializeField]
    private TMP_Text _levelIndicatorText;
    [SerializeField]
    private int _maxHealthAmmount=3;


    [SerializeField]
    private LevelProgressionSaver _levelSaver;

    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private GameObject _nextLevelButton;

    [SerializeField]
    private GameObject _losePanel;

    private int _comboCounter = 0;
    private int _experiencePoints;
    private int _currentHealthAmmount = 0;
    private int _currentScore=0;
    private int _currentRemainBlocks = 100;

    public static GameManager Instance;


    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            if (Instance != this) 
            {
                Destroy(this);
            }
        }

        Instantiate(_levelSaver.GetCurrentLevelGeometry());


        _currentHealthAmmount = _maxHealthAmmount;

        var blocks = FindObjectsOfType<Block>();
        foreach (var block in blocks)
        {
            block.OnBlockDestroyed += AddScore;
        }
        _currentRemainBlocks = blocks.Length;
        UpdateUI();
        Time.timeScale = 1f;
    }

    public void AddScore(int score) 
    {
        _currentScore += score;
        _comboCounter += 1;
        _currentRemainBlocks -= 1;
        UpdateUI();
        if (_currentRemainBlocks < 1) 
        {

            _levelSaver.IncreaceCurrentLevel();
            _winPanel.SetActive(true);
            if (_levelSaver.CurrentLevel > _levelSaver.LevelAmmount) 
            {

                _levelSaver.ResetToLastAvailableLevel();
                _nextLevelButton.SetActive(false);
            }
            Time.timeScale = 0f;
        }
    }
    public void ContinueGame() 
    {
        Time.timeScale = 1f;
    }
    private void UpdateUI() 
    {
        _scoreText.text = _currentScore.ToString("D6");
        _healthAmmountText.text = _currentHealthAmmount.ToString();
        _levelIndicatorText.text = _levelSaver.CurrentLevel.ToString() + "/" + _levelSaver.LevelAmmount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallController ballController = collision.GetComponent<BallController>();
        if (ballController) 
        {
            DecreaseHealth();
            if (_currentHealthAmmount <= 0) 
            {
                Time.timeScale = 0f;
                _losePanel.SetActive(true);
            }
            else 
            {
                ballController.ResetBall();
            }
        }
    }

    private void DecreaseHealth() 
    {
        _currentHealthAmmount -= 1;
        SoundManager.Instance.PlaySound(SoundManager.Instance.FailSound);
        UpdateUI();
    }
}
