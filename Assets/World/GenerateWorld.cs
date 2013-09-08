﻿using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {
	
	public int numWalls = 100;
	public int numBoxes = 200;
	public int numGuns = 5;
	
	public Wall wall;
	public Box box;
	public Plant outsideTree;
	public Gun gun;
	
	// Use this for initialization
	void Start () {
		int worldSize = (int) transform.localScale.x * 5;
		
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
			location = Quaternion.Euler(0, Random.Range(0, 360), 0) * location;
			location.y = 2;
			
			Instantiate(gun, location, Random.rotation);
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
