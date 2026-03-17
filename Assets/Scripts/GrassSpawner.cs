using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public float spawnSpeed;
    public GameObject grassPrefab;
    private float t;
    public BoxCollider spawnArea;
    public Vector3 spawnPosition;
    private float spawnX, spawnY, spawnZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Increase t by time
        t += Time.deltaTime;

        // If t is greated than spawnSpeed
        if (t > spawnSpeed)
        {
            // Spawn a grass prefab at a random position in the spawn area
            SetSpawnPosition();
            Instantiate(grassPrefab, spawnPosition, Quaternion.identity);
            
            // Set t back to 0
            t = 0;
        }

    }

    void SetSpawnPosition()
    {
        // Set the spawnX and spawnZ valuea to be within the box collider
        spawnX = Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2);
        spawnZ = Random.Range(-spawnArea.size.z / 2, spawnArea.size.z / 2);

        // Set the spawnY value to be the spawner's height
        spawnY = transform.position.y;
        
        // Set the spawn position with the x, y, and z variables
        spawnPosition = new Vector3 (spawnX, spawnY, spawnZ);
    }
}
