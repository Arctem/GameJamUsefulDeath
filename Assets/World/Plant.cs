using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.rotation = Quaternion.Euler(0, Random.Range (0, 360), 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
