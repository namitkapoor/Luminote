using UnityEngine;

public class ColorChangerForButton : MonoBehaviour
{
    public Renderer objectRenderer; // Reference to the renderer of the object whose color will change
    public Color colorToChangeTo; // The color to change to, set this in the inspector

    // Function to be called by button's OnClick event
    public void ChangeColor()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = colorToChangeTo;
        }
    }
}
