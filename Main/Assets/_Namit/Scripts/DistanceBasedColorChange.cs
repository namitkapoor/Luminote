using UnityEngine;

public class DistanceBasedColorChange : MonoBehaviour
{
    public Transform controllerTransform; // Assign the controller's transform here
    public float maxDistance = 5.0f; // Maximum distance at which color change occurs

    private Renderer objRenderer; // Renderer of the object to change color
    private Color originalColor; // To store the original color of the object

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            originalColor = objRenderer.material.color; // Store the original color
        }
    }

    void Update()
    {
        // Perform a raycast from the controller
        if (Physics.Raycast(controllerTransform.position, controllerTransform.forward, out RaycastHit hit, maxDistance))
        {
            // Check if this object was hit
            if (hit.transform == transform)
            {
                // Calculate the distance from the controller to the hit point
                float distance = hit.distance;

                // Calculate color based on distance
                Color newColor = CalculateColorBasedOnDistance(distance, maxDistance);
                objRenderer.material.color = newColor;
            }
            else
            {
                // If the raycast hits a different object, revert to the original color
                objRenderer.material.color = originalColor;
            }
        }
        else
        {
            // If the raycast doesn't hit anything, revert to the original color
            objRenderer.material.color = originalColor;
        }
    }

    Color CalculateColorBasedOnDistance(float distance, float maxDistance)
    {
        // Simple linear interpolation between two colors based on distance
        float t = Mathf.Clamp01(distance / maxDistance);
        // Example: Closer is red, farther away is green
        return Color.Lerp(Color.red, Color.green, t);
    }
}
