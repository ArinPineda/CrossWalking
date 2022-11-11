using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatsController : MonoBehaviour
{

    public GameObject panelStats;
    public TMP_Text mistakesText;
    public TMP_Text TimeText;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.panelStats = gameObject;
        UIManager.instance.mistakesText = mistakesText;
        UIManager.instance.TimeText = TimeText;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
