using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public int columns = 50;
	public int rows = 50;
	public GameObject[] cloudTiles;

	private List<Vector3> gridPositions = new List<Vector3>();
    private Transform cloudParent;

	void InitialiseList(){
		// Clear our list gridPositions
		gridPositions.Clear();

		// Loop through x axis (columns)
		for(int x = 1; x < columns; x++){
			// Within each column, loop through y axis (rows)
			for(int y = 1; y < rows; y++){
				// At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add(new Vector3(x, y, 0f));
			}
		}
	}

	Vector3 RandomPosition(){
		int randomIndex = Random.Range (0, gridPositions.Count);

		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);

		return randomPosition;
	}

	void LayoutObjectAtRandom(GameObject[] itemArray, int minimum, int maximum){
		// Choose a random number of objects to instantiate within the maximum and minimum limits.
		int objectCount = Random.Range (minimum, maximum);

		// Instantiate objects until the randomly chosen limit objectCount is reached.
		for(int i = 0; i < objectCount; i++){
			Vector3 randomPosition = RandomPosition ();

			GameObject itemChoice = itemArray [Random.Range (0, itemArray.Length)];

			Instantiate (itemChoice, randomPosition, Quaternion.identity, cloudParent);
		}
	}

	public void SetupScene(){
        cloudParent = GameObject.Find("Clouds").transform;

		// Reset our list of gridPositions
		InitialiseList ();

		// Instantiate random number of clouds accros the gameBoard.
		LayoutObjectAtRandom (cloudTiles, 80, 100);
	}

	public int GetColumns(){
		return columns;
	} 

	public int GetRows(){
		return rows;
	}
}
