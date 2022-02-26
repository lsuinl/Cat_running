using UnityEngine;
using System.Collections;

public class JumpNSlide : MonoBehaviour {

	public GameObject Moving;
	public string MethodName;
	
	public void OnMouseDown(){
		Moving.SendMessage(MethodName);
	}
}
