using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    scoreborad sc = new scoreborad();

    public string BestName;
    public string BestScore;

    public string GameName;
    private Dictionary<string, int> userData = new Dictionary<string, int>();

    string nowName;
    int nowScore;
    public Text[] score_txt;

    public string NowName;
    public string NowScore;

    void OnEnable()
    {
        StartCoroutine(Ranking_());
    }


    IEnumerator Ranking_()
    {
        yield return StartCoroutine(Reset());
        yield return StartCoroutine(DataSet());
    }


    IEnumerator Reset()
    {
        for (int i = 0; i < score_txt.Length; i++) { score_txt[i].text = (i + 1) + ". N/A"; }
        yield return null;
    }

    IEnumerator DataSet()
    {
        nowName = PlayerPrefs.GetString(NowName);
        nowScore = PlayerPrefs.GetInt(NowScore);
        userData = sc.scoreboard_process(GameName, userData, nowName, nowScore);

        List<string> name = new List<string>(userData.Keys);
        List<int> score = new List<int>(userData.Values);

        for (int i = 0; i < name.Count; i++)
        {
            score_txt[i].text = (i + 1) + ". Name : " + name[i] + "(" + score[i] + ")";

            if (i == 0) // 최고 점수 저장하기
            {
                PlayerPrefs.SetString(BestName, name[i]); // 최고 점수 이름 저장
                PlayerPrefs.SetInt(BestScore , score[i]);
            }
        }

        yield return null;
    }

}
