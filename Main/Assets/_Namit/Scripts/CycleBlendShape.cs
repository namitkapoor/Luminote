using UnityEngine;

public class BlendShapeCycler : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private int currentBlendShapeIndex = 0;
    private int totalBlendShapes;
    public Transform userTransform; // Assign the user's transform in the Inspector
    public float maxDistance = 10f; // Maximum distance for the slowest change interval
    private float timer = 0f;

    // The range for changeInterval depending on the distance
    public float minChangeInterval = 0.005f;
    public float maxChangeInterval = 0.05f;
    private float changeInterval;

    void Start()
    {
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        if (skinnedMeshRenderer != null)
        {
            totalBlendShapes = skinnedMeshRenderer.sharedMesh.blendShapeCount;
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer is not attached to the GameObject.");
        }
    }

    void Update()
    {
        if (userTransform != null)
        {
            // Calculate distance to the user
            float distance = Vector3.Distance(transform.position, userTransform.position);

            // Normalize the distance to a 0-1 scale
            float normalizedDistance = Mathf.Clamp(distance / maxDistance, 0f, 1f);

            // Interpolate the changeInterval based on the distance
            changeInterval = Mathf.Lerp(minChangeInterval, maxChangeInterval, normalizedDistance);
        }

        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            CycleBlendShape();
            timer = 0f; // Reset the timer
        }
    }

    void CycleBlendShape()
    {
        if (skinnedMeshRenderer == null) return;

        // Reset the current blend shape weight to 0
        skinnedMeshRenderer.SetBlendShapeWeight(currentBlendShapeIndex, 0f);

        // Move to the next blend shape index
        currentBlendShapeIndex = (currentBlendShapeIndex + 1) % totalBlendShapes;

        // Set the next blend shape weight to 100 (fully activated)
        skinnedMeshRenderer.SetBlendShapeWeight(currentBlendShapeIndex, 100f);
    }
}
