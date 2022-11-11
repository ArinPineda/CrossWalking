using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseManager : MonoBehaviour
{

    private DatabaseReference dbReference;

    [Tooltip("Listo with information about Users")]
    [SerializeField]
    private List<User> localDB;


    private bool dbIsLoaded = false;
    public User currentUserByCedula;

    public static DataBaseManager instance;
    

    private void Awake()
    {
        if (!DataBaseManager.instance)
        {
            DataBaseManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        dbReference.Push();

        GetRemoteDatabaseInfo();
    //    StartCoroutine(MyTests());
    }

    private void GetRemoteDatabaseInfo()
    {
        StartCoroutine(GetRemoteDataBase((List<User> Users) =>
        {
            localDB = Users;
            dbIsLoaded = true;

        }));

    }

    private IEnumerator GetRemoteDataBase(Action<List<User>> onCallBack)
    {
        var userNameData = dbReference.GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            //Make sure that the format is correct
            string dbJson = snapshot.Child("array").GetRawJsonValue();
            onCallBack.Invoke(new List<User>(JsonHelper.getJsonArray<User>(dbJson)));
        }
    }

    public void CreateUser(User newUser)
    {

        if (dbIsLoaded)
        {
            localDB.Add(newUser);
            UploadData();
        }
        else
        {
            Debug.Log("Database is not loaded check your connection");
        }
    }
    public User GetUserByCedula (string cedula)
    {
        User foundUser = new User("bad-user", "null",0);
        if (dbIsLoaded)
        {
            //return null if there are not users
            foundUser = localDB.FirstOrDefault(u => u.cedula ==cedula);
            
        }
        else
        {
            Debug.Log("Database is not loaded check your connection");
           
        }

        return foundUser;
    }

    private void UploadData()
    {
        if (dbIsLoaded)
        {
            string dbJson = JsonHelper.arrayToJson(localDB.ToArray());
            StartCoroutine(UploadDataToServer(dbJson));

        }
        else
        {
            Debug.Log("Database is not loaded check your connection");
        }
    }

    IEnumerator UploadDataToServer(string dbJson)
    {
        var dbData = dbReference.SetRawJsonValueAsync(dbJson);

        yield return new WaitUntil(predicate: () => dbData.IsCompleted);

        Debug.Log("Data Base is Uploaded");
    }

    public void UpdateUser (string userCedula, User newUser)
    {
        if (dbIsLoaded)
        {
            User currentUser = GetUserByCedula(userCedula);
            if (currentUser != null)
            {
                int currentUserIndex = localDB.IndexOf(currentUser);
                localDB[currentUserIndex] = newUser;
                UploadData();
            }
        }
        else
        {
            Debug.Log("Data Base is not loaded yet");
        }

    }


    IEnumerator MyDemoRoutine()
    {
        string myjdon = JsonHelper.arrayToJson(localDB.ToArray());
        Debug.Log(myjdon);

        GetRemoteDatabaseInfo();

        yield return new WaitForSeconds(2f);

        var zai = localDB.FirstOrDefault(u => u.name == "Zai");
        int index = localDB.IndexOf(zai);

        Debug.Log("the user " + zai.name + " has this gold: " + zai.cedula);

        zai.TotalErros = 3000;

        localDB[index] = zai;


        myjdon = JsonHelper.arrayToJson(localDB.ToArray());
        dbReference.SetRawJsonValueAsync(myjdon);
    }

    IEnumerator MyTests ()
    {
        yield return new WaitForSeconds(2);

        LevelData l1 = new LevelData(1, 15, 20);
        yield return new WaitForEndOfFrame();
        LevelData l2 = new LevelData(2, 20, 25);
        yield return new WaitForEndOfFrame();
        LevelData l3 = new LevelData(3, 25, 30);
        yield return new WaitForEndOfFrame();

        User currentUser = GetUserByCedula("1024533723");
        if (currentUser == null)
        {
            int userIndex = localDB.IndexOf(currentUser);

            currentUser.LevelsData.Add(new LevelData (3, 20, 20));

            localDB[userIndex] = currentUser;

        }
            


        

        localDB[0].LevelsData.Add(l1);
        localDB[0].LevelsData.Add(l2);
        localDB[0].LevelsData.Add(l3);

        UploadData();


        Debug.Log(localDB[0].GetTotalMistakes());
        

    }

    IEnumerator AddNewLevelDataLocal(int levelNumber, float time, int mistakes, string currentCedula)
    {

        User currentUser = GetUserByCedula(currentCedula);
        if (!currentUser.cedula.Equals("null"))
        {
            int userIndex = localDB.IndexOf(currentUser);

            currentUser.LevelsData.Add(new LevelData(levelNumber, time, mistakes));
            yield return new WaitForEndOfFrame();

            localDB[userIndex] = currentUser;
            UploadData();
        }

    }

    public void AddNewLevelDataUpload(int levelNumber, float time, int mistakes, string currentCedula)
    {

        StartCoroutine(AddNewLevelDataLocal(levelNumber,time,mistakes,currentCedula));

    }


    public void currentUser(User currentUser)
    {
        this.currentUserByCedula = currentUser;
    }

}
