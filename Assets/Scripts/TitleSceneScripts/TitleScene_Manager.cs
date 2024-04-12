using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene_Manager : MonoBehaviour
{
    public GameObject _mainMenu;
    public GameObject _gameMode;
    public GameObject _achievments;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenu.SetActive(true);
        _gameMode.SetActive(false);
        _achievments.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFreePlay()
    {
        SceneManager.LoadScene(SceneNameList.FREEPLAY_SCENE);
    }
    public void LoadTutorial1()
    {
        SceneManager.LoadScene(SceneNameList.TUTORIAL1_SCENE);
    }
    public void LoadTutorial2()
    {
        SceneManager.LoadScene(SceneNameList.TUTORIAL2_SCENE);
    }
    public void LoadScenario1()
    {
        SceneManager.LoadScene(SceneNameList.SCENARIO1_SCENE);
    }
    public void LoadScenario2()
    {
        SceneManager.LoadScene(SceneNameList.SCENARIO2_SCENE);
    }
    public void LoadScenario3()
    {
        SceneManager.LoadScene(SceneNameList.SCENARIO3_SCENE);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }

}
