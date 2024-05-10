using UnityEngine;

public class MainCanvs : MonoBehaviour
{
    MainVideo video;
    RectTransform Contenet;

    int interval = 500; // 간격 
    int i; // 현재 보이는 버튼 위치 

    Vector3 orgLocation; // 현재 위치
    Vector3 targetLocation; // 목표위치

    public int GameCount; // 게임 갯수 입력하기
    int GameValue;

  
    public float moveSpeed = 1f; // 버튼 도착하는 시간
    float moveTime = 0f;

    public bool moving = false; // 현재 버튼이 움직이고 있는지 체크하는 변수


    void Awake()
    {
        Contenet = GameObject.Find("Content").GetComponent<RectTransform>();
        video = GameObject.Find("Video Panel").GetComponent<MainVideo>();
    }

    void Start()
    {
        orgLocation = new Vector3(0, 0, 0);
        i = 0;
        moveTime = 0f;
        GameValue = GameCount * interval;
    }

    void Update()
    {
        moveTime += Time.deltaTime;
        float t = moveTime / moveSpeed;

       Contenet.anchoredPosition = Vector2.Lerp(orgLocation, targetLocation, t);

        if (Contenet.anchoredPosition.y % interval == 0)
        {
            moving = true;
            video.videoOn(i / interval);
        }


        else
        {
            moving = false;
           
        }

        
       
    }


    public void UpButton()
    {
        if (i - interval < 0 || !moving) return;

        video.videoEnd(i / interval);
        orgLocation = new Vector3(0, i);
        moveTime = 0f;

        i -= interval;
        targetLocation = new Vector3(0, i);
    }

    public void DownButton()
    {
        if (i + interval >= GameValue || !moving) return;

        video.videoEnd(i / interval);
        orgLocation = new Vector3(0, i);
        moveTime = 0f;

        i += interval;
        targetLocation = new Vector3(0, i);
    }


}
