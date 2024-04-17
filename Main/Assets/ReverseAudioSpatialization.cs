using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InverseDistanceVolumeControl : MonoBehaviour
{
    public Transform playerTransform;
    public float closestDistance = 1f; // Distance at which the volume should be the lowest.
    public float farthestDistance = 10f; // Distance beyond which the volume should not increase further.
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Make sure to set spatial blend to 3D for spatialization to work.
        audioSource.spatialBlend = 1.0f;
        // Disable Unity's default volume rolloff
        audioSource.rolloffMode = AudioRolloffMode.Custom;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        float volume;

        if (distanceToPlayer <= closestDistance)
        {
            // Closest to the player, volume is at its lowest
            volume = 0f;
        }
        else if (distanceToPlayer >= farthestDistance)
        {
            // Beyond the farthest distance, volume is at its highest
            volume = 1f;
        }
        else
        {
            // Between the closest and farthest distance, volume increases with distance
            volume = (distanceToPlayer - closestDistance) / (farthestDistance - closestDistance);
        }

        audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }
}
