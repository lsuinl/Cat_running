using UnityEngine;
using System.Collections;

public enum GameState{
	Play,
	Pause,
	End
}

public class GameManager : MonoBehaviour {

	public GameState GS;

	public GUIText Text_Meter;
	public GUIText Text_Gold;
	public GameObject Final_GUI;
	public GUIText Final_Meter;
	public GUIText Final_Gold;
	public GameObject Pause_GUI;
	public GameObject AnotherSpeaker;

	public float Speed;
	public float Meter;
	public static int Stage1Gold; //스테이지1에서 먹은 붕어빵 총 개수
	public static int Stage2Gold; //스테이지 1, 2에서 먹은 붕어빵 총 개수
	public static int Stage3Gold; //스테이지 1~3에서 먹은 붕어빵 총 개수
	public static int Stage4Gold; //스테이지 1~4에서 먹은 붕어빵 총 개수

	public void Update(){
		
		
		meter ();
	}
	
	public void CoinGet(){
		if (GS != GameState.End) {
			AnotherSpeaker.SendMessage ("SoundPlay");

			if (Application.loadedLevel==0)
			{
				Stage1Gold += 1;
				Text_Gold.text = string.Format ("{0}", Stage1Gold);
				Stage2Gold = Stage1Gold; //Stage1Gold에 붕어빵 개수 저장
			}
			
			else if (Application.loadedLevel==1)
			{
				Stage2Gold += 1; //스테이지1에서 먹은 붕어빵 += 1
				Text_Gold.text = string.Format ("{0}", Stage2Gold);
				Stage3Gold = Stage2Gold; //Stage2Gold에 붕어빵 개수 저장
			}
			
			else if (Application.loadedLevel==2)
			{
				Stage3Gold += 1; //스테이지2에서 먹은 붕어빵 += 1
				Text_Gold.text = string.Format ("{0}", Stage3Gold);
				Stage4Gold = Stage3Gold; //Stage3Gold에 붕어빵 개수 저장
			}
			
			else if (Application.loadedLevel==3)
			{
				Stage4Gold += 1; //스테이지3에서 먹은 붕어빵 += 1
				Text_Gold.text = string.Format ("{0}", Stage4Gold);
			}
			//else if (Application.loadedLevel==6)
			//{
				//Text_Gold.text = string.Format ("{0}", Stage4Gold);
			//}
		}

		else if(GS == GameState.End) //죽으면
			Stage1Gold = 0; //붕어빵 개수 리셋
	}

	public void meter(){ //거리를 나타내는 함수
		if (GS == GameState.Play) {
			Meter += Time.deltaTime * Speed;
			if(Application.loadedLevel==0){
				Text_Meter.text = string.Format("{0:N0}m",Meter);
			}
			else if (Application.loadedLevel==1)
			{
				Text_Meter.text = string.Format("{0:N0}m",Meter+300);
			}
			
			else if (Application.loadedLevel==2)
			{
				Text_Meter.text = string.Format("{0:N0}m",Meter+600);
			}
			else if (Application.loadedLevel==3)
			{
				Text_Meter.text = string.Format("{0:N0}m",Meter+900);
			}
			else if (Application.loadedLevel==6)
			{
				Final_Meter.text = string.Format("{0:N0}m",1200);
			}
		}
	}


	public void GameOver(){

		Final_Meter.text = string.Format ("{0:N1}", Meter);
		FinalScore(); //결과창에 나올 붕어빵 개수

		GS = GameState.End;
		Final_GUI.SetActive (true);

	
	}

	public void FinalScore(){ //결과창에 나올 붕어빵 개수 함수 (스테이지별로 나눔)
		switch(Application.loadedLevel){
		case 0: //스테이지1이면
			Final_Gold.text = string.Format ("{0}", Stage1Gold);
			break;
		case 1: //스테이지2라면
			Final_Gold.text = string.Format ("{0}", Stage2Gold);
			break;
		case 2: //스테이지3이라면
			Final_Gold.text = string.Format ("{0}", Stage3Gold);
			break;
		case 3: //스테이지4라면
			Final_Gold.text = string.Format ("{0}", Stage4Gold);
			break;
		//case 6: //if EndingScene
			//Final_Gold.text = string.Format ("{0}", Stage4Gold);
			//break;
		}
	}

	public void Replay(){
		Time.timeScale = 1f;
		Application.LoadLevel ("stage_1");

	}

	public void MainGo(){
		Time.timeScale = 1f;
		Application.LoadLevel ("IntroScene");

	}

	public void Pause(){

		GS = GameState.Pause;
		Time.timeScale = 0f;
		Pause_GUI.SetActive (true);
	}

	public void UnPause(){
		
		GS = GameState.Play;
		Time.timeScale = 1f;
		Pause_GUI.SetActive (false);
	}
}
