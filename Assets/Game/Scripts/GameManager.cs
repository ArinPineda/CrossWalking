using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int level;
    [SerializeField]
    public Boolean enableEvaluationMode = false;
    private String[] wordList;
    private String word;
    public int nextPoint = 0;
    public Boolean boolNextLevel = false;
    public string currentCedula;
    public GameObject[] wayBlockPoints;
    public int nextWayBlock;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        currentCedula = DataBaseManager.instance.currentUserByCedula.cedula;
        if (level == 0)
        {
            level = level + 1;
        }
        switchLevel(this.level);


    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
      
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
       
       
        switchLevel(this.level);
   

        if (GameManager.instance.boolNextLevel)
        {
            GameManager.instance.boolNextLevel = false;

            Timer.instance.findTextTimer();
            this.nextPoint = 0;
            this.nextWayBlock = 0;
            //switchLevel(this.level);
        }
    }



    public void nextIndication()
    {
        nextPoint++;
        word = wordList[nextPoint];
        UIManager.instance.Ui_Word.text = word;
    }

    public String getIndication()
    {
        return word;
    }

    private void SetLevelOne()
    {
       // 8 points

        enableEvaluationMode = PlayerPrefs.GetInt("EvaluateMode", 0) == 1;

        if (!enableEvaluationMode)
        {


            wordList = new String[]{ "Go straight ahead", "Turn Left",
            "Go straight along to  hot dog cart", "Cross the street to the right",
            "Cross the street to the left", "Go straight along to entrace to the park", "Then turn to the right","You are finish" };
        }
        else
        {
            wordList = new String[]{ "", "",
            "", "",
            "", "", "","You are finish" };
        }
        word = wordList[nextPoint];
        UIManager.instance.Ui_Word.text = word;
    }

    private void SetLevelTwo()
    {
        // 14 points
        enableEvaluationMode = PlayerPrefs.GetInt("EvaluateMode", 0) == 1;

        if (!enableEvaluationMode)
        {
            wordList = new String[]{ "Go straight ahead", "Turn Right", " Go straight ahead",
            "Cross the street to the left", "Cross the street to the right",
            "Keep left until you see the park entrance", "Turn right", "Turn right and go straight ahead" ,
            "Turn left", "Turn right and go straight","Soon turn left",
            "Take the first exit to the left", "Turn left and keep walking to the crossing",
            "Pass the avenue and arrive at the bus stop","You are finish" };
        }
        else
        {
            wordList = new String[]{ "", "", "",
            "", "",
            "", "", "" ,
            "", "","",
            "", "",
            "","You are finish" };
        }


        this.nextPoint = 0;
        word = wordList[nextPoint];
        UIManager.instance.Ui_Word.text = word;
        print("AAAAAAAAAAAAAAAAAA arreglo");
        print(wordList.ToString());

    }

    private void SetLevelThree()
    {

        //9 points
        enableEvaluationMode = PlayerPrefs.GetInt("EvaluateMode", 0) == 1;

        if (!enableEvaluationMode)
        {

            wordList = new String[]{ "Go straight ahead", "Cross the street", "Go straight ahead",
            "Turn left and go straight", "Keep walking down the sidewalk to the corner",
            "Then turn the Left", "Cross the street to the right and turn left", "Turn right and go straight" ,
            "Turn left and go straight until you see the hospital", "You are finish" };
        }
        else
        {
            wordList = new String[]{ "", "", "",
            "", "",
            "", "", "" ,
            "", "You are finish" };
        }

        this.nextPoint = 0;
        word = wordList[nextPoint];
        UIManager.instance.Ui_Word.text = word;
        print("AAAAAAAAAAAAAAAAAA arreglo");
        print(wordList.ToString());

    }

    public void modeEvaluativeOn()
    {        
        PlayerPrefs.SetInt("EvaluateMode", 1);
    }



    public void switchLevel(int level)
    {
        print("Entra al switch text");
        print("Nivel actual es: " + this.level);

        switch (level)
        {
            case 1:
                boolNextLevel = true;
                this.nextPoint = 0;
                this.nextWayBlock = 0;
                SetLevelOne();
                break;
            case 2:
                SetLevelTwo();
                break;
            case 3:
                SetLevelThree();
                break;

            default:

                break;
        }
    }

    public IEnumerator WaitNextLevel(int mistakesPlayer)
    {

        UIManager.instance.SetAllTextStats(mistakesPlayer);
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);


        print("Start to load the second level");
        level = level + 1;
        print("El nivel es " + level);

        DataBaseManager.instance.AddNewLevelDataUpload((level - 1), Timer.instance.timeRemaining, mistakesPlayer, currentCedula);




        if (level == 4)
        {

            User updateUser = DataBaseManager.instance.GetUserByCedula(this.currentCedula);
            int totalMisstakes = updateUser.GetTotalMistakes();
            updateUser.TotalErros = totalMisstakes;
            DataBaseManager.instance.UpdateUser(updateUser.cedula, updateUser);
            level = 0;
            PlayerPrefs.SetInt("EvaluateMode", 0);
            LevelManager.instance.MenuScene();
        }
        else
        {


            LevelManager.instance.NextLevel();
            boolNextLevel = true;

        }



    }
    public void NextWayBlock()
    {
        if (wayBlockPoints.Length > nextWayBlock)
        {
            wayBlockPoints[nextWayBlock].SetActive(true);
            nextWayBlock += 1;
        }
    }

  
}
