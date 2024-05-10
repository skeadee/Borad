using UnityEngine;
using UnityEngine.Video;

public class MainVideo : MonoBehaviour
{
    public GameObject[] videos;

    public void videoOn(int value)
    {
        if (videos[value] == null) return;

        videos[value].SetActive(true);
    }

    public void videoEnd(int value)
    {
        if (videos[value] == null) return;

        videos[value].SetActive(false);
    }

}
    


