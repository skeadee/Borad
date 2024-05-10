using UnityEngine;
using UnityEngine.UI;

public class ButtonImgChange : MonoBehaviour
{
    public Sprite[] img;

    public void click_In()
    {
        GetComponent<Image>().sprite = img[1];
    }

    public void click_Out()
    {
        GetComponent<Image>().sprite = img[0];
    }


}
