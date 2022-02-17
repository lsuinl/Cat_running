using UnityEngine;
using System.Collections;

public class Block_Loop : MonoBehaviour {

	public float Speed = 10;
	public GameObject[] Block;
	public GameObject A_Zone;
	public GameObject B_Zone;
	public float Meter = 0;

	void Start(){

		SceneChange ();

	}

	void Update(){

		Move ();

	}

	void Move() {
		A_Zone.transform.Translate (Vector3.left * Speed * Time.deltaTime);
		B_Zone.transform.Translate (Vector3.left * Speed * Time.deltaTime);

		if(B_Zone.transform.position.x <= 0){
			Destroy (A_Zone);
			A_Zone = B_Zone;
			Make();
		}
	
	}

	void Make(){
		
		int A = Random.Range (0, Block.Length);
		
		B_Zone = Instantiate
			(Block[A], new Vector3 (A_Zone.transform.position.x+30, -5, 0), transform.rotation) 
				as GameObject;
		
	}

	public void SceneChange()
	{
		Meter += Time.deltaTime * 10;
		
		if (Meter == 50.0)
		{
			Application.LoadLevel("stage_2");
		}
		
		else if (Meter == 1000)
		{
			Application.LoadLevel ("stage_3");
		}
		
		else if (Meter == 1500)
		{
			Application.LoadLevel ("stage_4");
		}
		
		//엔딩씬
		else if (Meter == 1500)
		{
			Application.LoadLevel ("ending");
		}
	}
}
