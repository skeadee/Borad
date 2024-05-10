using UnityEngine;
using System.Collections;

public class Omok_Mouse : MonoBehaviour
{
    Vector3 mouse;
    float y, x;
    public GameObject[] Doll;

    Omok_GameManager GameManager;
    [HideInInspector] public int Arr_y, Arr_x; // 배열 위치값

    float[] loc_x;
    float[] loc_y;

    public float Start_y = 2.115f, Start_x = -2.115f ; // 시작 위치 (왼쪽 모서리)

    [Space(15f)]
    public float One_Space = 0.235f; // 한칸 이동 간격
    public float Half_Space = 0.1138f; // 반칸 이동 간격

    float OverNum = 0.2f;
    bool Delay = false;

    void Start()
    {
        GameManager = GetComponent<Omok_GameManager>();

        loc_x = new float[19];
        loc_y = new float[19];

        for (int i = 0; i < 19; i++) // 비교값 세팅
        {
            loc_x[i] = Start_x;
            loc_y[i] = Start_y;

            Start_x += One_Space;
            Start_y -= One_Space;
        }

    }

    void Update()
    {
        ArrChange();
        if (Input.GetMouseButtonUp(0) && !Delay && MouseCheck() && !GameManager.Win) Doll_Set();

    }

    void ArrChange() // 1. 마우스의 위치를 배열 형태의 변수로 바꾼다
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        y = mouse.z;
        x = mouse.x;


       

        for (int i = 0; i < 19; i++)
        {

            float Right_x = loc_x[i] + Half_Space; // 중간 간격
            float Left_x = loc_x[i] - Half_Space;

            float Up_y = loc_y[i] - Half_Space;
            float Down_y = loc_y[i] + Half_Space;

            if (x <= Right_x && x >= Left_x) Arr_x = i;
            if (y <= Down_y && y >= Up_y) Arr_y = i;

        } 

    }

    bool MouseCheck() // 3. 마우스가 Map위에 있는지 확인한다
    {

        if (x < loc_x[0] - OverNum || x > loc_x[18] + OverNum || y > loc_y[0] + OverNum || y < loc_y[18] - OverNum) return false;
        else return true;

    }



    void Doll_Set() // 5. 마우스 클릭시 돌을 놓는다
    {
        if (GameManager.Map[Arr_y, Arr_x] != 0) return;

        GameManager.Setvalue(Arr_y, Arr_x);

        GameObject doll = (GameManager.turn == 0) ? Doll[1] : Doll[0];
        GameObject obj = Instantiate(doll, new Vector3(loc_x[Arr_x], 0, loc_y[Arr_y]), Quaternion.identity);
        obj.GetComponent<Omok_Doll>().AniOn();

        Delay = true;
        StartCoroutine(DelayCheck());
    }


    IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(1f);
        Delay = false;
    }






}
