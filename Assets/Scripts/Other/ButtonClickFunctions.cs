using UnityEngine;
using System.Collections;

public class ButtonClickFunctions : MonoBehaviour{

	public void OnePlayerClicked(){
		Application.LoadLevel("OnePlayerScene");
	}

	public void TwoPlayerClicked(){
		Debug.Log ("TwoPlayer");
	}
}
