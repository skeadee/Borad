using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Mask : MonoBehaviour
{
    public float speed = 1f;
    public float Max_Size = 300f;

    public RectTransform target;
    Vector2 size;


    void Start()
    {
        StartCoroutine(Fade_OUT());

    }

    public void NextScene(int Scene)
    {
        target.gameObject.SetActive(true);
        StartCoroutine(Fade_IN(Scene)); 

    }

    IEnumerator Fade_IN(int Scene)
    {
        size.x = target.rect.width;
        size.y = target.rect.width;


        while (true)
        {
            if (target.sizeDelta.x < 0 || target.sizeDelta.y < 0) break;

            target.sizeDelta = size;
            size.x -= Time.deltaTime * speed;
            size.y -= Time.deltaTime * speed;

            yield return null;
        }


        SceneManager.LoadScene(Scene);

    }

    IEnumerator Fade_OUT()
    {
        target.sizeDelta = Vector2.zero;

        while (true)
        {
            if (target.sizeDelta.x > Max_Size) break;

            target.sizeDelta = size;
            size.x += Time.deltaTime * speed;
            size.y += Time.deltaTime * speed;

            yield return null;
        }

        target.gameObject.SetActive(false);
    }



}
