using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {
	public enum PlayerType
	{
		HUMAN, AI	
	};

	public float MAX_HEALTH = 100f;
	public float health;

	public string fighterName;
	public bool enable = false;

	public PlayerType player;
	public Fighter enemy;
	public FighterStates currentState = FighterStates.IDLE;

	protected Animator animator;
	private Rigidbody myBody;
	private AudioSource audioPlayer;
	private int countF, countB,countDefend, countSquat;

	//for AI
	private float random;
	private float randomSetTime;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		audioPlayer = GetComponent<AudioSource> ();
		health = MAX_HEALTH;
	}

	public void UpdateHumanInput (){
		if (countF > 0) {
			animator.SetBool ("Walk", true);
			countF--;
		} else {
			animator.SetBool ("Walk", false);
			countF =0;
		}

		if (countB > 0) {
			animator.SetBool ("WalkBack", true);
			countB--;
		} else {
			animator.SetBool ("WalkBack", false);
			countB =0;
		}

		if (countDefend>0) {
			animator.SetBool ("Defend", true);
			countDefend--;
		} else {
			countDefend = 0;
			animator.SetBool ("Defend", false);
		}

		if (countSquat > 0) {
			animator.SetBool ("Duck", true);
			countSquat--;
		} else {
			animator.SetBool ("Duck", false);
		}
	}

	public void walkForward(){
		countF = 20;
	}

	public void walkBackward(){
		countB = 20;
	}

	public void punchRight(){
		animator.SetTrigger("PunchRight");
	}

	public void punchLeft(){
		animator.SetTrigger("PunchLeft");
	}

	public void kickHit(){
		animator.SetTrigger("KickRight");
	}

	public void defend(){
		countDefend = 8;

	}

	public void jump(){
		animator.SetTrigger("Jump");
	}

	public void squat(){
		countSquat = 10;
	}

	public void UpdateAiInput (){
		animator.SetBool ("defending", defending);
		animator.SetBool ("oponent_attacking", enemy.punching||enemy.kicking);
		animator.SetFloat ("distanceToOponent", getDistanceToOponent());

		if (Time.time - randomSetTime > 1) {
			random = Random.value;
			randomSetTime = Time.time;
		}
		animator.SetFloat ("random", random);
	}

	private float getDistanceToOponent(){
		return Mathf.Abs(transform.position.x - enemy.transform.position.x);
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("Health", healthPercent);

		if (enemy != null) {
			animator.SetFloat ("EnemyHealth", enemy.healthPercent);
		} else {
			animator.SetFloat ("EnemyHealth", 1);
		}

		if (enable) {
			if (player == PlayerType.HUMAN) {
				UpdateHumanInput ();
			}else{
				UpdateAiInput();
			}

		}

		if (health <= 0 && currentState != FighterStates.DEAD) {
			animator.SetTrigger ("Dead");
		}
	}



	public virtual void hurt(float damage){
		if (!invulnerable) {
			if (defending){
				damage *= 0.2f;
			}
			if (health >= damage) {
				health -= damage;
			} else {
				health = 0;
			}

			if (health > 0) {
				animator.SetTrigger ("TakeHit");
			}
		}
	}

	public void playSound(AudioClip sound){
		GameUtils.playSound (sound, audioPlayer);
	}

	public bool invulnerable {
		get {
			return currentState == FighterStates.TAKE_HIT 
				|| currentState == FighterStates.TAKE_HIT_DEFEND
					|| currentState == FighterStates.DEAD;
		}
	}

	public bool defending {
		get {
			return currentState == FighterStates.DEFEND 
				|| currentState == FighterStates.TAKE_HIT_DEFEND;
		}
	}

	public bool punching {
		get {
			return currentState == FighterStates.PUNCH;
		}	
	}

	public bool kicking {
		get {
			return currentState == FighterStates.KICK;
		}	
	}

	public float healthPercent {
		get {
			return health / MAX_HEALTH;
		}	
	}

	public Rigidbody body {
		get {
			return this.myBody;
		}
	}
	//Keyboard inputs
	/*public void UpdateHumanInput (){
		if (Input.GetAxis ("Horizontal") > 0.1) {
			animator.SetBool ("Walk", true);
		} else {
			animator.SetBool ("Walk", false);
		}
		
		if (Input.GetAxis ("Horizontal") < -0.1) {
			animator.SetBool ("WalkBack", true);
			animator.SetBool ("Defend", false);
		} else {
			animator.SetBool ("WalkBack", false);
			animator.SetBool ("Defend", false);
		}
		
		if (Input.GetAxis ("Vertical") < -0.1) {
			animator.SetBool ("Duck", true);
		} else {
			animator.SetBool ("Duck", false);
		}
		
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			animator.SetTrigger("Jump");
		} 
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			animator.SetTrigger("PunchRight");
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			animator.SetTrigger("KickRight");
		}
		
	}*/
}
