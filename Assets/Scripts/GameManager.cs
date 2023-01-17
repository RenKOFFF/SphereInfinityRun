using System;
using System.Collections;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    Easy = 1,
    Hard = 2,
    Hell = 3
}
public class GameManager : MonoBehaviour
{
    #region Fields

    public event Action OnGameStartedEvent;
    
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    [SerializeField] private float _maxPlayerOffset = 4.5F;
    public float MaxPlayerOffset => _maxPlayerOffset;
    
    
    [SerializeField, Min(1)] private float _timeIncreaseMovementSpeed = 15;
    public float TimeIncreaseMovementSpeed => _timeIncreaseMovementSpeed;
    
    [SerializeField, Min(1)] private float _sizeIncreaseMovementSpeedSize = 1;
    public float SizeIncreaseMovementSpeed => _sizeIncreaseMovementSpeedSize;

    
    [SerializeField, Min(1)] private float _playerMoveSpeed = 4;
    
    public float StartPlayerMoveSpeed { get; private set; }
    public float PlayerMoveSpeed
    {
        get => _playerMoveSpeed * (int)_difficultyLevel;
        set
        {
            if (value > 0) 
                _playerMoveSpeed = value;
        }
    }

    public int LastedTime { get; set; }
    public int AttemptCount { get; set; }

    [SerializeField] private TMP_Dropdown _difficultyLevelDropdown;
    private Difficulty _difficultyLevel = Difficulty.Easy;

    private bool _isGameOver;
    #endregion
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;

        StartPlayerMoveSpeed = PlayerMoveSpeed;
    }

    public void StartGame()
    {
        if (PlayerPrefs.HasKey("AttemptCount"))
        {
            AttemptCount = PlayerPrefs.GetInt("AttemptCount");
        }
        else
        {
            PlayerPrefs.SetInt("AttemptCount", 0);
        }
        
        _isGameOver = false;
        LastedTime = 0;
        
        StartCoroutine(IncreaseMovementSpeed());
        StartCoroutine(LastedTimer());
        OnGameStartedEvent?.Invoke();
    }

    private void OnEnable()
    {
        Player.OnPlayerDiedEvent += OnPlayerDied;
    }

    private void OnDisable()
    {
        Player.OnPlayerDiedEvent -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        AttemptCount++;
        PlayerPrefs.SetInt("AttemptCount", AttemptCount);
        
        _isGameOver = true;
        PlayerMoveSpeed = StartPlayerMoveSpeed;
        StopAllCoroutines();
    }

    public void SwitchDifficulty(int value)
    {
        _difficultyLevel = (Difficulty)(value + 1);
    }
    
    IEnumerator IncreaseMovementSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeIncreaseMovementSpeed);
            PlayerMoveSpeed += SizeIncreaseMovementSpeed;
            if (_isGameOver) yield break;
        }
    }
    
    IEnumerator LastedTimer()
    {
        while (true)
        {
            LastedTime++;
            yield return new WaitForSeconds(1F);
            if (_isGameOver)
            {
                yield break;
            }
        }
    }
}
