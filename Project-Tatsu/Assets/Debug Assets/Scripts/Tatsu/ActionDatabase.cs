using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tatsu;
using System.IO;

public class ActionDatabase : MonoBehaviour {

    public Dictionary<string, string> database = new Dictionary<string, string>();
    public TatsuAction[] actions;
    
    private string databasePath = "actiondatabase.txt";

    //on level load, create a database of all possible attacks
    private void Start() {
        createDatabase();
    }

    void createDatabase() {
        var rawData = new StreamReader(Application.dataPath + "/" + databasePath);
        var data = rawData.ReadToEnd();
        rawData.Close();

        var dataArray = data.Split('\n');
        for (int i = 0; i < data.Length - 1; i++) {
            parseDataIntoDictionary(dataArray[i]);
        }
    }

    void parseDataIntoDictionary(string line) {
        string[] splitData = line.Split(char.Parse(":"));
        var key = splitData[0];
        var value = splitData[1];
        database.Add(key, value);
        //print("key: "+ key + ", value: " + value);
    } 

    public TatsuAction getAction(string tag) {
        if(database.ContainsKey(tag)) {
            int index;
            int.TryParse(tag, out index);
            return actions[index];
        }
        return null;
    }
}