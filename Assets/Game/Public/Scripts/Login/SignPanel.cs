using UnityEngine;
using UnityEngine.UI;

public class SignPanel : MonoBehaviour
{
    public InputField id , pass;
    UserData userData;
    public Text errorText; // 패널 밑쪽에 텍스트

    string blank = "Please enter all ID/password"; // 입력이 공백일때
    string same = "The ID that already exists"; // 같은 아이디와 비번이 있을때
    string create = "Sign up is complete";

    void Start()
    {
        userData = new UserData();
    }

    public void CreateButton() // 아이디를 생성하는 버튼 
    {
        errorText.text = ""; // 버튼을 누를 때 마다 글자 초기화

        if (id.text.Length == 0 || pass.text.Length == 0)
        {
            PanelText();
            errorText.text = blank;
            return;
        }

        bool check = userData.Compare(id.text , pass.text); // 같은 아이디와 비번을 가지고 있는 체크

        if (check)
        {
            PanelText();
            errorText.text = same;
        }

        else // 아이디가 생성이 가능하다고 뜨게 만들기
        {
            userData.Add(id.text, pass.text);
            PanelText();
            errorText.text = create;
        }
      
    }

    void PanelText()
    {
        GameObject txt = errorText.gameObject;

       if(txt.activeSelf == transform)
       {
            txt.SetActive(false);
            txt.SetActive(true);
       }

       else
       { 
            txt.SetActive(true);
       }

    }

}
