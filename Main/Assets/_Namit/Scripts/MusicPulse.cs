using UnityEngine;

public class MusicPulse : MonoBehaviour
{
    public AudioSource musicSource;
    public Renderer pulseRenderer;
    public float beatInterval = 0.5f; // Time in seconds for one beat. Calculate from BPM.

    private float nextBeatTime = 0f;

    void Update()
    {
        if (musicSource.isPlaying && Time.time >= nextBeatTime)
        {
            pulseRenderer.material.SetFloat("_PulseStrength", Random.Range(0.5f, 2f));
            nextBeatTime = Time.time + beatInterval;
        }
    }
}
