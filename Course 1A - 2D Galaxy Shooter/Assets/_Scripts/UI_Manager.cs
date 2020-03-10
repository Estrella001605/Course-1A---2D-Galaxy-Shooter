using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    // handle text
    [SerializeField] private Text _scoreText;

    [Header("Lives for Player")]
    [SerializeField] private Image _LiveSprites;
    [SerializeField] private Sprite[] _livesSprite;

    [Header("Game Over Messages")]
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartGameLevel;

    [Header("Call Game Manager")]
    [SerializeField] private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _scoreText.text = "Score : " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartGameLevel.gameObject.SetActive(false);

        if(_gameManager == null)
        {
            Debug.LogError("Game Manger not Loaded..");
        }        
    }

    public void UpDateScore(int playerScore)
    {
        _scoreText.text = "Score : " + playerScore.ToString();
    }

    public void UpDateLives(int currentLives)
    {
        _LiveSprites.sprite = _livesSprite[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartGameLevel.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {      
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
