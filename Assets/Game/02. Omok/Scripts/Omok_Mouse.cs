using UnityEngine;
using System.Collections;

public class Omok_Mouse : MonoBehaviour
{
    Vector3 mouse;
    float y, x;
    public GameObject[] Doll;

    Omok_GameManager GameManager;
    [HideInInspector] public int Arr_y, Arr_x; // �迭 ��ġ��

    float[] loc_x;
    float[] loc_y;

    public float Start_y = 2.115f, Start_x = -2.115f ; // ���� ��ġ (���� �𼭸�)

    [Space(15f)]
    public float One_Space = 0.235f; // ��ĭ �̵� ����
    public float Half_Space = 0.1138f; // ��ĭ �̵� ����

    float OverNum = 0.2f;
    bool Delay = false;

    void Start()
    {
        GameManager = GetComponent<Omok_GameManager>();

        loc_x = new float[19];
        loc_y = new float[19];

        for (int i = 0; i < 19; i++) // �񱳰� ����
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

    void ArrChange() // 1. ���콺�� ��ġ�� �迭 ������ ������ �ٲ۴�
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        y = mouse.z;
        x = mouse.x;


       

        for (int i = 0; i < 19; i++)
        {

            float Right_x = loc_x[i] + Half_Space; // �߰� ����
            float Left_x = loc_x[i] - Half_Space;

            float Up_y = loc_y[i] - Half_Space;
            float Down_y = loc_y[i] + Half_Space;

            if (x <= Right_x && x >= Left_x) Arr_x = i;
            if (y <= Down_y && y >= Up_y) Arr_y = i;

        } 

    }

    bool MouseCheck() // 3. ���콺�� Map���� �ִ��� Ȯ���Ѵ�
    {

        if (x < loc_x[0] - OverNum || x > loc_x[18] + OverNum || y > loc_y[0] + OverNum || y < loc_y[18] - OverNum) return false;
        else return true;

    }



    void Doll_Set() // 5. ���콺 Ŭ���� ���� ���´�
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
