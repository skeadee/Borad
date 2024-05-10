using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginPanel : MonoBehaviour
{
    public InputField id; // 현재 아이디
    public InputField password; // 현재 패스워드
    public GameObject ErrorText;

    UserData userData = new UserData();
    void Start()
    {
        userData = new UserData();
        ErrorText.SetActive(false);
    }

    public void play(int nextScene) // play 버튼 
    {
       bool check = userData.Compare(id.text, password.text);

        if(check) // 로그인이 가능하다면
        {
            PlayerPrefs.SetString("NowName", id.text); // 현재 플레이 하고 있는 플레이어의 닉네임
            SceneManager.LoadScene(nextScene);
        }

        else // 로그인이 안된다면
        {
            ErrorCheck();
            textClear();
        }
    }

    void ErrorCheck() // 에러텍스트 띄우는 함수
    {
        if(ErrorText.activeSelf == true) // 로그인이 안되는 상태로 연속으로 클릭했을 때
        {
            ErrorText.SetActive(false);
            ErrorText.SetActive(true);
        }

        else ErrorText.SetActive(true);
    }

    void textClear() // 텍스트 클리어 함수
    {
        id.text = "";
        password.text = "";
    }


}
