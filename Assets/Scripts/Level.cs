using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform[] spawnPoints;

    public SpawnManager[] spawnManagers;

    public GameObject player;

    private static int currentRoomIndex = 0;

    public void Start()
    {
        currentRoomIndex = 0;
        // Spawn the player at the first spawn point
        SpawnPlayer(currentRoomIndex);
    }

    public void Update()
    {
        // Check if the player has reached the end of the level
        if (spawnManagers[currentRoomIndex].currentEnemyCount == spawnManagers[currentRoomIndex].maxEnemies - 1)
        {
            spawnManagers[currentRoomIndex].gameObject.SetActive(false);
            // Move to the next room
            currentRoomIndex++;
            if (currentRoomIndex >= spawnPoints.Length)
            {
                // End of the level
                Debug.Log("End of the level");
                return;
            }
            // Spawn the player at the next spawn point
            SpawnPlayer(currentRoomIndex);
        }
    }

    // Go to a spawn point and spawn the player
    public void SpawnPlayer(int index)
    {
        Transform spawnPoint = spawnPoints[index];
        player.transform.position = spawnPoint.position;
        currentRoomIndex = index;
        // set the spawner to be active
        spawnManagers[index].gameObject.SetActive(true);
    }
}
