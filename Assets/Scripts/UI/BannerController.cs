using UnityEngine;
using System.Collections;

public class BannerController : MonoBehaviour {

	protected Animator animator;
	private AudioSource audioPlayer;

	private bool animating;
	
	void Start () {
		animator = GetComponent<Animator>();
		audioPlayer = GetComponent<AudioSource>();
	}

	public void showFight(){
		animating = true;
		if (animator == null)
			animator = GetComponent<Animator> ();
		animator.SetTrigger ("SHOW_FIGHT");
	}

	public void showYouWin(){
		animating = true;
		if (animator == null)
			animator = GetComponent<Animator> ();
		animator.SetTrigger ("SHOW_YOU_WIN");
	}

	public void showYouLose(){
		animating = true;
		if (animator == null)
			animator = GetComponent<Animator> ();
		animator.SetTrigger ("SHOW_YOU_LOSE");
	}

	public void playVoice(AudioClip voice){
		GameUtils.playSound (voice, audioPlayer);
	}

	public void animationEnded(){
		animating = false;
	}

	public bool isAnimating{
		get{
			return animating;
		}
	}
}
