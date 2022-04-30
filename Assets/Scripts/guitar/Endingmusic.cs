using UnityEngine;
using System.Collections;

public class Endingmusic : MonoBehaviour {

	AudioSource backmusic;
	int check;
	
	void Start()
	{
		DontDestroyOnLoad(gameObject); 
	}
	
	void Update () {
		if(Application.loadedLevel==6){
			Destroy(gameObject);
		}
	}
}
