using UnityEngine;
using System.Collections;

public class Oth_Mouse : MonoBehaviour
{
    Oth_GameManager GameManager;

    AudioSource audioSource;

    public Vector3 Start_Pos = new Vector3(-3.16f , 3.16f , 0);
    public float One_Space = 0.9f; // 한칸 간격
    public float Half_Space = 0.45f;

    Vector3 mouse;

    [Space(15f)]
    public float Change_Speed = 0.1f;
    WaitForSeconds ChangeSpeed;
   

    float[] Loc_x = new float[8];
    float[] Loc_y = new float[8];

    public int Arr_x;
    public int Arr_y;

    GameObject[,] Map;
    public GameObject[] Dolls;
    public GameObject[] ChangeDolls;

    void Start()
    {
        GameManager = GetComponent<Oth_GameManager>();
        audioSource = GetComponent<AudioSource>();
        Map = new GameObject[GameManager.Map.GetLength(0), GameManager.Map.GetLength(1)];
        ChangeSpeed = new WaitForSeconds(Change_Speed);

        Mouse_ArrSet();
        Start_Set();
    }

    void Update()
    {
        if (GameManager.GameEnd) return;

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Arr_LocSet();


        if(Input.GetMouseButtonDown(0) && GameManager.DirCheck(Arr_y, Arr_x) && GameManager.Map[Arr_y , Arr_x] == 0) // 4. 옆에 바꿀수 있는 돌이 있는지 확인인한다
        {
            Doll_Set();

            StartCoroutine(Change_Doll(GameManager.ChangeValue(Arr_y, Arr_x)));
            audioSource.Play();

            GameManager.GameEndCheck();
            GameManager.Next_Turn();
           
                
        }
        
        
       

      
    }


    void Mouse_ArrSet() // 1. 마우스 배열 세팅
    {
        for (int i = 0; i < 8; i++)
        {
            Loc_x[i] = Start_Pos.x;
            Loc_y[i] = Start_Pos.y;

            Start_Pos.x += One_Space;
            Start_Pos.y -= One_Space;

        }

    }

    void Arr_LocSet() // 2. 마우스 위치에 따른 배열 위치 세팅
    {
        float right_x , left_x;
        float up_y, down_y;


        for (int i=0;i<8;i++)
        {
             right_x = Loc_x[i] + Half_Space;
             left_x = Loc_x[i] - Half_Space;

             up_y = Loc_y[i] + Half_Space;
             down_y = Loc_y[i] - Half_Space;

            if (mouse.x < right_x && mouse.x > left_x) Arr_x = i;
            if (mouse.y < up_y && mouse.y > down_y) Arr_y = i;
        }

    }

    void Doll_Set() // 5. trun 과 위치에 맞게 돌이 보이게 한다
    {

        GameObject doll = (GameManager.turn == 1) ? Dolls[0] : Dolls[1];
        Quaternion Loc = (GameManager.turn == 1) ? Quaternion.Euler(new Vector3(270, 0, 0)) : Quaternion.Euler(new Vector3(90, 0, 0));
        
       Map[Arr_y , Arr_x] = Instantiate(doll, new Vector3(Loc_x[Arr_x] , Loc_y[Arr_y] , 0), Loc);
 
    }

    IEnumerator Change_Doll(int[,] SameLoc)
    {
        GameObject doll = (GameManager.turn == 1) ? ChangeDolls[0] : ChangeDolls[1];
        Quaternion Loc = (GameManager.turn == 1) ? Quaternion.Euler(new Vector3(90, 0, 0)) : Quaternion.Euler(new Vector3(90, 0, 180));

        for (int i = 0; i < SameLoc.GetLength(0); i++)
        {
            Destroy(Map[ SameLoc[i, 0] ,  SameLoc[i,1]] );
            Map[SameLoc[i, 0] , SameLoc[i,1]] = Instantiate(doll, new Vector3(Loc_x[SameLoc[i, 1]], Loc_y[SameLoc[i, 0]], 0), Loc);

            yield return ChangeSpeed;
        }

    }

    void Start_Set()
    {
       
        for(int i=0;i<8;i++)
        {
            for(int j=0;j<8;j++)
            {
                Quaternion Loc = (GameManager.Map[i, j] == 1) ? Quaternion.Euler(new Vector3(270, 0, 0)) : Quaternion.Euler(new Vector3(90, 0, 0));

                if (GameManager.Map[i, j] == 1) Map[i,j] =  Instantiate(Dolls[0], new Vector2(Loc_x[j], Loc_y[i]), Loc);
                if (GameManager.Map[i, j] == 2) Map[i,j] = Instantiate(Dolls[1], new Vector2(Loc_x[j], Loc_y[i]), Loc);

            }
        }

    }

  

}
