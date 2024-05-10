using System.Collections.Generic;
using UnityEngine;
using LogicBase;
using UnityEngine.UI;

public class Oth_GameManager : MonoBehaviour
{
    public int[,] Map = new int[8, 8];
    public int turn = 1;
    int[] Dir = new int[] {0,1,2,3,4,5,6,7};

    int[] SameLength;
    bool[] DifferentValue;
    int[,] SameLoc;
    List<int> ChangeDir;

    public bool GameEnd = false;
    public int Block, White;

    public Text block_text, white_text;
    public Text block_text2, white_text2;
    public Text GameEnd_text;
    public Text GameEnd_text2;

    public GameObject Right_Turn, Left_Turn;

    Logic2D logic;

    void Awake()
    {
        logic = new Logic2D();

        Map[3, 3] = 2;
        Map[3, 4] = 1;
        Map[4, 3] = 1;
        Map[4, 4] = 2;
    }

    void Start()
    {
        GameEnd_text.gameObject.SetActive(false);
        GameEnd_text2.gameObject.SetActive(false);
    }

    

    public void Next_Turn() // 다음 턴으로 바꿀때
    {
        turn = (turn == 1) ? 2 : 1;

        if (turn == 1)
        {
            Right_Turn.SetActive(true);
            Left_Turn.SetActive(false);
        }

        else
        {
            Right_Turn.SetActive(false);
            Left_Turn.SetActive(true);
        }

    }

   
    public bool DirCheck(int y , int x) // 1. 바꿀 값이 있는지 체크한다 (마우스 )
    {
        int CheckValue = (turn == 1) ? 2 : 1;
        bool isValueChang = false;

        ChangeDir = new List<int>();

        logic.Value_Set(Map, y, x, Dir, CheckValue);

        logic.Same_Length(ref SameLength); 
        logic.Different_Value(ref DifferentValue); 
     
        if (SameLength.Length == 0 || DifferentValue.Length == 0) return isValueChang; 

        for(int i=0;i< Dir.Length; i++)
        {
            if (SameLength[i] != 0 && DifferentValue[i]) // 현재 옆방향에 다른 값이 있는가? , 현재 방향에 기존 값이 있는가?
            {
                isValueChang = true;
                ChangeDir.Add(i); // 방향 저장
            }

              
        }

        return isValueChang;
    }

    public int[,] ChangeValue(int y , int x) // 2. 마우스 클릭시 배열 값을 변경한다
    {
        Map[y, x] = turn;

        int CheckValue = (turn == 1) ? 2 : 1;
        int[] Dir = new int[ChangeDir.Count]; 

        for (int i = 0; i < ChangeDir.Count; i++) Dir[i] = ChangeDir[i]; // 저장한 방향을 배열로 다시 세팅한다
       
        logic.Value_Set(Map, y, x, Dir, CheckValue); // 새롭게 지정한 방향에서만 탐색을 진행한다
        logic.Same_Location(ref SameLoc);

        for (int i = 0; i < SameLoc.GetLength(0); i++)
        {
            Map[SameLoc[i, 0], SameLoc[i, 1]] = turn;
        }

        return SameLoc;

    }


    public void GameEndCheck()
    {
        int check = 0;

        Block = 0;
        White = 0;


        for (int i =0;i<8;i++)
        {
            for(int j=0;j<8;j++)
            {
                if (Map[i, j] != 0) check++;

                if (Map[i, j] == 1) Block++;
                if (Map[i, j] == 2) White++;
            }
        }

        if (check == Map.Length)
        {
            GameEnd = true;

            if (Block > White)
            { 
                GameEnd_text.text = "Block Win!!";
                GameEnd_text2.text = "Block Win!!";

                GameEnd_text.color = Color.black;
                GameEnd_text2.color = Color.black;
            }

            else if (Block < White)
            {
                GameEnd_text.text = "Whtie Win!!";
                GameEnd_text2.text = "Whtie Win!!";
            }

            else if(Block == White)
            {
                GameEnd_text.text = "Draw";
                GameEnd_text2.text = "Draw";
            }

            GameEnd_text.gameObject.SetActive(true);
            GameEnd_text2.gameObject.SetActive(true);
            GameEnd_text.gameObject.GetComponent<AudioSource>().Play();
        }

        block_text.text = " Block : " + Block.ToString("00");
        white_text.text = " White : " + White.ToString("00");
        block_text2.text = " Block : " + Block.ToString("00");
        white_text2.text = " White : " + White.ToString("00");


    }
   



}
