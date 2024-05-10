using UnityEngine;
using UnityEngine.UI;

public class count : MonoBehaviour
{
    public GameObject Count;

    Text txt;
    RectTransform scale;

    float t;
    public bool stop;

    float x  = 1 , y = 1;
    public float waitTime = 0;  // 추가로 기다리는 시간

    void Start()
    {
      txt = Count.GetComponent<Text>();
      scale = Count.GetComponent<RectTransform>();
      
      t = 4;
      txt.text = "3";
    }

    void FixedUpdate()
    {
        waitTime -= Time.deltaTime;
        if (waitTime > 0) return;

        if (stop) return;

        t -= Time.deltaTime;
        x -= Time.deltaTime;
        y -= Time.deltaTime;

        if (x < 0) { x = 1; y = 1; }

        scale.localScale = new Vector3(x, y, 1);

        if (t < 1) txt.text = "GO";
        else if (t < 2) txt.text = "1";
        else if (t < 3) txt.text = "2";
            
        if (t < 0) Destroy(gameObject);
    }

    public void CountStop()
    {
        stop = true;
    }

    public void CountStart()
    {
        stop = false;
    }

}
