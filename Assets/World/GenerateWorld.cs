using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {
	
	public Wall wall;
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 30; i++) {
			Instantiate(wall,
				new Vector3(Random.Range(-50, 50), 2, Random.Range(-50, 50)),
				Random.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
