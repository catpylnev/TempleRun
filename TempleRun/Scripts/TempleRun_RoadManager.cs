using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRun_RoadManager : MonoBehaviour
{
    public GameObject[] roadPrefabs; // Array of road prefabs
    public Transform player;  // Reference to the player or camera

    private float spawnX = 18.29f; // Initial spawn position
    private float roadLength = 18.25f; // Length of road prefab
    private int roadsOnScreen = 3; // Number of road pieces to keep on the screen

    private List<GameObject> activeRoads;

    private void Start()
    {
        activeRoads = new List<GameObject>();
        SpawnRoad();
    }

    private void Update()
    {
        // Check if need to spawn a new road piece
        if (player.position.x + roadLength * roadsOnScreen > spawnX + roadLength)
        {
            SpawnRoad();
            if (activeRoads.Count > roadsOnScreen)
            {
                // Delay the deletion of the very last road piece by 5 seconds
                Invoke("DeleteRoad", 3f);
            }
        }
    }

    private void SpawnRoad()
    {
        // Randomly select a road prefab from the array
        GameObject selectedRoadPrefab = roadPrefabs[Random.Range(0, roadPrefabs.Length)];

        // Instantiate the selected road prefab
        GameObject road = Instantiate(selectedRoadPrefab, new Vector3(spawnX, -7.34f, 0), Quaternion.identity);

        // Add the instantiated road to the activeRoads list
        activeRoads.Add(road);

        // Adjust the offset for the next road piece
        spawnX += roadLength;
    }

    private void DeleteRoad()
    {
        // Remove the oldest road piece 
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}
