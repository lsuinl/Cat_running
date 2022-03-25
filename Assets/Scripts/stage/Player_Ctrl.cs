using UnityEngine;
using System.Collections;
//조금 더 간략하게 만들어보기
public enum PlayerState{
	Run,
	Jump,
	D_Jump,
	Sliding,
	Death
}

public class Player_Ctrl : MonoBehaviour {
	
	public CapsuleCollider collider;
	
	public PlayerState PS;
	public float Jump_Power = 0.05f;
	public float doubleJump_Power = 0.04f;
	
	public Rigidbody rig;
	public float slideSpeed = 10f;
	
	public AudioClip[] Sound;
	public Animator animator;
	
	public GameObject AnotherSpeaker;
	public GameManager GM;
	public float Meter;
	public int strongtime = 0;

	public GameObject Fish;
	public GUITexture JumpButton;
	public GUITexture SlideButton;
	int i=0, j=0,z=0;
	//SpriteRenderer spriteRenderer;
	
	void Start()
	{
		collider = GetComponent<CapsuleCollider>();
		rig = GetComponent<Rigidbody>();
	}
	//------------------------------------------------------------
	void Update(){
		SceneChange ();
		
		rigidbody.WakeUp(); 
		if(j==1 && PS != PlayerState.Death){ //점프키가 눌리고, 플레이어가 죽은 상태가 아니라면
			if(PS == PlayerState.Jump){  //플레이어 상태가 점프라면 
				D_Jump ();//더블점프 
			}
			if(PS == PlayerState.Run){//플레이어 상태가 달리기라면
				Jump (); //점프 
			}
			if(PS == PlayerState.Sliding){
				GoUp();
				Jump ();
			}
		}
		
		if (i==1)//점프 도중에 슬라이드를 눌러도 캐릭터가 바닿에 내려가면 바로 슬라이드가 가능하도록 함(GetKey)
			if (PS == PlayerState.Run)
				Sliding ();
		
		if (z==1)//슬라이드 손 떼기 
			if (PS == PlayerState.Sliding)
				GoUp ();
	
	}
	//---------------------------------------------------------------
	public int btnjump(){
		return j = 1;
	}
	public int btnslide(){
		return  i= 1;
	}
	public int btnslideup(){
		return z = 1;
	}

	void Jump(){
		PS = PlayerState.Jump;
		rigidbody.AddForce (Vector3.up *Jump_Power, ForceMode.Impulse);
		//SoundPlay (2);
		AnotherSpeaker.SendMessage ("SoundPlay");
		
		animator.SetTrigger("Jump");
		animator.SetBool("Ground",false);
		j = 0;
	}
	
	void D_Jump(){
		PS = PlayerState.D_Jump;
		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce (Vector3.up *doubleJump_Power, ForceMode.Impulse);
		//SoundPlay (2);
		AnotherSpeaker.SendMessage ("SoundPlay");
		
		animator.SetTrigger("D_Jump");
		animator.SetBool("Ground",false);
		j = 0;
	}
	void Run(){
		PS = PlayerState.Run;
		
		animator.SetBool("Ground",true);
	}
	void OnCollisionEnter(Collision collision){
		if(PS != PlayerState.Run && PS != PlayerState.Death && PS !=PlayerState.Sliding){
			Run();
		}
	}
	
	void Sliding()
	{
		PS = PlayerState.Sliding;
		SlidingSound();
		transform.rotation = Quaternion.Euler(0,0,-90);
		rig.MovePosition(transform.position + Vector3.down); //캐릭터가 누울때 내려가는 움직임을 없애줌 
		animator.SetTrigger("Sliding");
		animator.SetBool("Ground",false);
		i = 0;
	}
	
	void GoUp()
	{
		transform.transform.rotation = Quaternion.Euler(0,0,0);
		rig.MovePosition (transform.position + Vector3.up);
		Run();
		animator.SetTrigger("Run"); //Fix
		animator.SetBool("Ground",true);
		z = 0;
	}
	//---------------------------------------------------------------------------------------------

	public void ActiveTheRainbowFish () {
		Fish.SetActive(true);
		
	}
	
	public void DeactiveTheRainbowFish(){
		Fish.SetActive(false);
	}

	void CoinGet(){
		if (GM != null) {
			GM.CoinGet ();
		}
	}
	
	void GameOver(){
		PS = PlayerState.Death;
		SoundPlay (1);
		GM.GameOver ();
	}

	void SlidingSound(){
		SoundPlay(3);
	}
	
	void SoundPlay(int Num){
		audio.clip = Sound [Num];
		audio.Play ();
	}
	
	void OnTriggerStay(Collider other){
		rigidbody.WakeUp();
		if (other.gameObject.name == "Coin") {
			Destroy (other.gameObject);
			CoinGet ();
		}
		else if (other.gameObject.tag == "Coin_R"){ //무지개 붕어빵이랑 닿으면
			OnStrong(); //OnStrong 함수 실행
			Destroy (other.gameObject);
			CoinGet ();
		}

		if(other.gameObject.name == "DeathZone" && PS != PlayerState.Death && this.gameObject.layer != 9){ //player layer가 9번이 아니라면
			GameOver();
		}
	}
	
	void OnStrong() //강화 함수
	{
		this.gameObject.layer = 9; //player layer이 9번 (Strong)으로 바뀜
		ActiveTheRainbowFish (); //오른쪽 상단에 무지개붕어빵 그림 활성화
		//spriteRenderer.color = new Color (1, 1, 1, 0.5f); //무적상태가 되면 캐릭터가 약간 투명해지도록
		strongtime += 3; //무지개 붕어빵을 하나 새로 먹을 때마다 무적시간 3초 추가
		Invoke ("OffStrong", strongtime); //3초 후에는 OffStrong 함수 실행
		
	}
	
	void OffStrong() //강화해제 함수
	{
		strongtime = 0;
		this.gameObject.layer = 0; //player layer이 Default로 바뀜
		DeactiveTheRainbowFish (); //오른쪽 상단에 무지개붕어빵 그림 비활성화
		//spriteRenderer.color = new Color (1, 1, 1, 1); //투명화 해제
	}
	
	void SceneChange(){
		if (PS != PlayerState.Death) {  //죽으면 스테이지 넘김 x
			Meter += Time.deltaTime * 10;
		}
		if(Meter>=500){
			if(Application.loadedLevel==0){ //해당 씬의 인덱스를 확인(0==스테이지 1로 빌드해야함. )
				Application.LoadLevel ("stage_2");
			}
			else if (Application.loadedLevel==1)
			{
				Application.LoadLevel ("stage_3");
			}
			
			else if (Application.loadedLevel==2)
			{
				Application.LoadLevel ("stage_4");
			}
			else if (Application.loadedLevel==2)
			{
				Application.LoadLevel ("ending");
			}
		}    
	}
}