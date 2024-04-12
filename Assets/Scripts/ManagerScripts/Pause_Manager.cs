using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Pause_Manager _Instance { get; private set; } = null;

    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
            return;
        }
        _Instance = this;
    }
    #endregion

    Achievement_Manager _achievementManager;
    private InteractiveAudio_Manager _interactiveAudioManager;

    public GameObject _pausePanel;
    private bool isPaused = false;
    public GameObject _victoryPanel;
    public GameObject _defeatPanel;

    // Start is called before the first frame update
    void Start()
    {
        _achievementManager = Achievement_Manager._Instance;
        _interactiveAudioManager = InteractiveAudio_Manager._Instance;

        _pausePanel.SetActive(false);
        _victoryPanel.SetActive(false);
        _defeatPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void GameOver(bool victory)
    {
        _achievementManager.CheckEndGameAchievements(victory);

        if (victory)
        {
            Invoke("ActivateVictoryPanel", 2.0F);
        }
        else
        {
            Invoke("ActivateDefeatPanel", 2.0F);
        }
    }

    private void ActivateVictoryPanel()
    {
        _interactiveAudioManager.victory();
        _victoryPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void ActivateDefeatPanel()
    {
        _interactiveAudioManager.defeat();
        _defeatPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void PauseGame()
    {
        _pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        _pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneNameList.MAINMENU_SCENE);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }
}
