using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    public int s;

    void Start()
    {
        Invoke("SceneLoad", 3);
    }


    void SceneLoad()
    { 
        SceneManager.LoadScene(1);
    }
}
