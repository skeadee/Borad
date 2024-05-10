using UnityEngine;
using LogicBase;
using UnityEngine.UI;

public class Omok_GameManager : MonoBehaviour
{
    int[] Dir = new int[] { 0,1,2,3,4,5,6,7 };
    int[] SameLength;

    public int[,] Map = new int[19, 19];
    public int turn = 0;
    public bool Win = false;
    public GameObject[] GameEnd_txt;
    Text[] txt;
    public Text[] Turn_txt;

    Logic2D logic;
    Omok_Mouse Mouse;
    AudioSource Audio;

    void Awake()
    {
        logic = new Logic2D();

        for (int i=0;i<19;i++)
        {
            for(int j=0;j<19; j++)
            {
                Map[i, j] = 0;
            }
        }
    }

    void Start()
    { 
        SameLength = new int[8];
        Mouse = GetComponent<Omok_Mouse>();

        txt = new Text[2];

        for (int i = 0; i < 2; i++) txt[i] = GameEnd_txt[i].GetComponent<Text>();

        Audio = GetComponent<AudioSource>();
    }




    public void Setvalue(int y, int x)
    {
        Map[y, x] = turn + 1;
        
        logic.Value_Set(Map, Mouse.Arr_y, Mouse.Arr_x, Dir, (turn + 1));  // 1. 같은 값이 몇개 있는지 확인한다

        if(logic.Same_Length(ref SameLength)) Win_Check();
       
        turn = (turn == 0) ? 1 : 0;

        if(turn == 1)
        {
            Turn_txt[0].text = "";
            Turn_txt[1].text = "My Turn";
        }

        else
        {
            Turn_txt[1].text = "";
            Turn_txt[0].text = "My Turn";
        }


    }

    void Win_Check() // 2. 안에 들어있는 값이 4개 이상이면 게임 종료 
    {

        int w = 0; // 가로 
        int h = 0; // 세로
        int rd = 0; // 오른쪽 대각선
        int ld = 0; // 왼쪽 댁가선
       
         for (int i = 0; i < SameLength.Length; i++) 
         {
            if ((i == (int)Logic2D.Direction.D || i == (int)Logic2D.Direction.U) && SameLength[i] != 0) h += SameLength[i];
            if ((i == (int)Logic2D.Direction.R || i == (int)Logic2D.Direction.L) && SameLength[i] != 0) w += SameLength[i];
            if ((i == (int)Logic2D.Direction.UR || i == (int)Logic2D.Direction.DL) && SameLength[i] != 0) rd += SameLength[i];
            if ((i == (int)Logic2D.Direction.UL || i == (int)Logic2D.Direction.DR) && SameLength[i] != 0) rd += SameLength[i];
         }
       
        if (h >= 4 || w >= 4 || rd >= 4 || ld >= 4) Win = true;
        if (Win) Win_Text();
    }

   


    void Win_Text() // 3. turn에 맞춰서 text 출력 
    {
        Audio.Play();

        if (turn == 0)
        {
            for(int i=0;i<2;i++)
            {
                txt[i].color = Color.black;
                txt[i].text = "Block Win!";
            }
   
        }
            
        else
        {
             for(int i=0;i<2;i++)
             {
                txt[i].color = Color.white;
                txt[i].text = "White Win!";
             }
           
        }

        for (int i = 0; i < 2; i++) GameEnd_txt[i].SetActive(true);
    }
 

}
