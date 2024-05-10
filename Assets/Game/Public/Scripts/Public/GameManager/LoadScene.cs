using UnityEngine;

public class LoadScene : MonoBehaviour
{
    GameObject fade_In, fade_Out;

    void Start()
    {
        fade_In = Resources.Load<GameObject>("Fade/Fade In");
        fade_Out = Resources.Load<GameObject>("Fade/Fade Out");

        Fade_Out();
    }

    public void loadScene(int sceneNumber) // 씬 이동시 Fade_In인 작동하는 함수
    {
        Application.targetFrameRate = 30;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("NextScene", sceneNumber);
        Fade_In();     
    }


    public void Fade_In() // 씬 끝날 때 작동
    {
       Instantiate(fade_In);    
    }

    public void Fade_Out() // 씬 시작시 작동 
    {
       Instantiate(fade_Out);
    }

 
}
