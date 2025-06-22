using System.Collections;
using System.Collections.Generic;
using MPUIKIT;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
   [SerializeField] private Canvas canvas;
   [SerializeField] private List<GameObject> _winFailPrefabs;
   public void PlayWinEffect(Transform player)
   {
      GameObject instance = Instantiate(_winFailPrefabs[0]);
      instance.GetComponent<FloatingTextEffect>().Initialize(player.position, canvas);
   }

   public void PlayFailEffect(Transform player)
   {
      GameObject instance = Instantiate(_winFailPrefabs[1]);
      instance.GetComponent<FloatingTextEffect>().Initialize(player.position, canvas);
   }
   
   
    [Header("Screens")]
    public GameObject splashScreen;
    public GameObject menuScreen;
    public GameObject gameplayScreen;
    public GameObject resultScreen;

    [Header("Menu Screen UI")]
    public TMP_Text highScoreText;
    public TMP_Text soundTxt;
    // public List<Image> soundSprites;

    [Header("Gameplay UI")]
    public TMP_Text scoreText;

    [Tooltip("Drag 3 life icon GameObjects here")]
    public GameObject[] lifeIcons; // Should have 3 icons

    [Header("Result Screen UI")]
    public TMP_Text finalScoreText;

    [SerializeField] private Color activeColor;
    [SerializeField] private Color deactiveColor;

    // ==============================
    // Screen Visibility Handlers
    // ==============================

    public void ShowOnlySplash()
    {
        splashScreen.SetActive(true);
        menuScreen.SetActive(false);
        gameplayScreen.SetActive(false);
        resultScreen.SetActive(false);
    }

    public void ShowOnlyMenu()
    {
        splashScreen.SetActive(false);
        menuScreen.SetActive(true);
        gameplayScreen.SetActive(false);
        resultScreen.SetActive(false);
    }

    public void ShowOnlyGameplay()
    {
        splashScreen.SetActive(false);
        menuScreen.SetActive(false);
        gameplayScreen.SetActive(true);
        resultScreen.SetActive(false);
    }

    public void ShowOnlyResult()
    {
        splashScreen.SetActive(false);
        menuScreen.SetActive(false);
        gameplayScreen.SetActive(false);
        resultScreen.SetActive(true);
    }

    // ==============================
    // Menu UI Updates
    // ==============================

    public void UpdateHighScore(int highScore)
    {
        highScoreText.text = $"{highScore}";
    }

    // ==============================
    // Gameplay UI Updates
    // ==============================

    public void UpdateScore(int score)
    {
        scoreText.text = $"{score}";
    }

    public void UpdateLife(int life)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            MPImage img = lifeIcons[i].GetComponent<MPImage>();
            if (img != null)
            {
                img.color = i < life ? activeColor : deactiveColor;
            }
        }
    }
    // ==============================
    // Result Screen Actions
    // ==============================

    public void SetFinalScore(int score)
    {
        finalScoreText.text = $"{score}";
    }

    public void OnClickRestart()
    {
      //  SoundManager.Instance.PlayClick();
        GameManager.Instance.ChangeState(GameManager.GameState.Gameplay);
    }

    public void OnClickHome()
    {
     //   SoundManager.Instance.PlayClick();
        GameManager.Instance.ChangeState(GameManager.GameState.Menu);
    }

    public void ToggleSound()
    {
        bool isSoundOn = SoundManager.Instance.ToggleSound();
        soundTxt.text = !isSoundOn ? "SOUND OFF" : "SOUND ON";
    } 
}
