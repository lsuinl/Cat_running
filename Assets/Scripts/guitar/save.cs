using UnityEngine;
using System.Collections;

public class save : MonoBehaviour {

	public GameManager GM;
	public int a=0;
	public float Meter;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}
	/*
	void Update () {
		Meter += Time.deltaTime * 10;
		if (Meter >= 50) {
			me
				}
	}
*/
}
