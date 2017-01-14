using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTime : MonoBehaviour {

	private float score = 0f;

	// Use this for initialization
	void Start () 
	{
		this.GetComponent<Text> ().text = 0.0.ToString("0");
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.GetComponent<Text> ().text = (score + Time.timeSinceLevelLoad).ToString("0");
	}

	public void AddScore(float num)
	{
		score += num;
	}
}
