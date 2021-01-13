using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class User : MonoBehaviour {

    public GameObject model;
    public UserData user;
    public Youjo[] youjo;
    public int blankDays;   //最終ログインからの日数
    DateTime today;

    void NewData() {
        user.firstLogin = DateTime.Now;
        user.lastLogin = DateTime.Now;
        user.Encord();
        Save(JsonUtility.ToJson(user), "userdata.json");
    }


    void Start() {
        if (!File.Exists(Application.dataPath + "\\userdata.json")) NewData();
        ReLoad();
        today = DateTime.Now;

    }

    void Update() {
        if (DateTime.Now.Subtract(today).Days > 1) ReLoad();
    }

    void ReLoad() {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath);
        FileInfo[] files = dir.GetFiles("*.json");
        youjo = new Youjo[files.Length - 1];

        user = Load();

        for (int i = 0; i < files.Length - 1;) {
            youjo[i] = Call(i.ToString() + ".json");
            i++;
        }

        user.Form();
        if (user.firstLoginStr == "") user.firstLogin = DateTime.Now;
        blankDays = DateTime.Now.Subtract(user.lastLogin).Days;
        if (blankDays > 1) user.constantLoginDays = 1;
        else if (blankDays == 1) user.constantLoginDays++;
        user.lastLogin = DateTime.Now;
        user.Encord();
        Save(JsonUtility.ToJson(user), "userdata.json");
    }

    bool Save(string data, string filename) {
        try {
            using (StreamWriter writer = new StreamWriter(Application.dataPath + "\\" + filename, false)) {
                writer.Write(data);
                writer.Flush();
                writer.Close();
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
            return false;
        }
        return true;
    }

    public bool SaveUser() {
        user.Form();
        if (user.firstLoginStr == "") user.firstLogin = DateTime.Now;
        blankDays = DateTime.Now.Subtract(user.lastLogin).Days;
        if (blankDays > 1) user.constantLoginDays = 1;
        else if (blankDays == 1) user.constantLoginDays++;
        user.lastLogin = DateTime.Now;
        user.Encord();
        return Save(JsonUtility.ToJson(user), "userdata.json");
    }

    void OnApplicationQuit() {
        Save(JsonUtility.ToJson(user), "userdata.json");
        for (int i = 0; i < youjo.Length; i++) Save(JsonUtility.ToJson(youjo[i]), i.ToString() + ".json");
    }

    Youjo Call(string filename) {
        string str = "";
        try {
            using (StreamReader sr = new StreamReader(Application.dataPath + "\\" + filename)) {
                str = sr.ReadToEnd();
                sr.Close();
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
        }

        return JsonUtility.FromJson<Youjo>(str);
    }

    UserData Load() {
        string str = "";
        try {
            using (StreamReader sr = new StreamReader(Application.dataPath + "\\userdata.json")) {
                str = sr.ReadToEnd();
                sr.Close();
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
        }

        return JsonUtility.FromJson<UserData>(str);
    }
}

[Serializable]
public class Youjo {
    public string name;
    public string modelName;
    public int closeness;
    public string personarity;
    public string relationship;
    public string feeling;
    public string firstPerson;
    public string secondPerson;
    public DateTime firstServe;
    public string firstServeStr;


    public void Form() {
        if (firstServeStr != "") firstServe = DateTime.Parse(firstServeStr);
    }

    public void Encord() {
        firstServeStr = firstServe.ToString("yyyy/MM/dd HH:mm:ss");
    }
}

[Serializable]
public class UserData {

    public string name;
    public bool music;
    public Time[] alarms;
    public string[] alarmsStr;
    public bool[] alarmLoop;
    public DateTime firstLogin;
    public string firstLoginStr;
    public DateTime lastLogin;
    public string lastLoginStr;
    public int constantLoginDays;
    public string[] servingYoujo;
    public List<AppSC> appSC;

    public struct AppSC {
        public string name;
        public string exePath;
        public string[] key;
    }


    public void Form() {
        if (firstLoginStr != "") firstLogin = DateTime.Parse(firstLoginStr);
        if (lastLoginStr != "") lastLogin = DateTime.Parse(lastLoginStr);
    }

    public void Encord() {
        firstLoginStr = firstLogin.ToString("yyyy/MM/dd HH:mm:ss");
        lastLoginStr = lastLogin.ToString("yyyy/MM/dd");
    }
}

