using UnityEngine;

public class ToggleAnimation : MonoBehaviour
{
    public Animator animator; // Assign the Animator component of your target GameObject.

    // This method is called when the button is pressed.
    public void Toggle()
    {
        // Get the current state of the 'IsOn' parameter.
        bool isOn = animator.GetBool("IsOn");
        // Toggle the 'IsOn' parameter.
        animator.SetBool("IsOn", !isOn);
    }
}
