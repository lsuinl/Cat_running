using UnityEngine;
using System.Collections;

public class StageName : MonoBehaviour {

	public GUIText Stagename;
	
	void Update () {

		Destroy(Stagename, 2); //2초 뒤에 Stagename 텍스트 삭제

	}
}
