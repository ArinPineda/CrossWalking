using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayBlocksController : MonoBehaviour
{

    public GameObject[] wayBlockPoints;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.wayBlockPoints = wayBlockPoints;    
    }

}
