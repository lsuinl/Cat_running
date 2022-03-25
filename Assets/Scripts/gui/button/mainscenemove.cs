using UnityEngine;
using System.Collections;

public class mainscenemove : MonoBehaviour {

	public void start(){
		Time.timeScale = 1f;
		Application.LoadLevel ("stage_1");
		
	}
	public void rule(){
		Time.timeScale = 1f;
		Application.LoadLevel ("rule");
		
	}

	public void main(){
		Time.timeScale = 1f;
		Application.LoadLevel ("IntroScene");
		
	}
}
