using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sonidos : MonoBehaviour 
{
	private Game gm;
	private AudioSource audio;
	public AudioClip[] clips;
	
	void Awake()
	{
		gm = FindObjectOfType<Game>();
		audio = GetComponent<AudioSource> ();
	}

	public void play(int index)
	{
		audio.clip = clips [index];
		audio.Play ();
	}

	public void playBoton()
	{
		audio.clip = clips[2];
		audio.Play();
	}

	private void Update()
    {
		audio.volume = gm.perfil.configuracion.volumen_efectos;
	}
}
