using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]

public class BezierRoute : MonoBehaviour
{
    BezierCurve bez;

    public GameObject Routes; // 여기에다가 루트 오브젝트 넣기(밑에 자식오브젝트로 경로 있어야 함) , 점은 전체적으로 일정하게 하는게 좋음 
    [HideInInspector]public Transform[] ControlPoint;
    public int count = 100;
    Vector3[] route;
    int All;


    LineRenderer temp;

    void Awake()
    {
        bez = new BezierCurve();
    }

    void Start()
    {
        All = transform.childCount;
        route = new Vector3[count];
        route = bez.BezierCurveStart(Routes, count);

        temp = GetComponent<LineRenderer>();
        temp.startWidth = 0.3f;
        temp.endWidth = 0.3f;

        temp.SetPosition(0, route[0]);
        temp.SetPosition(1, route[route.Length - 1]);

        StartCoroutine(move(route));
    }

    IEnumerator move(Vector3[] route)
    {
        route = bez.BezierCurveStart(Routes, count);

        List<Vector3> vectorlist = new List<Vector3>(route.Length);

        for (int i = 0; i < route.Length; i++) vectorlist.Add(route[i]);

        temp.positionCount = vectorlist.Count;
        temp.SetPositions(vectorlist.ToArray());

        yield return null;

        StartCoroutine(move(route));
    }

    void OnDestroy()
    {
        int child = transform.childCount;
        Destroy(temp);
        SpriteRenderer render;

        for (int i = 0; i <= child; i++)
        {
            render = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
            Destroy(render);
        }

       
    }


    void Update()
    {
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

    }


}
