using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    public GameObject objectToInstantiate; // Assign your prefab in the inspector
    public Transform parentObject; // Assign the parent object in the inspector
    public Vector3 offset = new Vector3(1, 0, 0); // Adjust this offset as needed

    public void InstantiateNextToParent()
    {
        if (objectToInstantiate != null && parentObject != null)
        {
            // Calculate a position next to the parent with the given offset.
            Vector3 newPosition = parentObject.position + offset;

            // Instantiate the object at the new position, with the same rotation as the parent
            GameObject newObject = Instantiate(objectToInstantiate, newPosition, parentObject.rotation);

            // Optional: To maintain a parent-child relationship (if desired)
            // newObject.transform.SetParent(parentObject);
        }
    }
}
