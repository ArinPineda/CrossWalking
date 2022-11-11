using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

     void Awake()
    {
        if (instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

    }



    public void chooseMode(int mode)
    {
        switch (mode)
        {
            case 1:
                PlayerPrefs.SetInt("EvaluateMode", 0);
                FirstLevel();
                break;
            case 2:
                PlayerPrefs.SetInt("EvaluateMode", 1);
                EvaluateMode();
                break;  

        }
    }
    


    public void EvaluateMode()
    {
        PlayerPrefs.SetInt("EvaluateMode", 1);
        FirstLevel();
    }

    

    public void FirstLevel()
    {
        var loadOperation = SceneManager.LoadSceneAsync("FirstLevel", LoadSceneMode.Single);
        loadOperation.completed += OnSceneLoaded;
    }

    public void SecondLevel()
    {
        SceneManager.LoadScene("SecondLevel");

       
    }
    public void ThirdLevel()
    {
        SceneManager.LoadScene("ThirdLevel");
       
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");

    }

    public void RepeatWrongLevel()
    {
        string nameLevelCurrent = SceneManager.GetActiveScene().name;
        print(nameLevelCurrent);
        SceneManager.LoadScene(nameLevelCurrent);
    }

    private void OnSceneLoaded (AsyncOperation obj)
    {
        Debug.Log("Scene Completed");
    }



    public void exitGame()
    {
        Application.Quit();
    }


    public void NextLevel()
    {
        SceneManager.LoadScene(GameManager.instance.level);
       
    }






}
