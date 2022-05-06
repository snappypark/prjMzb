using System.Runtime.CompilerServices;
using UnityEngine;

public class audio : MonoBehaviour
{
    [SerializeField] AudioSource _source;

    public AudioSource GetSource() { return _source; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PlaySound()
    {
        _source.Play();
    }
}
