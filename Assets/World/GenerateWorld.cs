using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {
	
	public Wall wall;
	public Box box;
	public Plant outsideTree;
	
	// Use this for initialization
	void Start () {
		int worldSize = (int) transform.localScale.x * 5;
		
		for(int i = 0; i < 100; i++) {
			Instantiate(wall,
				new Vector3(Random.Range(-worldSize, worldSize), 2,
				Random.Range(-worldSize, worldSize)),
				Random.rotation);
		}
		
		for(int i = 0; i < 200; i++) {
			Instantiate(box,
				new Vector3(Random.Range(-worldSize, worldSize),
				2, Random.Range(-worldSize, worldSize)),
				Random.rotation);
		}
		
		for(int i = 0; i < 20; i++) {
			Instantiate (outsideTree,
				new Vector3(110, 10, 95 - 10 * i),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(-110, 10, 95 - 10 * i),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(95 - 10 * i, 10, 110),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(95 - 10 * i, 10, -110),
				Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
