using UnityEngine;
using System.Collections;

public class intromusic : MonoBehaviour {
	AudioSource backmusic;
	int check;

	void Start()
	{
		DontDestroyOnLoad(gameObject); 
	}

	void Update () {
		if (Application.loadedLevel != 4)
				check = 2;
		if(Application.loadedLevel!=4 && Application.loadedLevel!=5)
		{ //인트로씬이나 룰씬이 아닐경우
			Destroy(gameObject); //부셔 (music off)
		}
		if(Application.loadedLevel==4 && check==2){
			Destroy(gameObject);
		}
	}

}