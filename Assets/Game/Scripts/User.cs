
using System;
using System.Collections.Generic;

[Serializable]
public class User
{
    public string cedula;
    public int TotalErros;
  //  public int gold;
    public string name;

    public List<LevelData> LevelsData;
   
    //First Test to upload values
    /*
    public User(string name, int gold)
    {
        this.name = name;
        this.gold = gold;
    }
    */

    //Constructor of the user 
    public User(string name, string cedula,int TotalErros)
    {
        this.cedula = cedula;
        this.name = name;
        this.TotalErros = TotalErros;
        //this.level = level;
                     
    }

    public void SaveLevelInfo (LevelData levelInfo)
    {

        LevelsData.Add(levelInfo);
    }

    public int GetTotalMistakes ()
    {
        int mistakes = 0;

        foreach (var level in LevelsData)
        {
            mistakes += level.mistakes;
        }

        return mistakes;
    }

}
[Serializable]
public class LevelData
{
    public string date = DateTime.Now.ToShortDateString();
    public int levelNumber = 0;
    public float time = 0;
    public int mistakes = 0;

    public LevelData (int levelNumber,float time,int mistakes)
    {
        this.date = DateTime.Now.ToShortDateString();
        this.levelNumber = levelNumber;
        this.time = time;
        this.mistakes = mistakes;
    }
}
