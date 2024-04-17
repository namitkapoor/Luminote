using UnityEngine;
using UnityEngine.UI; // For UI elements like Image
using TMPro; // For TextMeshPro elements

public class ToggleUIElements : MonoBehaviour
{
    // Arrays to hold references to multiple Image components
    public Image[] targetImages;
    // Arrays to hold references to multiple TMP_InputField components
    public TMP_InputField[] targetInputFields;

    // This method toggles the visibility of the Images and the interactability of the TMP_InputFields
    public void ToggleElements()
    {
        // Toggle Image visibility for each Image in the array
        foreach (var image in targetImages)
        {
            if (image != null)
            {
                image.enabled = !image.enabled;
            }
        }

        // Toggle TMP_InputField interactability for each input field in the array
        foreach (var inputField in targetInputFields)
        {
            if (inputField != null)
            {
                inputField.interactable = !inputField.interactable;
            }
        }
    }
}
