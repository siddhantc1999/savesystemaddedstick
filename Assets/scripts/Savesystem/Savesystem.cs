using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;


[System.Serializable]
public class Savedata
{
    public float savedcherryscore = 0;
    public float gamplayhighscore = 0;
    public float playershopno = 0;
}
public class Savesystem : MonoBehaviour
{
    public float highscore;
    public float cherryscore;
    public float playshopno =0;
    Savedata savedata;
    Savedata newsavedata;
    Savedata data;
    public static Savesystem Instance;
    private void Awake()
    {
        Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        savedata = new Savedata();
        savedata = loadplayervalues();
        if (savedata == null)
        {
            savedata.savedcherryscore = 0;
            savedata.gamplayhighscore = 0;
            savedata.playershopno = 0;
        }
        else
        {
            highscore = savedata.gamplayhighscore;
            cherryscore = savedata.savedcherryscore;
            playshopno = savedata.playershopno;
        }
        Scoringsystem.Instance.cherryscore = cherryscore;
        Scoringsystem.Instance.highscorevalue = highscore;
    }

    // Update is called once per frame
    void Update()
    {

     /*   if (Input.GetKeyDown(KeyCode.Space))
        {

            savedata.savedcherryscore++;
            savedata.gamplayhighscore++;
            saveplayervalues();
            savedata = loadplayervalues();
         
        }*/
    }

    public void saveplayervalues()
    {

        savedata.savedcherryscore = cherryscore;
        savedata.gamplayhighscore = highscore;
        savedata.playershopno = playshopno;
        //savedata assign all the values here
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/debanjan1.fun";
       
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, savedata);
        stream.Close();
    }
    public Savedata loadplayervalues()
    {
       
        string path = Application.persistentDataPath + "/debanjan1.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length != 0)
            {

                newsavedata = formatter.Deserialize(stream) as Savedata;
                stream.Close();
                return newsavedata;
            }

        }
        else
        {
            saveplayervalues();
        }

        return savedata;
    }

}