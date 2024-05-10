using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierStart2 : MonoBehaviour
{
    public Vector3[] Route;
    public int segmentation = 100;
    public float speed = 1.5f;

    Vector3[] Routes;
    BezierCurve2 bezier;
    float AngleCenter = 90; // 위쪽 90 , 오른쪽 -180 , 아래 -90 , 왼쪽 0

    [Header("[Target Direction]")]
    public bool Up = true;
    public bool Right;
    public bool Down;
    public bool Left;

    void Start()
    {
        bezier = new BezierCurve2();
        Routes = new Vector3[segmentation];


        if (Up) AngleCenter = 90;
        else if (Right) AngleCenter = 180;
        else if (Down) AngleCenter = -90;
        else if (Left) AngleCenter = 0;


        Routes = bezier.BezierCurveRoute(Route , segmentation);

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        for(int i=0;i< Routes.Length;i++)
        {
            Routes = bezier.BezierCurveRoute(Route, segmentation);

            gameObject.transform.position = Routes[i];

            if (i + 1 != Routes.Length)
            {
                Vector3 A = Routes[i];
                Vector3 B = Routes[i + 1];
                Vector3 C = A - B;

                float angle = Mathf.Atan2(C.y, C.x) * Mathf.Rad2Deg;

                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + AngleCenter);
            }

            yield return new WaitForSeconds(0.01f * speed);
        }
    }

}
