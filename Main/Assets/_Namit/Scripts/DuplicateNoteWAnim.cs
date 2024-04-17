using UnityEngine;

public class ObjectInteractionWithHoverAnimation : MonoBehaviour
{
    public GameObject objectToInstantiate; // Assign your prefab in the inspector
    public Vector3 offset = new Vector3(1, 0, 0); // Adjust this offset as needed

    private Animator animator; // Reference to the Animator component

    // Controller button and hand to use for interaction
    public OVRInput.Button duplicationButton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Controller controller = OVRInput.Controller.RTouch;

    public Transform controllerTransform; // Assign your controller's transform here

    private bool wasHovering = false; // Tracks the previous hover state

    void Start()
    {
        animator = GetComponent<Animator>(); // Attempt to get the Animator component attached to the GameObject
        if (animator != null)
        {
            animator.enabled = false; // Initially disable the Animator
        }
    }

    void Update()
    {
        CheckHoverState();
        HandleDuplication();
    }

    void CheckHoverState()
    {
        RaycastHit hit;
        Vector3 rayDirection = controllerTransform.forward;
        Ray ray = new Ray(controllerTransform.position, rayDirection);
        Debug.DrawLine(controllerTransform.position, controllerTransform.position + rayDirection * 100, Color.red);

        bool isHovering = Physics.Raycast(ray, out hit) && hit.transform == transform;

        // Enable the Animator and set "IsHovering" only when hover begins
        if (isHovering && !wasHovering)
        {
            if (animator != null)
            {
                animator.enabled = true; // Enable the Animator on hover
                animator.SetBool("IsHovering", true); // Trigger the hover animation
            }
            wasHovering = true;
        }
        // Disable the Animator when hover ends
        else if (!isHovering && wasHovering)
        {
            if (animator != null)
            {
                animator.SetBool("IsHovering", false); // Ensure the animation can finish or transition out
                animator.enabled = false; // Then disable the Animator
            }
            wasHovering = false;
        }
    }

    void HandleDuplication()
    {
        // Duplicate the object if the specified button is pressed while hovering
        if (wasHovering && OVRInput.GetDown(duplicationButton, controller))
        {
            DuplicateObject();
        }
    }

    void DuplicateObject()
    {
        // Calculate a position next to the current object with the given offset.
        Vector3 newPosition = transform.position + offset;
        // Instantiate the object at the new position, with the same rotation as the current object
        Instantiate(objectToInstantiate, newPosition, transform.rotation);
    }
}
