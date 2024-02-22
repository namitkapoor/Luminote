using System.Collections;
using UnityEngine;

public class SpawnNote : MonoBehaviour
{
    public GameObject notePrefab;
    public float spawnDistanceFromController = 0.12f;
    private float lastButtonPressTime;
    private float doubleTapThreshold = 0.3f;

    public static System.Collections.Generic.List<GameObject> spawnedNotes = new System.Collections.Generic.List<GameObject>();

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            float currentTime = Time.time;
            if (currentTime - lastButtonPressTime < doubleTapThreshold)
            {
                SpawnNotePrefab();
            }
            lastButtonPressTime = currentTime;
        }
    }

    void SpawnNotePrefab()
    {
        Vector3 spawnPosition = transform.position + transform.forward * spawnDistanceFromController;
        GameObject spawnedNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);

        spawnedNote.transform.LookAt(Camera.main.transform.position);
        spawnedNote.transform.Rotate(0f, 180f, 0f, Space.Self);

        Rigidbody spawnedNoteRB = spawnedNote.GetComponent<Rigidbody>();
        if (spawnedNoteRB != null)
        {
            spawnedNoteRB.velocity = Vector3.zero;
            spawnedNoteRB.isKinematic = true;
            spawnedNoteRB.useGravity = false;
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on the spawned note prefab.");
        }

        NoteCollisionHandler noteCollisionHandler = spawnedNote.AddComponent<NoteCollisionHandler>();

        spawnedNotes.Add(spawnedNote);
    }
}

public class NoteCollisionHandler : MonoBehaviour
{
    private bool isAnchored = false;
    private Collision collisionInfo;  // Store collision info

    void OnCollisionEnter(Collision collision)
    {
        if (!isAnchored)
        {
            collisionInfo = collision;  // Capture the collision info
            AnchorNote();
            isAnchored = true;
        }
    }

    private void AnchorNote()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            rb.useGravity = false;

            // Align the note parallel to the surface using the surface normal
            if (collisionInfo != null)
            {
                ContactPoint contact = collisionInfo.contacts[0];
                Quaternion rotationToSurfaceNormal = Quaternion.FromToRotation(Vector3.forward, contact.normal);
                transform.rotation = rotationToSurfaceNormal * Quaternion.Euler(90, 0, 0);
            }
        }
    }
}