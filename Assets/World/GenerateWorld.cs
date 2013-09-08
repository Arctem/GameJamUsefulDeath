using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {
	
	public int numWalls = 100;
	public int numBoxes = 200;
	public int numGuns = 5;
	
	public int worldSize;
	public Wall wall;
	public Box box;
	public Plant outsideTree;
	public Gun gun;
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		
		worldSize = (int) transform.localScale.x * 5;
		print ("World size: " + worldSize);
		
		for(int i = 0; i < numWalls; i++) {
			Instantiate(wall,
				new Vector3(Random.Range(-worldSize, worldSize), 2,
				Random.Range(-worldSize, worldSize)),
				Random.rotation);
		}
		
		for(int i = 0; i < numBoxes; i++) {
			Instantiate(box,
				new Vector3(Random.Range(-worldSize, worldSize),
				2, Random.Range(-worldSize, worldSize)),
				Random.rotation);
		}
		
		for(int i = 0; i < numGuns; i++) {
			Vector3 location = new Vector3(1, 0, 0) *
				Random.Range(worldSize / 2, worldSize);
			location = Quaternion.Euler(0, i * 360 / numGuns +
				Random.Range(0, 360 / numGuns), 0) * location;
			location.y = 2;
			
			Instantiate(gun, location, Random.rotation);
		}
		
		for(int i = 0; i < transform.localScale.x; i++) {
			Instantiate (outsideTree,
				new Vector3(worldSize + 10, 10, worldSize - 5 - 10 * i),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(-worldSize - 10, 10, worldSize - 5 - 10 * i),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(worldSize - 5 - 10 * i, 10, worldSize + 10),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(worldSize - 5 - 10 * i, 10, -worldSize - 10),
				Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
