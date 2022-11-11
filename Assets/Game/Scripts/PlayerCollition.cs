using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollition : MonoBehaviour
{

    private CapsuleCollider capsuleCollider;
    public Transform backPosition;
    private GameObject character;
    public GameObject [] cubesRed;
    public int mistakes;
    public GameObject WayBlocker;
    public AudioClip[] audios;


    void Start()
    {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        mistakes = 0;
        character = GameObject.Find("ThirdPersonController_LITE");
        backPosition = GameObject.Find("StartPoint").transform;
        backPosition.position = transform.position;
        print(backPosition.position);
        gameObject.GetComponent<AudioSource>().clip = audios[0];
        gameObject.GetComponent<AudioSource>().Play();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            bool enableOrDisabled = PlayerPrefs.GetInt("EvaluateMode", 0) == 1;
            if (enableOrDisabled)
            {
               LevelManager.instance.RepeatWrongLevel();
            }
            else
            {
                UIManager.instance.Ui_Word.text = "Wrong Way";
                character.GetComponent<Animator>().enabled = false;
                character.GetComponent<vThirdPersonInput>().enabled = false;
                character.GetComponent<Rigidbody>().isKinematic = true;
                
                Invoke("TpToBackPosition",1.0f);
                
            }
           
        }
        else if (other.gameObject.CompareTag("NextPoint"))
        {
            print("Collision in the nextPoint");
            GameManager.instance.nextIndication();
            gameObject.GetComponent<AudioSource>().clip = audios[GameManager.instance.nextPoint];
            gameObject.GetComponent<AudioSource>().Play();
            backPosition.position = other.gameObject.transform.position;
            other.gameObject.SetActive(false);
            GameManager.instance.NextWayBlock();
          //  WayBlocker.GetComponent<Transform>().position = new Vector3(other.gameObject.GetComponent<Transform>().position.x, -5.59f, other.gameObject.GetComponent<Transform>().position.z);

            if (UIManager.instance.Ui_Word.text.CompareTo("You are finish") == 0)
            {

                StartCoroutine(GameManager.instance.WaitNextLevel(mistakes));
               
            }
           

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            mistakes += 1;

            string word = GameManager.instance.getIndication();
            UIManager.instance.Ui_Word.text = word;
        }
    }

    private void TpToBackPosition()
    {
        print("Call the tp back position");
        character.transform.position = backPosition.position;
        character.GetComponent<Animator>().enabled = true;
        character.GetComponent<Rigidbody>().isKinematic = false;
        
        Invoke("EnableMovement", 1);
    }

    private void EnableMovement ()
    {
        character.GetComponent<vThirdPersonInput>().enabled = true;
    }


    

    
}
