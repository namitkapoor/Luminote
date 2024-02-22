using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject objectToToggle;
    public GameObject alternateObjectToToggle;
    public float scaleReduction = 0.5f;
    private Vector3 originalScale;
    private bool isObjectToggled = false;

    // Controller button and hand to use for interaction
    public OVRInput.Button toggleButton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Controller controller = OVRInput.Controller.RTouch;

    public Transform controllerTransform; // Assign your controller's transform here

    private Coroutine hideAlternateObjectCoroutine;
    void Start()
    {
        originalScale = transform.localScale;
        if (alternateObjectToToggle != null)
        {
            alternateObjectToToggle.SetActive(false);
        }
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 rayDirection = controllerTransform.forward;
        Ray ray = new Ray(controllerTransform.position, rayDirection);
        Debug.DrawLine(controllerTransform.position, controllerTransform.position + rayDirection * 100, Color.red);

        bool isHovering = Physics.Raycast(ray, out hit) && hit.transform == transform;

        // Click to toggle the main object
        if (isHovering && OVRInput.GetDown(toggleButton, controller))
        {
            ToggleMainObject();
        }

        // Hover to toggle the alternate object
        ToggleAlternateObjectOnHover(isHovering);
    }

    public void ToggleMainObject()
    {
        if (objectToToggle != null)
        {
            isObjectToggled = !isObjectToggled;
            objectToToggle.SetActive(isObjectToggled);

            // Adjust scale based on the toggle state
            if (isObjectToggled)
            {
                // Revert to original scale when toggled on
                transform.localScale = originalScale;

                // Ensure the alternate object is off
                if (alternateObjectToToggle != null)
                {
                    alternateObjectToToggle.SetActive(false);
                    StopCoroutineIfRunning();
                }
            }
            else
            {
                // Reduce scale when toggled off
                transform.localScale = originalScale * scaleReduction;
            }

            // If main object is toggled on, ensure the alternate object is off
            if (isObjectToggled && alternateObjectToToggle != null)
            {
                StopCoroutineIfRunning();
                alternateObjectToToggle.SetActive(false);
            }
        }
    }

    public void ToggleAlternateObjectOnHover(bool isHovering)
    {
        if (alternateObjectToToggle != null && !isObjectToggled)
        {
            if (isHovering)
            {
                alternateObjectToToggle.SetActive(true);
                StopCoroutineIfRunning();
            }
            else if (alternateObjectToToggle.activeSelf)
            {
                hideAlternateObjectCoroutine = StartCoroutine(HideAlternateObjectAfterDelay());
            }
        }
    }
    private IEnumerator HideAlternateObjectAfterDelay()
    {
        yield return new WaitForSeconds(3f); // Delay in seconds before hiding the object
        if (!isObjectToggled) // Check again to ensure the main object is not toggled on
        {
            alternateObjectToToggle.SetActive(false);
        }
    }

    private void StopCoroutineIfRunning()
    {
        if (hideAlternateObjectCoroutine != null)
        {
            StopCoroutine(hideAlternateObjectCoroutine);
        }
    }
}
