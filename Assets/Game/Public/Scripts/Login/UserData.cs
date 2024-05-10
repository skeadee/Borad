using UnityEngine;

public class UserData :MonoBehaviour
{   
    int maxUserData = 100;
    string[] id_Data;
    string[] pass_Data;

    void Awake()
    {
        maxUserData = 100;
        id_Data = new string[maxUserData];
        pass_Data = new string[maxUserData];
        Load();
    }

    public UserData() // 생성자 , 다른 곳에서 인스턴트 생성시 자동으로 실행되는 함수
    {
        maxUserData = 100;
        id_Data = new string[maxUserData];
        pass_Data = new string[maxUserData];
        //Load();
    }

   

    public void Add(string id, string password) // 여기서 새로운 유저데이터를 추가한다.
    {
        for (int i = 0; i < maxUserData; i++)
        {
            
            if (id_Data[i] == null)
            {
                id_Data[i] = id;
                pass_Data[i] = password;

                break;
            }
            
        }

        Save();
    }

    public void Save() // 여기서 유저데이터를 저장한다.
    {
        for (int i = 0; i < maxUserData; i++)
        {
            if (id_Data[i] == null) break;

            PlayerPrefs.SetString("id" + i, id_Data[i]);
            PlayerPrefs.SetString("password" + i, pass_Data[i]);
        }

        //Debug.Log("데이터 세이브 끝");
    }

    public void Load() //  여기서 데이터를 불러 온다
    {
        for (int i = 0; i < maxUserData; i++)
        {
            string id = PlayerPrefs.GetString("id" + i, "");
            string pass = PlayerPrefs.GetString("password" + i, "");

            if (id == "") break;

            id_Data[i] = id;
            pass_Data[i] = pass;
        }

       // Debug.Log("데이터 로드 끝");
    }


    //같은 데이터가 있는지 확인하는 함수
    public bool Compare(string id , string pass) // 현재 플레이어의 아이디와 패스워드를 입력 받는다
    {
        Load(); // 현재 저장되어 있는 데이터를 불러오고

        bool sameCheck = false; // 같은 이름이 있는지 체크 하는 변수

        for(int i=0 ; i < maxUserData ; i++)
        {
            if (id == id_Data[i] && pass == pass_Data[i])
            {
                sameCheck = true;
                break;
            }
       
        }

         return sameCheck; // true면 아이디가 있고 false면 아이디가 없다
    }


}
