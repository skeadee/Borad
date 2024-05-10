using UnityEngine;

public class BezierCurve2 
{
    float t = 1f;
    Vector3[] bezierPosition;
    int s, s2;

    public Vector3[] BezierCurveRoute(Vector3[] Routs, int count) // 바로 Vector3 형태로 받는 것 
    {
        bezierPosition = new Vector3[count];

        s = Routs.Length - 1;
        s2 = s - 1;

        t = 0f;
        float t2 = 1f / count;

        for (int i = 0; i < count; i++)
        {
            t += t2;
            bezierPosition[i] = Mathf.Pow(1 - t, s) * Routs[0] + Add(Routs) + Mathf.Pow(t, s) * Routs[s];
        }

        

        return bezierPosition;
    }


    Vector3 Add(Vector3[] ControlPoint)
    {
        Vector3 A = new Vector3();

        int s3 = s2;
        int s4 = 0;
        int Bin;

        for (int i = 0; i < s2; i++)
        {
            if (i <= (s2 / 2)) Bin = BinomialCoefficient(ControlPoint.Length - 1, ++s4);
            else Bin = BinomialCoefficient(ControlPoint.Length - 1, --s4);

            A += Bin * Mathf.Pow(t, i + 1) * Mathf.Pow(1 - t, s3--) * ControlPoint[i + 1];
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
