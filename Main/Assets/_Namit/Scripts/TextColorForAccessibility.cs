using UnityEngine;
using UnityEngine.UI; // Required for working with UI Text components

public class TextColorForAccessibility : MonoBehaviour
{
    public Color backgroundColor; // The current background color
    public Color lightTextColor = Color.black; // Text color for light backgrounds
    public Color darkTextColor = Color.white; // Text color for dark backgrounds

    // Array of Text components to change color
    public Text[] textComponents;

    void Update()
    {
        // Calculate the luminance of the background color
        float luminance = 0.2126f * backgroundColor.r + 0.7152f * backgroundColor.g + 0.0722f * backgroundColor.b;

        // Determine the text color based on the luminance
        Color textColor = luminance > 0.5f ? darkTextColor : lightTextColor;

        // Apply the text color to all Text components in the array
        foreach (var textComponent in textComponents)
        {
            if (textComponent != null)
            {
                textComponent.color = textColor;
            }
        }
    }

    // Call this method when the background color changes
    public void UpdateBackgroundColor(Color newBackgroundColor)
    {
        backgroundColor = newBackgroundColor;
    }
}
