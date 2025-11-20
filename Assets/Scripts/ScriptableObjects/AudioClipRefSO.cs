using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefSO", menuName = "Scriptable Objects/AudioClipRefSO")]
public class AudioClipRefSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footsteps;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip panSizzle;
    public AudioClip[] trash;
    public AudioClip[] waring;
}
