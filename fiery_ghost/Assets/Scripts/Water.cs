﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	public float damage;
	public float speed;
	public float waterVariance;
	public float waterLifetime;
	public MeshRenderer VFX;

	public Material[] waterMaterials;

	public AudioClip douse;
    public AudioClip[] quench;

	private Rigidbody2D rb2d;
	private float lifetime;
    
    // Use this for initialization
    void Start () 
	{
		VFX.material = waterMaterials[Random.Range(0, waterMaterials.Length)];

		Vector3 playerPosition = GetComponentInParent<Transform> ().position;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		float normalizationValue = Vector3.Distance (playerPosition, mousePosition);

		Vector3 fireDirection = new Vector3(
			mousePosition[0] - playerPosition[0], 
			mousePosition[1] - playerPosition[1], 
			0.0f);

		Vector3 offset = new Vector3 (Random.Range (-waterVariance, waterVariance), Random.Range (-waterVariance, waterVariance), 0.0f);

		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.AddForce (((fireDirection + offset) / normalizationValue) * speed);

		lifetime = Time.timeSinceLevelLoad;
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad > lifetime + waterLifetime) 
		{
			Object.Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
        if (other.tag == "Firehit") 
		{
			float fireHitpoints = other.gameObject.GetComponentInParent<Fire> ().hitpoints;

			if (fireHitpoints <= 15) 
			{
				PlayClipAt(douse, transform.position, 0.35f, 30);

				other.transform.parent.GetComponent<Fire> ().score.AddScore (5f);
				other.transform.parent.GetComponent<Fire> ().ReportDeath ();

				Destroy (other.transform.parent.gameObject);
			} 
			else 
			{
				other.gameObject.GetComponentInParent<Fire>().hitpoints = fireHitpoints - damage;

                //plays one of arrayed clip when water hits the fire
                int randClip = Random.Range(0, quench.Length);
                AudioSource.PlayClipAtPoint(quench[randClip], transform.position, 200);
            }

			Destroy (this.gameObject);
		}
		else if (other.tag == "Monster") 
		{
			MonsterController monster = other.gameObject.GetComponentInParent<MonsterController> ();
			if (!monster.monsterAudio.isPlaying) 
			{
				int randClip = Random.Range(0, monster.slurps.Length);
				monster.monsterAudio.clip = monster.slurps[randClip];
				monster.monsterAudio.Play ();
			}
			Destroy (this.gameObject);
		}
	}

	AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float volume, int priority)
	{
		GameObject tempGO = new GameObject("TempAudio"); 
		tempGO.transform.position = pos; 
		AudioSource aSource = tempGO.AddComponent<AudioSource>();

		aSource.clip = clip; 
		aSource.volume = volume;
		aSource.priority = priority;
		aSource.spatialBlend = 0;

		aSource.Play(); 
		Destroy(tempGO, clip.length); 
		return aSource; 
	}
}
