using UnityEngine;

public class MainButton : MonoBehaviour
{
    public RectTransform LoginPanel;
    public RectTransform SignPanel;


    public float timer = 2f; // 오브젝트가 타겟에 도착하는 시간
    Vector3 target; // UI창 도착지점

    float moveTime = 0;

    Vector3 UI; // 목표가 되는 ui 위치 , ui가 움직이기 시작하는 위치
    Vector3 LoginStart = new Vector3(0, 1100f, 0); // 로그인 창 처음 위치
    Vector3 SignStart = new Vector3(0, -1100f, 0); // 회원가입창 처음 위치
    Vector3 center = Vector3.zero; // 화면 중앙

    bool panelCheck = false; // 현재 로그인 창이 움직이고 있는지 체크하는 변수
    bool logCheck = false; // 로그인창 체크 변수
    bool signCheck = false; // 회원가입창 체크 변수

    RectTransform objRect; // 업데이트 문에서 위치를 움직일 오브젝트


    void Start()
    {
        UI = LoginStart;
        target = LoginStart;

      
    }

    public void LogIn() // 로그인창을 가운데로 불러 오는 함수
    {
        if (!panelCheck) return; // 패널창이 움직이는 중이면 리턴

        objRect = LoginPanel; // 움직일 오브젝트 선택 
        UI = objRect.anchoredPosition; // 오브젝트가 움직이기 시작할 위치 선택
        moveTime = 0f; // 시간 초기화 

        if(!logCheck) { target = center; logCheck = true; }
        else{ target = LoginStart; logCheck = false; }  
    }

    public void SignIn()
    {
        if (!panelCheck) return; 

        objRect = SignPanel;
        UI = objRect.anchoredPosition;
        moveTime = 0f;

        if(!signCheck){ target = center; signCheck = true; }
        else { target = SignStart; signCheck = false; }        
    }


    void Update()
    {
        moveTime += Time.deltaTime;
        float t = moveTime / timer;

        if(objRect != null) objRect.anchoredPosition = Vector3.Lerp(UI , target , t);

        if (t > 1) panelCheck = true; // 창이 움직임이 끝났다
        else if(t < 1) panelCheck = false; // 창이 움직이는 중이다
    }


}
