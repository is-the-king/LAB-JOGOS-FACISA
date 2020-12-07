using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHeroControl : MonoBehaviour {


	// variaveis
	public Rigidbody2D playerheroRigidbody;
	public Animator 		Anime;
	public int 				ForceJump;
	public bool 			jump;
    public bool slide;
	public Transform 		GroundCheck;
	public bool 			grounded;
	public LayerMask 		whatIsGround;
	public float 			radiusOverlapCircle;
	public float 			timeSlide;
	public float 			timeTemp;
	public Transform		colisor;
	public float			colisorConstante;


	void Start () {
		
	}
	void Update () {
		if(Input.GetButtonDown("Jump") && grounded) {
			Debug.Log("> botão de pulo pressionado.");
            playerheroRigidbody.AddForce (new Vector2(0,ForceJump));
			jump = true;
			if(slide){
				colisor.position = new Vector3(colisor.position.x, colisor.position.y + colisorConstante, colisor.position.z);
				slide = false;
			}
		}
		if(Input.GetButtonDown("Slide") && grounded) {
			Debug.Log ("> botão de slide pressionado.");
			if (!slide) {
				colisor.position = new Vector3(colisor.position.x, colisor.position.y - colisorConstante, colisor.position.z);
			}
            slide = true;
			timeTemp = 0;
		}
		grounded = Physics2D.OverlapCircle (GroundCheck.position, radiusOverlapCircle, whatIsGround);
		if(slide){
			timeTemp += Time.deltaTime;
			if (timeTemp >= timeSlide) {		
				colisor.position = new Vector3(colisor.position.x, colisor.position.y + colisorConstante, colisor.position.z);
				slide = false;
			}
		}



        Anime.SetBool ("jump", !grounded);
        Anime.SetBool ("slide", slide);

	} 

	void OnTriggerEnter2D(){
		Debug.Log ("> Colidiu.");
		SceneManager.LoadScene ("index");
	}

}
