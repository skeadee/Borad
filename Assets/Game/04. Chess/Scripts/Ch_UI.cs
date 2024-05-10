using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Ch_UI : MonoBehaviour
{
    Ch_GameManager GameManager;

    public GameObject[] Turn_txt;
    public Text[] GameEnd_txt;

    void Start()
    {
        GameManager = GetComponent<Ch_GameManager>();
    }

    public void Next_Turn(int turn)
    {
        if(turn == 1)
        {
            Turn_txt[0].SetActive(true);
            Turn_txt[1].SetActive(false);
        }

        else
        {
            Turn_txt[1].SetActive(true);
            Turn_txt[0].SetActive(false);
        }
    }


    public void Game_End(int color)
    {
        if (color == 1)
        {

            for (int i = 0; i < GameEnd_txt.Length; i++)
            {
                GameEnd_txt[i].gameObject.SetActive(true);
                GameEnd_txt[i].text = "Black Win!";
            }

        }

        else
        {
            for (int i = 0; i < GameEnd_txt.Length; i++)
            {
                GameEnd_txt[i].gameObject.SetActive(true);
                GameEnd_txt[i].text = "White Win!";
                GameEnd_txt[i].color = Color.white;
            }

        }


    }


}
