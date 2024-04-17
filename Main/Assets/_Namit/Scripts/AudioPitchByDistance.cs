using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPitchByDistance : MonoBehaviour
{
    public Transform playerTransform;
    public float minDistance = 1f; // The distance at which the pitch is at its minimum.
    public float maxDistance = 10f; // The distance at which the pitch is at its maximum.
    public float minPitch = 0.5f; // Minimum pitch value.
    public float maxPitch = 2.0f; // Maximum pitch value.
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Set the spatial blend to 3D for the spatialization to work.
        audioSource.spatialBlend = 1.0f;
    }

    void Update()
    {
        // Calculate the distance from the player to the audio source.
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        // Normalize the distance within the minDistance and maxDistance range.
        float normalizedDistance = Mathf.InverseLerp(minDistance, maxDistance, distance);
        // Lerp the pitch value based on the normalized distance.
        audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedDistance);
    }
}
