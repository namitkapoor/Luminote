using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI; // MRTK UI components namespace

public class PulseSpeedControllerWithPinchSlider : MonoBehaviour
{
    public PinchSlider pulseSpeedSlider; // Reference to the MRTK pinch slider
    public Material pulseMaterial; // Direct reference to the material

    public float minPulseSpeed = 0.5f; // Minimum pulse speed
    public float maxPulseSpeed = 2.0f; // Maximum pulse speed

    private void Start()
    {
        if (pulseSpeedSlider != null && pulseMaterial != null)
        {
            // Subscribe to the OnValueUpdated event of the Pinch Slider
            pulseSpeedSlider.OnValueUpdated.AddListener(OnSliderValueUpdated);
        }
    }

    private void OnSliderValueUpdated(SliderEventData eventData)
    {
        ChangePulseSpeedBasedOnSlider(eventData.NewValue);
    }

    public void ChangePulseSpeedBasedOnSlider(float value)
    {
        // Map the slider value (0 to 1) to the desired range of pulse speeds
        float pulseSpeed = Mathf.Lerp(minPulseSpeed, maxPulseSpeed, value);

        // Apply the calculated pulse speed to the material's shader property directly
        pulseMaterial.SetFloat("_PulseSpeed", pulseSpeed);
    }
}
