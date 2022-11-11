using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStart : MonoBehaviour
{
    private int chooseMode;

    public InputField NameInput;
    public InputField CedulaInput;

    public GameObject panelForm;

    public string cedulaString;



  


    public void OpenForm(int num)
    {

        chooseMode = num;
        panelForm.SetActive(true);



    }

    public void PlayLevel()
    {

        string name = NameInput.text;
        string cedula = CedulaInput.text;
        cedulaString = cedula;
        DataBaseManager.instance.currentUser(DataBaseManager.instance.GetUserByCedula(cedula));
        print(DataBaseManager.instance.currentUserByCedula);
        if (DataBaseManager.instance.currentUserByCedula == null)
        {
            User newUser = new User(name, cedula, 0);
            DataBaseManager.instance.CreateUser(newUser);
        }
        else
        {

            print("Have this user");
            
        }

        LevelManager.instance.chooseMode(chooseMode);

        if(GameManager.instance)
        {
            GameManager.instance.level = 1;
        }


    }




}
