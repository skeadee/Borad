using System.Collections;
using UnityEngine;


public class Oth_Doll : MonoBehaviour
{
    public Vector3 angle;

    void Start()
    {
        StartCoroutine(Set_Doll());
    }

    IEnumerator Set_Doll()
    {
        Quaternion Target_Loc = transform.rotation;
        angle = transform.eulerAngles;


        for (int i = 0; i < 180; i++)
        {
            // transform.Rotate(new Vector3(0, 0, 1));
            transform.Rotate(new Vector3(1, 0, 0));
            angle = transform.eulerAngles;
            yield return null;
        }

    }

 
}
