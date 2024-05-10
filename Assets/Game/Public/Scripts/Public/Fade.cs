using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject par = animator.gameObject.transform.parent.gameObject;

        if (par.name == "Fade Out(Clone)") Destroy(par);

        if (par.name == "Fade In(Clone)")
        {
            int s = PlayerPrefs.GetInt("NextScene" , 0);
            SceneManager.LoadScene(s);
        }
   
    }   
}
