using UnityEngine;
using System.Collections;

public class PictureA_To_B : MonoBehaviour { 
	//게임방법에서 한번 터치했을 때 화면이 넘어가는 스크립트

	public GameObject picture_A;
	public GameObject picture_B;

	void Update () {

		if (Input.GetMouseButton(0)) { //마우스 좌클릭/화면을 터치하면 
			picture_A.gameObject.SetActive(false); //첫 번째 장면이 사라지고
			picture_B.gameObject.SetActive(true); //두 번째 장면이 나타남
			}
	}
}
