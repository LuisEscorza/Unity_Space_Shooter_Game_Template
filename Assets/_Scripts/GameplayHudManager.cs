using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameplayHudManager : MonoBehaviour
{
    #region Fields
    [field: Header("Components")]
    [field: SerializeField] private TextMeshProUGUI _fpsText;
    [field: SerializeField] private TextMeshProUGUI _scoreText;
    [field: SerializeField] private Image[] _healthPointSprites;
    [field: SerializeField] private AudioClip _gameOverAudio;
    [field: SerializeField] private TextMeshProUGUI _gameOverText;
    [field: SerializeField] private TextMeshProUGUI _powerupMessageText;
    private string _currentMessage;
    #endregion

    private void Start()
    {
        StartCoroutine(nameof(FPSCounter));
    }

    public void UpdateScoreText(int newScore)
    {
        _scoreText.text = "Score: " + newScore.ToString();
    }

    private IEnumerator FPSCounter()
    {
        while (true)
        {
            // Calculate and update the FPS
            float currentFPS = 1.0f / Time.deltaTime;
            _fpsText.text = "FPS: " + Mathf.Round(currentFPS);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void UpdateCurrentHealth(int value)
    {
        for (int i = 0; i < 3; i++)
        {
            if (value >= i + 1)
            {
                _healthPointSprites[i].color = new Color(1, 1, 1, 1);
            }
            else
                _healthPointSprites[i].color = new Color(1, 1, 1, 0.3f);
        }
    }

    public void PowerupPickedUp(string messageToShow)
    {
        _currentMessage = messageToShow;
        StartCoroutine(nameof(ShowPowerupMessage));
    }

    private IEnumerator ShowPowerupMessage()
    {
        _powerupMessageText.text = _currentMessage;
        yield return new WaitForSeconds(1f);
        _powerupMessageText.text = "";
    }

    public void ShowGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        AudioManager.Manager.PlaySFX(_gameOverAudio);
        Time.timeScale = 0;
    }
}
