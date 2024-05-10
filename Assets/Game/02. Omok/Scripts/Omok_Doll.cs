using UnityEngine;

public class Omok_Doll : MonoBehaviour
{
    AudioSource Audio;
    Animator ani;
   


    public void AniOn()
    {
        ani = GetComponent<Animator>();
        ani.enabled = true;
    }

    public void SoundOn()
    {
        Audio = GetComponent<AudioSource>();
        Audio.Play();
    }

}
