using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSafezone : MonoBehaviour {

	private AudioSource safezoneAudio;

	void Start()
	{
		safezoneAudio = GetComponent<AudioSource> ();
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.tag == "Player" && !safezoneAudio.isPlaying && safezoneAudio.isActiveAndEnabled) 
		{
			safezoneAudio.Play ();
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		safezoneAudio.Stop ();
	}
}