using System.Linq;
using UnityEngine;

public class PlayerWalkSoundPlayer : AudioPlayer
{
    [Header("Audio Sets")]
    [SerializeField] private AudioSet walkAudioSetOutside;
    [SerializeField] private AudioSet walkAudioSetInside;
    [SerializeField] private AudioSet runAudioSetOutside;
    [SerializeField] private AudioSet runAudioSetInside;

    [Header("Material Samples")]
    [SerializeField] private Material outsideMat;
    [SerializeField] private Material insideMat;
    
    public void PlayWalkSound()
    {
        StopRunSound();
        if (audioSource.isPlaying) return;

        Physics.Raycast(transform.position, Vector3.down, out var hit, 5f, LayerMask.GetMask("Ground"));

        if (!hit.collider) return;
        if (hit.transform.GetComponent<Renderer>().material == outsideMat) Play(walkAudioSetOutside);
        else if (hit.transform.GetComponent<Renderer>().material == insideMat) Play(walkAudioSetInside);
    }

    public void StopWalkSound()
    {
        if (walkAudioSetOutside.Set.Contains(audioSource.clip) || walkAudioSetInside.Set.Contains(audioSource.clip)) audioSource.Stop();
    }

    public void PlayRunSound()
    {
        StopWalkSound();
        if (audioSource.isPlaying) return;

        Physics.Raycast(transform.position, Vector3.down, out var hit, 5f, LayerMask.GetMask("Ground"));

        if (hit.transform.GetComponent<Renderer>().material == outsideMat) Play(runAudioSetOutside);
        else if (hit.transform.GetComponent<Renderer>().material == insideMat) Play(runAudioSetInside);
    }

    public void StopRunSound()
    {
        if (runAudioSetOutside.Set.Contains(audioSource.clip) || runAudioSetInside.Set.Contains(audioSource.clip)) audioSource.Stop();
    }
}
