using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vectorArray : MonoBehaviour
{
    public Vector3[] all;

    void Update()
    {
        int p = gameObject.transform.childCount;
        all = new Vector3[p];

        for (int i=0;i<p;i++)
        {
            all[i] = gameObject.transform.GetChild(i).position;
        }
    }

}
