using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    

    [SerializeField] private GameObject _gameplayArea;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SpawnerDifficultyController _difficultyController;
    

    private void Awake()
    {
        _gameplayArea.SetActive(false);
        _playerController.EndGame();
        _playerController.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        GameData.CurrentLives = 3;
        _difficultyController.OnScoreChanged(GameData.CurrentScore);
        _gameplayArea.SetActive(true);
        _playerController.gameObject.SetActive(true);
        _playerController.StartGame();
    }

    public void EndGame()
    {
        _gameplayArea.SetActive(false);
        _playerController.EndGame();
        _playerController.gameObject.SetActive(false);
        GameManager.Instance.ChangeState(GameManager.GameState.Result);
    }

    public void OnCollectedItem(Transform player)
    {
        _uiController.PlayWinEffect(player);
        
        GameData.CurrentScore++;
        _difficultyController.OnScoreChanged(GameData.CurrentScore);
        GameManager.Instance.UpdateLifeAndScore();
    }

    public void OnCollectedObstacle(Transform player)
    {
        _uiController.PlayFailEffect(player);
        GameData.CurrentLives--;
        if (GameData.CurrentLives == 0)
        {
            EndGame();
        }
        GameManager.Instance.UpdateLifeAndScore();
    }
}
