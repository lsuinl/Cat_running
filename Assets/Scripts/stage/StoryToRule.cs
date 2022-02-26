using UnityEngine;
using System.Collections;

public class StoryToRule : MonoBehaviour { 
	//게임방법에서 한번 터치했을 때 화면이 넘어가는 스크립트

	public GameObject Story;
	public GameObject Rule;

	void Update () {

		if (Input.GetMouseButton(0)) { //마우스 클릭/화면을 터치하면 
			Story.gameObject.SetActive(false); //Story가 사라지고
			Rule.gameObject.SetActive(true); //Rule이 나타남
			}
		}
	}
