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
        t += Time.deltaTime;
        if (t > spawnSpeed)
        {
            // Spawn a grass prefab at a random position in the spawn area
            SetSpawnPosition();
            Instantiate(grassPrefab, spawnPosition, Quaternion.identity);
            t = 0;
        }

    }

    void SetSpawnPosition()
    {

        spawnX = Random.Range(-spawnArea.size.x / 2, spawnArea.size.x / 2);
        spawnY = transform.position.y;
        spawnZ = Random.Range(-spawnArea.size.z / 2, spawnArea.size.z / 2);
        spawnPosition = new Vector3 (spawnX, spawnY, spawnZ);
    }
}
