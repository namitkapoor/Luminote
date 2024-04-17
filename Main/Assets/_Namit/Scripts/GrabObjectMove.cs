using UnityEngine;

public class OVRControllerPushPull : MonoBehaviour
{
    [Tooltip("Speed at which the object will move forward or backward.")]
    public float pushPullSpeed = 1.0f;

    private GameObject currentlyGrabbedObject;

    void Update()
    {
        if (currentlyGrabbedObject != null)
        {
            // Check if the primary thumbstick is being moved on the right controller
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

            // Use the Y axis of the thumbstick to determine forward/backward movement
            float pushPullAmount = thumbstick.y * pushPullSpeed * Time.deltaTime;

            // Determine the direction to move the object
            Vector3 moveDirection = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).normalized;

            // Move the object
            currentlyGrabbedObject.transform.position += moveDirection * pushPullAmount;
        }
    }

    // This method should be called by your grabbing script when the grab state changes
    public void SetGrabbedObject(GameObject grabbedObject)
    {
        currentlyGrabbedObject = grabbedObject;
    }
}
