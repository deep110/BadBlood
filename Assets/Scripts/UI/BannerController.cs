using UnityEngine;
using System.Collections;

public class BannerController : MonoBehaviour {

	private Animator animator;
	private AudioSource audioPlayer;

	private bool animating;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		audioPlayer = GetComponent<AudioSource> ();
	
	}

	public void showRoundFight(){
		animating = true;
		animator.SetTrigger ("SHOW_ROUND_FIGHT");
	}

	public void showYouWin(){
		animating = true;
		animator.SetTrigger ("SHOW_YOU_WIN");
	}

	public void showYouLose(){
		animating = true;
		animator.SetTrigger ("SHOW_YOU_LOSE");
	}

	public void playVoice(AudioClip voice){
		//GameUtils.playSound (voice, audioPlayer);
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
