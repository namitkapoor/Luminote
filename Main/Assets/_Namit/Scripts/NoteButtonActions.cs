using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomButtonActions : MonoBehaviour
{
    public Button toggleTargetButton;
    private int firstButtonPressCount = 0;
    private int secondButtonPressCount = 0;
    private int toggleButtonPressCount = 0;
    private int confirmButtonPressCount = 0; // Counter for the confirm button

    public TextMeshProUGUI firstTextField;
    public Button firstButton;

    public TextMeshProUGUI secondTextField;
    public Button secondButton;
    public GameObject Calendar;

    // Confirm button and its related UI elements (if any)
    //public TextMeshProUGUI confirmButtonTextField; // Optional, based on your UI design
    //public Button confirmButton;

    public void UpdateFirstButton()
    {
        firstButtonPressCount++;
        // Check if the press count is odd
        if (firstButtonPressCount % 2 != 0)
        {
            UpdateTextAndChangeButtonTransparency(firstTextField, firstButton, "Psychology 101 Research Paper Submission ", 0f);
        }
    }

    public void UpdateSecondButton()
    {
        secondButtonPressCount++;
        // Check if the press count is odd
        if (secondButtonPressCount % 2 != 0)
        {
            UpdateTextAndChangeButtonTransparency(secondTextField, secondButton, "Topic: 'The Impact of Social Media on Adolescent Mental Health.'\n" +
    "- Complete literature review section, focusing on recent studies (post-2020).\n", 0f);
        }
    }

    public void ToggleCalendar()
    {
        if (Calendar != null)
        {
            Calendar.SetActive(!Calendar.activeSelf);
            CheckAndToggleTargetButton();
        }
        else
        {
            Debug.LogError("Prefab to instantiate is not assigned.");
        }
    }

    public void ToggleTargetButton()
    {
        toggleButtonPressCount++;
        // Check if the press count is odd
        if (toggleButtonPressCount % 2 != 0)
        {
            UpdateTextAndChangeButtonTransparency(null, toggleTargetButton, "Edit", 1f);
        }
        else
        {
            UpdateTextAndChangeButtonTransparency(null, toggleTargetButton, "Confirm", 1f);
        }
    }

    private void CheckAndToggleTargetButton()
    {
        // If all conditions are met and the target button is assigned
        if (firstButtonPressCount > 0 && secondButtonPressCount > 0 && toggleTargetButton != null)
        {
            // Toggle the target button's active state
            toggleTargetButton.gameObject.SetActive(true);
        }
    }
    //public void ToggleConfirmButton()
    //{
    //    confirmButtonPressCount++;
    //    if (confirmButtonPressCount % 2 != 0)
    //    {
    //        // Update text and transparency when the confirm button is pressed an odd number of times
    //        UpdateTextAndChangeButtonTransparency(confirmButtonTextField, confirmButton, "Edit", 1f);
    //    }
    //    else
    //    {
    //        // Update text and transparency for even number of presses
    //        UpdateTextAndChangeButtonTransparency(confirmButtonTextField, confirmButton, "Confirm", 1f);
    //    }
    //}

    public void UpdateTextAndChangeButtonTransparency(TextMeshProUGUI textField, Button button, string message, float newAlpha)
    {
        // Update the text if textField is not null
        if (textField != null)
        {
            textField.text = message;
        }

        // Change the alpha value of the button's Image component
        if (button != null)
        {
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                Color currentColor = buttonImage.color;
                currentColor.a = newAlpha;
                buttonImage.color = currentColor;
            }
        }
    }
}
