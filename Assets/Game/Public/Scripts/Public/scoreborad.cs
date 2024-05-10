using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class scoreborad
{
    public int MaxUser = 100; //  최대 유저 수

    public Dictionary<string , int> scoreboard_process(string GameName,  Dictionary<string, int> userData , string nowName , int nowScore )
    {
        userData = load(GameName);
        userData = compare(userData, nowName, nowScore);
        userData = sort(userData);
        save(GameName , userData);

        return userData; // 최종 리턴 값 딕셔너리 형태로 받아서 다른 스크립에서 출력해야함 , Rnaking 스크립트 사용하기 
    }

    public int Maxscore(string GameName , string name)
    {
       Dictionary<string , int > dic = load(GameName);

        if (dic.ContainsKey(name)) return dic[name];
        else return 0;
    }

    Dictionary<string, int> load(string GameName)
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();

        for (int i = 0; i < MaxUser; i++) 
        {
            int score = PlayerPrefs.GetInt(GameName + i + "score", 0);
            string name = PlayerPrefs.GetString(GameName + i + "name", "");

            if (name.Length != 0) dic.Add(name, score);
        }

        return dic;
    }

    void save(string GameName , Dictionary<string,int> userdata)
    {
        List<string> name = new List<string>(userdata.Keys);
        List<int> score = new List<int>(userdata.Values);

        for (int i = 0; i < userdata.Count ; i++)
        {
            PlayerPrefs.SetInt(GameName + i + "score", score[i]);
            PlayerPrefs.SetString(GameName + i + "name", name[i]);
        }
    }

    
    Dictionary<string , int> compare(Dictionary<string,int> dic , string nowName , int nowScore)
    {
        if (dic.ContainsKey(nowName))
        {
            int org = dic[nowName];
            if (nowScore > org) dic[nowName] = nowScore;
        }

        else dic.Add(nowName, nowScore); 

        return dic;
    }

    Dictionary<string, int> sort(Dictionary<string, int> userData)
    {
        var sortedData = userData.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
        return sortedData;
    }




}