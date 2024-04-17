using UnityEngine;

public class RaycastColorChanger : MonoBehaviour
{
    public Material distanceBasedMaterial;
    public Transform raycastOrigin;
    public LayerMask layerMask; // Make sure to assign the layer mask for raycast hit detection.

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, Mathf.Infinity, layerMask))
        {
            // Update the material properties if the raycast hits an object
            distanceBasedMaterial.SetVector("_HitPosition", hit.point);
            distanceBasedMaterial.SetColor("_HitColor", Color.green);
        }
        else
        {
            // If nothing is hit, we simply update the hit position to be far away
            distanceBasedMaterial.SetVector("_HitPosition", raycastOrigin.position + raycastOrigin.forward * 1000);
            distanceBasedMaterial.SetColor("_HitColor", Color.red);
        }
    }
}
