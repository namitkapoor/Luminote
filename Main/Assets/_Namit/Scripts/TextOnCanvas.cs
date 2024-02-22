using UnityEngine;
using TMPro;

public class TextOnCanvas : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public SpawnNote spawnNote; // Reference to the SpawnBall script

    void Update()
    {
        
            // Set the text field to show the hit distance
            UpdateCanvasText("hello");
        
    }

    public void UpdateCanvasText(string message)
    {
        // Update the text on the canvas.
        textField.text = message;
    }
}
