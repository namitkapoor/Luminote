using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI; // Required for MRTK UI components

public class ShaderOpacityController : MonoBehaviour
{
    public PinchSlider opacitySlider; // Reference to the MRTK pinch slider
    public Material targetMaterial; // Reference to the renderer of the object whose opacity will change

    private void Start()
    {
        if (opacitySlider != null && targetMaterial != null)
        {
            // Subscribe to the OnValueUpdated event of the Pinch Slider
            opacitySlider.OnValueUpdated.AddListener(OnSliderValueUpdated);
        }
    }

    private void OnSliderValueUpdated(SliderEventData eventData)
    {
        ChangeOpacityBasedOnSlider(eventData.NewValue);
    }

    public void ChangeOpacityBasedOnSlider(float value)
    {
        // Ensure the value is between 0 (fully transparent) and 1 (fully opaque)
        float opacity = Mathf.Clamp(value, 0f, 1f);

        // Apply the calculated opacity to the object's material
        targetMaterial.SetFloat("_Opacity", opacity);
    }
}
