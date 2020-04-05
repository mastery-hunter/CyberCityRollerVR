using UnityEngine;
using System.Collections;

public class CitySpawner : MonoBehaviour {

    [Tooltip("X: Number of Rows Y: Number of Columns")]
    [SerializeField]
    private Vector2 gridSize;

    [Tooltip("Size of gaps between adjacent buildings")]
    [SerializeField]
    private float buildingSpawnGap = 4f;

    [Tooltip("Amount of random height scaling to be applied to each building")]
    [SerializeField]
    private float heightFluctuationSize = 1.5f;

    [Space]

    [Tooltip("Building prefabs")]
    [SerializeField]
    private GameObject[] buildings;

    private GameObject[] currentlySpawnedBuildings;

	// Use this for initialization
	void Start () {
        // Initializes the array, sets the size the number of buildings our script will generate
        currentlySpawnedBuildings = new GameObject[(int)(gridSize.x * gridSize.y)];

        //GenerateCity(); NOT YET
        //StartCoroutine(GenerateCity()); NOT YET
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateCity();

            //StartCoroutine(SpawnCity());
        }
    }

    //private IEnumerator SpawnCity()
    private void GenerateCity()
    {
        // Gets the distance from origin of the starting buildings
        // Seperately gets the distance for the rows(x) and columns(z)
        float rowDistFromOrigin = (gridSize.x * buildingSpawnGap) / 2;
        float columnDistFromOrigin = (gridSize.y * buildingSpawnGap) / 2;

        // Used to store each buildings spawn location
        // Specifically defined here to create one object on the heap instead of thousands 
        Vector3 spawnLocation = Vector3.zero;

        // Used to keep track of how many buildings have spawned for array indexing
        int indexCounter = 0;

        //INT BASED FOR LOOP GENERATION
        #region
        /*
        for (int x = 0; x < gridSize.x; x++)
        {
            for(int z = 0; z < gridSize.y; z++)
            {
                int indexC = (x * 10) + z;

                RemoveBuilding(indexC);

                spawnLocation.x = (x * buildingSpawnGap) - rowAbsDistFromOrigin;
                spawnLocation.z = (z * buildingSpawnGap) - columnAbsDistFromOrigin;

                currentlySpawnedBuildings[indexC] = SpawnBuilding(spawnLocation);
            }
        }
        */
        #endregion

        // Nested for loop for spawning buildings on a grid
        for (float x = -rowDistFromOrigin; x < rowDistFromOrigin; x += buildingSpawnGap)
        {
            for (float z = -columnDistFromOrigin; z < columnDistFromOrigin; z += buildingSpawnGap)
            {

                //Random.InitState(5); ALL SAME BUILDINGS
                //Random.InitState(Mathf.RoundToInt(x)); EACH ROW SAME BUILDING
                //Random.InitState(indexCounter); DETERMINISTIC SEED

                // Remove building at current index for replacement by new building
                RemoveBuilding(indexCounter);

                // Set building spawn location
                spawnLocation.x = x;
                spawnLocation.z = z;

                // Spawn building and store it in array
                currentlySpawnedBuildings[indexCounter] = SpawnBuilding(spawnLocation);
                
                // Increment building spawned counter
                indexCounter++;
            }
                //yield return new WaitForSeconds(0.001f);
        }
    }

    // Destroys a spawned building by its index in currentlySpawnedBuildings
    private void RemoveBuilding(int index)
    {
        // Checks to make sure the data at the array index is not null
        if (currentlySpawnedBuildings[index])
        {
            // If a building exists at the index, destroy it
            Destroy(currentlySpawnedBuildings[index]);
        }
    }

    // Instantiates a random building gameobject, modifies its height, and returns it
    private GameObject SpawnBuilding(Vector3 spawnLocation)
    {
        // Get random building from buildings array
        int randomIndex = Random.Range(0, buildings.Length);
        GameObject buildingToSpawn = buildings[randomIndex];

        // Instantiate a copy of the randomly chosen building and store it in CurrentBuilding
        GameObject CurrentBuilding = Instantiate(buildingToSpawn, spawnLocation, Quaternion.identity, transform);

        // Randomly modify the height scale of the building to give the city more variability
        CurrentBuilding.transform.localScale = CurrentBuilding.transform.localScale + (Vector3.up * Random.Range(-heightFluctuationSize / 2, heightFluctuationSize));

        // Return the instantiated building for storage in our CurrentlySpawnedBuilding array
        return CurrentBuilding;
    }
}
