using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public int playerHealth=100;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public GameObject[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public bool hit = false;
    //private Collider2D[] PlayerFound;
    private bool[] hasSpawn;
    private GameObject player;

    public void Start()
    {
        hasSpawn = new bool[spawnPoints.Length];
        player = GameObject.Find("player_character");
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
        //if (spawnPoints.Length > 0) { 
        //for (int i = 0; i < spawnPoints.Length; i++)
         //   {
          //   hasSpawn[i] = false;
           // }
       // }
    }
    public void Update()
    {
        playerHealth = player.GetComponent<playerController>().health;
        if (playerHealth <= 0) { hit = false; }

    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        //if (spawnPoints.Length <= 0) { }
        //else
        //{
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (playerHealth <= 0) { hit = false; }
                if (c.gameObject.tag=="Player"&&c.IsTouching(spawnPoints[i].GetComponent<Collider2D>())&& hit == false)
                {
                    Spawn(spawnPoints[i].transform);
                   hit = true;
                }
           // }
        }
    }

    void Spawn(Transform alive)
    {
        Instantiate(enemy, alive.position, alive.rotation);
    }

   /* void Spawn()
    {
        // If the player has no health left...
        if (playerHealth <= 0)
        {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }*/

}