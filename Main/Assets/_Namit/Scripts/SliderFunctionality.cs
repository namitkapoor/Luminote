using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI; // Make sure to include this namespace for MRTK UI components

public class ColorChangerWithPinchSlider : MonoBehaviour
{
    public PinchSlider colorSlider; // Reference to the MRTK pinch slider
    public Renderer objectRenderer; // Reference to the renderer of the object whose color will change

    private void Start()
    {
        if (colorSlider != null && objectRenderer != null)
        {
            // Subscribe to the OnValueUpdated event of the Pinch Slider
            colorSlider.OnValueUpdated.AddListener(OnSliderValueUpdated);
        }
    }

    private void OnSliderValueUpdated(SliderEventData eventData)
    {
        ChangeColorBasedOnSlider(eventData.NewValue);
    }

    public void ChangeColorBasedOnSlider(float value)
    {
        // Assuming value goes from 0 (green) to 1 (red), with orange in the middle
        // This creates a smooth gradient transition across the colors

        Color green = Color.green;
        Color orange = new Color(1f, 0.64f, 0f); // RGB for orange
        Color red = Color.red;

        Color targetColor;

        if (value <= 0.5f)
        {
            // Interpolate between green and orange
            targetColor = Color.Lerp(green, orange, value * 2);
        }
        else
        {
            // Interpolate between orange and red
            targetColor = Color.Lerp(orange, red, (value - 0.5f) * 2);
        }

        // Apply the calculated color to the object's material
        objectRenderer.material.color = targetColor;
    }
}
