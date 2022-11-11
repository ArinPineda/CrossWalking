using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text Ui_Word;
    public GameObject detectText;

    public GameObject panelStats;
    public TMP_Text mistakesText;
    public TMP_Text TimeText;

   


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
         //   DontDestroyOnLoad(this.gameObject);
            

        }
        else
        {
            Destroy(this);
        }


    }


    private void Start()
    {
        
    }


    public void findText()
    {

        Ui_Word = detectText.GetComponent<TMP_Text>();
       // mistakesText = GameObject.FindObjectsOfType("MistakesText").GetComponent<TMP_Text>();

        mistakesText = GameObject.FindObjectsOfType<TMP_Text>().FirstOrDefault(o => o.name == "MistakesText");
        TimeText = GameObject.FindObjectsOfType<TMP_Text>().FirstOrDefault(o => o.name == "TimeText");
         panelStats = GameObject.FindObjectsOfType<GameObject>().FirstOrDefault(o => o.name == "StatsPanel");
      
       





       // TimeText = GameObject.FindObjectsOfType("TimeText").GetComponent<TMP_Text>();
      //  panelStats = GameObject.FindObjectsOfType("StatsPanel");
     
        GameManager.instance.boolNextLevel = false;
    }

    public void SetAllTextStats(int misstakes)
    {
        panelStats.SetActive(true);
        TimeText.text = "Time: " + Timer.instance.timeText.text;
        mistakesText.text = "Mistakes: " + misstakes;
    }
   


}
