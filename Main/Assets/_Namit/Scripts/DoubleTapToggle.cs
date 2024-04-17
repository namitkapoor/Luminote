using UnityEngine;

public class DoubleTapToToggleAnimation : MonoBehaviour
{
    public ToggleAnimation toggleAnimationScript; // Assign this in the inspector
    private float lastButtonPressTime = 0f;
    private float doubleTapThreshold = 0.3f;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (Time.time - lastButtonPressTime <= doubleTapThreshold)
            {
                // Double-tap detected, call the Toggle() method on the ToggleAnimation script
                if (toggleAnimationScript != null)
                {
                    toggleAnimationScript.Toggle();
                }
                else
                {
                    Debug.LogWarning("ToggleAnimation script not assigned.");
                }
            }
            lastButtonPressTime = Time.time;
        }
    }
}
