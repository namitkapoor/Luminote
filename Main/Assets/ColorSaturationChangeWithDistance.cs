using UnityEngine;
using TMPro; // Required for TextMeshPro components

public class ColorSaturationAndTextAccessibility : MonoBehaviour
{
    public Transform playerTransform;
    public Renderer objectRenderer; // Reference to the renderer of the object whose color will change
    public Color saturatedColor; // The fully saturated color, set this in the inspector
    public Color desaturatedColor; // The desaturated color, set this in the inspector
    public float minDistance = 1f; // The distance at which the color is fully saturated.
    public float maxDistance = 10f; // Beyond this distance, the color will remain at the desaturated color.
    public TextMeshProUGUI[] textComponents; // Array of TextMeshProUGUI components to change color
    public Color lightTextColor = Color.black; // Text color for light backgrounds
    public Color darkTextColor = Color.white; // Text color for dark backgrounds

    private Color currentBackgroundColor; // Tracks the current background color

    void Update()
    {
        if (objectRenderer != null)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if (distanceToPlayer >= maxDistance)
            {
                currentBackgroundColor = desaturatedColor;
            }
            else
            {
                float lerpFactor = Mathf.InverseLerp(minDistance, maxDistance, distanceToPlayer);
                currentBackgroundColor = Color.Lerp(saturatedColor, desaturatedColor, lerpFactor);
            }
            objectRenderer.material.color = currentBackgroundColor;
            UpdateTextColors();
        }
    }

    void UpdateTextColors()
    {
        float luminance = CalculateLuminance(currentBackgroundColor);
        Color textColor = luminance > 0.5f ? darkTextColor : lightTextColor;

        foreach (var textComponent in textComponents)
        {
            if (textComponent != null)
            {
                textComponent.color = textColor;
            }
        }
    }

    float CalculateLuminance(Color color)
    {
        // Calculates the perceived luminance of the color
        return 0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b;
    }
}
