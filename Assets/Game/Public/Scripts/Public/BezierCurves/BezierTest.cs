using UnityEngine;
using System.Collections;

public class BezierTest : MonoBehaviour
{
    BezierCurve bez;

    public GameObject obj;
    public GameObject Routes;
    Transform[] ControlPoint;

    [Space(10f)]
    public int count = 100; // 얼마나 세분화 할것인가 , 값이 높으면 정교한 곡선이 만들어짐
    public float speed = 1.5f;
    public bool Replay = true;
    Vector3[] route;
    bool check = true;

    [Header("[Target Direction(Please check just one)]")]
    public bool Up = true;
    public bool Right = false;

    public bool Down = false;
    public bool Left = false;

    float AngleCenter = 90; // 이동하는 방향이 위쪽 90 , 오른쪽 -180 , 아래 -90 , 왼쪽 0

    int All;

    void Awake()
    {
        bez = new BezierCurve();
    }

    void Start()
    {
        All = transform.childCount;

        if (Up) AngleCenter = 90;
        else if (Right) AngleCenter = 180;
        else if (Down) AngleCenter = -90;
        else if (Left) AngleCenter = 0;
    }

    IEnumerator move(Vector3[] route)
    {
        for (int i = 0; i < route.Length; i++)
        {
            route = bez.BezierCurveStart(Routes , count);

            obj.gameObject.transform.position = route[i];

            if (i + 1 != route.Length)
            {
                Vector3 A = route[i];
                Vector3 B = route[i + 1];
                Vector3 C = A - B;

                float angle = Mathf.Atan2(C.y, C.x) * Mathf.Rad2Deg;

                obj.transform.rotation = Quaternion.Euler(0, 0, angle + AngleCenter);
            }


            yield return new WaitForSeconds(0.01f * speed);
        }

        if (Replay && check) StartCoroutine(move(route));
        else check = false;
    }


    void OnDisable()
    {
        StopAllCoroutines();
        obj.transform.position = new Vector3(0, 1f, 0);
    }

    void OnEnable()
    {
        BezierRoute bezier = GetComponent<BezierRoute>();
        ControlPoint = new Transform[bezier.ControlPoint.Length];

        for (int i = 0; i < ControlPoint.Length; i++) ControlPoint[i] = bezier.ControlPoint[i];
        route = new Vector3[bezier.count];
        route = bez.BezierCurveStart(bezier.Routes, bezier.count);

        StartCoroutine(move(route));
    }



    void Update()
    {
        if(Replay && !check)
        {
            StartCoroutine(move(route));
            check = true;
        }


        int child = transform.childCount;

        if (All < child)
        {
            All = child;
            ControlPoint = new Transform[All - 1];

            for (int i = 1; i <= All - 1; i++) ControlPoint[i - 1] = gameObject.transform.GetChild(i);
        }

        else if (All > child)
        {
            All = child;
            ControlPoint = new Transform[All - 1];

            for (int i = 1; i <= All - 1; i++) ControlPoint[i - 1] = gameObject.transform.GetChild(i);
        }

        if(Up) AngleCenter = 90;
        else if(Right) AngleCenter = 180;
        else if (Down) AngleCenter = -90;
        else if (Left) AngleCenter = 0;
         

    }



}
