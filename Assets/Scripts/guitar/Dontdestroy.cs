using UnityEngine;
using System.Collections;

public class Dontdestroy : MonoBehaviour {

	int check;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update (){
		if(Application.loadedLevel==4 ||  Application.loadedLevel==5){
			Destroy(gameObject);
		}

		if (Application.loadedLevel == 1){
			check = 2;
		}

		if(Application.loadedLevel==0 && check==2){
			Destroy(gameObject);
		}
	}
}
