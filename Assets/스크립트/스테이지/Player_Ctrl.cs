using UnityEngine;
using System.Collections;

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
	
	void Start()
	{
		collider = GetComponent<CapsuleCollider>();
		rig = GetComponent<Rigidbody>();
	}
	
	void Update(){
		rigidbody.WakeUp(); 
		if(Input.GetKeyDown (KeyCode.Space) && PS != PlayerState.Death){ //점프키가 눌리고, 플레이어가 죽은 상태가 아니라면
			if (Input.GetKeyDown (KeyCode.Space)) //점프키가 눌리면
			if(PS == PlayerState.Jump){  //플레이어 상태가 점프라면 
				D_Jump ();//더블점프 
			}
			if(PS == PlayerState.Run){ //플레이어 상태가 달리기라면
				Jump (); //점프 
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftShift) && Input.GetKey (KeyCode.Z))
			Sliding ();
		else if (Input.GetKeyUp (KeyCode.LeftShift))
			GoUp ();
	}
	
	void Jump(){
		PS = PlayerState.Jump;
		rigidbody.AddForce (Vector3.up *Jump_Power, ForceMode.Impulse);
		//SoundPlay (2);
		AnotherSpeaker.SendMessage ("SoundPlay");
		
		animator.SetTrigger("Jump");
		animator.SetBool("Ground",false);
	}
	
	void D_Jump(){
		PS = PlayerState.D_Jump;
		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce (Vector3.up *doubleJump_Power, ForceMode.Impulse);
		//SoundPlay (2);
		AnotherSpeaker.SendMessage ("SoundPlay");
		
		animator.SetTrigger("D_Jump");
		animator.SetBool("Ground",false);
	}
	void Run(){
		PS = PlayerState.Run;
		
		animator.SetBool("Ground",true);
	}
	void OnCollisionEnter(Collision collision){
		if(PS != PlayerState.Run && PS != PlayerState.Death){
			Run();
		}
	}
	
	void Sliding()
	{
		transform.rotation = Quaternion.Euler(0,0,-90);
	}
	
	void GoUp()
	{
		transform.rotation = Quaternion.Euler(0,0,0);
	}
	
	void CoinGet(){
		SoundPlay (0);
		
	}
	
	void GameOver(){
		PS = PlayerState.Death;
		SoundPlay (1);
	}
	
	void SoundPlay(int Num){
		audio.clip = Sound [Num];
		audio.Play ();
	}
	
	void OnTriggerEnter(Collider other){
		rigidbody.WakeUp();
		if (other.gameObject.name == "Coin") {
			Destroy (other.gameObject);
			CoinGet ();
		}
		if(other.gameObject.name == "DeathZone" && PS != PlayerState.Death){
			GameOver();
		}
	}
}