using UnityEngine;
using UnityEngine.UI;

public class MainGames : MonoBehaviour
{
    int buttonNumber;
    public GameObject[] Games;
    public GameObject[] videos;

    public string[] bestNames; // 각 게임의 저장해둔 최고 점수 이름 , 게임 순서에 맞게 넣기
    public string[] bestScore; // 각 게임의 저장해둔 최고 점수  

    public string[] GamesName; // 게임 이름
    Text gameNametxt;
    Text best_txt;

    public GameObject Mark; // 손가락

    RectTransform pos;
    int markCheck; // 현재 손가락 마크가 있는 위치

    public GameObject playButton;

    void Awake()
    {
        gameNametxt = GameObject.Find("GameName").GetComponent<Text>();
        best_txt = GameObject.Find("BestScoretxt").GetComponent<Text>();
    }

    void Start()
    { 
        buttonNumber = 0;
        markCheck = 0;
        pos = Mark.GetComponent<RectTransform>();

        Instantiate(Mark, Games[0].transform); // 손가락 위치 첫번째 게임
        videos[0].SetActive(true); // 첫번째 비디오 활성화 하기
        best_text_Set(); // 첫번째 게임 최고 플레이어 텍스트 출력
    }

    public void mouseOn(int Num) // 마우스가 게임 아이콘에 닿았을 때
    {
        buttonNumber = Num; // 버튼 위치를 수정한다
    }

    void Update()
    {
        keyBorad();

        if (buttonNumber != markCheck)
        {
            reset(); // 손가락 위치를 바꾸기 전에 변경해야 할 것들
            markCheck = buttonNumber; // 새롭게 손가락 위치를 세팅한다

            markSet();
            textSet();
            videoSet();
            best_text_Set();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            playButton.GetComponent<ButtonImgChange>().click_In();
        }

        if(Input.GetKeyUp(KeyCode.Return))
        {
            playButton.GetComponent<ButtonImgChange>().click_Out();
            NextScene();
        }

    }

    void keyBorad()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && buttonNumber + 1 < Games.Length)
        {
            buttonNumber += 1;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && buttonNumber - 1 > -1)
        {
            buttonNumber += -1;
        }



    }

    void reset() // 없애야하는 것 
    {
        GameObject mar = Games[markCheck].transform.Find("mark(Clone)").gameObject;
        Destroy(mar); // 현재 손가락을 지우고

        videos[markCheck].SetActive(false); // 현재 위치의 동영상 비활성화 하기
    }


    void markSet()
    {
        Instantiate(Mark, Games[markCheck].transform); // 새로운 위치에 손가락을 손가락 위치로 옮긴다   
    }

    void textSet()
    {
        gameNametxt.text = GamesName[markCheck];
    }

    void videoSet()
    {
        videos[markCheck].SetActive(true);
    }

    void best_text_Set()
    {
        string name = PlayerPrefs.GetString(bestNames[markCheck] , "None");
        int score = PlayerPrefs.GetInt(bestScore[markCheck] , 0);

        best_txt.text = name + "(" + score + ")";
    }

    public void NextScene()
    { 
        GameObject.Find("GameManager").GetComponent<LoadScene>().loadScene(markCheck + 3);
    }

    



}
