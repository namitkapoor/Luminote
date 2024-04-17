using UnityEngine;

public class SpawnObjectButton : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab you want to spawn
    public Transform spawnLocation; // The location where you want to spawn the prefab

    // Call this method when the button is pressed
    public void OnButtonPressed()
    {
        Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
    }
}
