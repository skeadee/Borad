using UnityEngine;
using System.Collections;

public class BezierStart : MonoBehaviour
{
    BezierCurve bez;

    [Tooltip("이동할 오브젝트 넣기")] public GameObject target;
    public GameObject Routes;

    [Space(10f)]
    [Tooltip("세분화")] public int count = 100;
    public float speed = 1.5f;
    float AngleCenter = 90; // 위쪽 90 , 오른쪽 -180 , 아래 -90 , 왼쪽 0
    public bool Replay = true;

    [Header("[Target Direction]")]
    public bool Up = true;
    public bool Right;
    public bool Down;
    public bool Left;

    bool check = true;
    Vector3[] route;

    void Awake()
    {
        bez = new BezierCurve();
    }

    void Start()
    {
        route = new Vector3[count];
        route = bez.BezierCurveStart(Routes, count);

        if (Up) AngleCenter = 90;
        else if (Right) AngleCenter = 180;
        else if (Down) AngleCenter = -90;
        else if (Left) AngleCenter = 0;

        StartCoroutine(move(route));
    }

    IEnumerator move(Vector3[] route)
    {
        for (int i = 0; i < route.Length; i++)
        {
            target.transform.position = route[i];

            if (i + 1 != route.Length)
            {
                Vector3 A = route[i];
                Vector3 B = route[i + 1];
                Vector3 C = A - B;

                float angle = Mathf.Atan2(C.y, C.x) * Mathf.Rad2Deg;

                target.transform.rotation = Quaternion.Euler(0, 0, angle + AngleCenter);
            }


            yield return new WaitForSeconds(0.01f * speed);
        }

        if (Replay && check) StartCoroutine(move(route));
        else check = false;
    }

    void Update()
    {
        if (Replay && !check)
        {
            StartCoroutine(move(route));
            check = true;
        }
    }



}
