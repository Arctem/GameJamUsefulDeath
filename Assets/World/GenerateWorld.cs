using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {
	
	public Wall wall;
	
	// Use this for initialization
	void Start () {
		int worldSize = (int) transform.localScale.x * 5;
		print (worldSize);
		
		for(int i = 0; i < 30; i++) {
			Instantiate(wall,
				new Vector3(Random.Range(-worldSize, worldSize), 2,
				Random.Range(-worldSize, worldSize)),
				Random.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
