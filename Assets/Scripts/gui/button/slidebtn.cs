using UnityEngine;
using System.Collections;

public class slidebtn : MonoBehaviour {

	
	public GameObject Moving;
	public string MethodName;
	
	public void OnMouseUp(){
		Moving.SendMessage(MethodName);
	}
}
