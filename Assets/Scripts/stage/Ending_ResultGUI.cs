using UnityEngine;
using System.Collections;

public class Ending_ResultGUI : MonoBehaviour {
	
	public GameObject FinalResult_GUI;
	
	void Update () {
		if (Input.GetMouseButton(1)) { //마우스 우클릭/화면을 터치하면 
			FinalResult_GUI.SetActive (true); //결과창이 뜸
		}
	}
}
