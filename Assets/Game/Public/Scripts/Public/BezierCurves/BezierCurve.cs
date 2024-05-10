using UnityEngine;

public class BezierCurve
{
    float t = 1f;
    Vector3[] bezierPosition;
    int s, s2;

    public Vector3[] BezierCurveStart(GameObject Routs, int count) // 이동할 위치(자식 오브젝트에 위치 점만 있어야 함),  나눌 숫자
    {
        bezierPosition = new Vector3[count];

        Transform[] ControlPoint = new Transform[Routs.transform.childCount];

        for(int i = 0 ; i < Routs.transform.childCount;i++)
        {
            ControlPoint[i] = Routs.transform.GetChild(i);
        }

        s = ControlPoint.Length - 1;
        s2 = s - 1;

        t = 0f;
        float t2 = 1f / count;

        for (int i = 0; i < count; i++)
        {
            t += t2;
            bezierPosition[i] = Mathf.Pow(1 - t, s) * ControlPoint[0].position + Add(ControlPoint) + Mathf.Pow(t, s) * ControlPoint[s].position;
        }

        return bezierPosition;

    }

    Vector3 Add(Transform[] ControlPoint)
    {
        Vector3 A = new Vector3();

        int s3 = s2;
        int s4 = 0;
        int Bin;

        for (int i = 0; i < s2; i++)
        {
            if (i <= (s2 / 2)) Bin = BinomialCoefficient(ControlPoint.Length - 1, ++s4);
            else Bin = BinomialCoefficient(ControlPoint.Length - 1, --s4);

            A += Bin * Mathf.Pow(t, i + 1) * Mathf.Pow(1 - t, s3--) * ControlPoint[i + 1].position;
        }

        return A;
    }

    int BinomialCoefficient(int a, int b) // a 세로 , b가 가로 , 이항 계수 구하는 함수
    {
        int Answer = (factorial(a)) / (factorial(b) * factorial(a - b));
        return Answer;
    }

    int factorial(int f) // 팩토리얼 함수
    {
        if (f == 0 || f == 1) return 1;
        return f * factorial(f - 1);
    }





}


