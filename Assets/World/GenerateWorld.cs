using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {
	
	public Wall wall;
	public Plant outsideTree;
	
	// Use this for initialization
	void Start () {
		int worldSize = (int) transform.localScale.x * 5;
		print (worldSize);
		
		for(int i = 0; i < 100; i++) {
			Instantiate(wall,
				new Vector3(Random.Range(-worldSize, worldSize), 2,
				Random.Range(-worldSize, worldSize)),
				Random.rotation);
		}
		
		for(int i = 0; i < 10; i++) {
			Plant tree;
			Instantiate (outsideTree,
				new Vector3(110, 10, Random.Range (-100, 100)),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(-110, 10, Random.Range (-100, 100)),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(Random.Range (-100, 100), 10, 110),
				Quaternion.identity);
			Instantiate (outsideTree,
				new Vector3(Random.Range (-100, 100), 10, -110),
				Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
