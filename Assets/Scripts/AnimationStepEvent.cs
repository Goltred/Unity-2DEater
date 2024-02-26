using UnityEngine;

public class AnimationStepEvent : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    
    public void PlayStep()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
