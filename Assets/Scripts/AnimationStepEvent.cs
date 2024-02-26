using UnityEngine;

/// <summary>
///  Used to handle animation events in the running animation of the character, allowing us to play the step sounds
/// based on the animation instead of looping or other coding magic
/// </summary>
public class AnimationStepEvent : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    
    public void PlayStep()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
