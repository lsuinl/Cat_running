using UnityEngine;
using System.Collections;

public class Dontdestroy : MonoBehaviour {
	public static Dontdestroy instance=null;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
		
	}
	// Update is called once per frame
	void Update (){

		if(Application.loadedLevel==4 ||  Application.loadedLevel==6){
			Destroy(gameObject);
		}
	}
}
