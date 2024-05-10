using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public void mouseOn() 
    {
        GetComponentInChildren<Text>().color = new Color(195 / 255f, 195 / 255f, 195 / 255f);
    }

    public void mouseOut()
    {
        GetComponentInChildren<Text>().color = new Color(58 / 255f,43 / 255f,43 / 255f);
    }

 

}
