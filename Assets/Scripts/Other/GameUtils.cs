using UnityEngine;
using System.Collections;

public class GameUtils {
	public static void playSound(AudioClip clip, AudioSource audioPlayer){
		audioPlayer.Stop ();
		audioPlayer.clip = clip;
		audioPlayer.loop = false;
		audioPlayer.time = 0;
		audioPlayer.Play ();
	}
}
