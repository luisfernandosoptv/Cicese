using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class _Sonido_Menu : MonoBehaviour 
{
	private Game gm;
	private AudioSource actual;
	private AudioSource otro;
	private float sonido_base;
	
	void Awake () 
	{
		gm = FindObjectOfType<Game>();
		actual =GetComponent<AudioSource> ();
		if (GameObject.FindGameObjectsWithTag ("Sonido_Menu").Length >=2) 
		{
			otro = GameObject.FindGameObjectsWithTag("Sonido_Menu").ToList().Find(x => x != this.gameObject).GetComponent<AudioSource>();
			sonido_base = otro.volume;
			otro.volume = sonido_base*gm.perfil.configuracion.volumen_musica;
            if (otro.clip != actual.clip)
            {
				otro.clip = actual.clip;
				otro.Play();
            }
			Destroy(gameObject);
        }
		if (sonido_base == 0)
			sonido_base = 1;
		actual.Play ();
		DontDestroyOnLoad (gameObject);
	}

	private void Update()
	{
		if(actual!=null)
			actual.volume = sonido_base*gm.perfil.configuracion.volumen_musica;
		if (otro != null)
			otro.volume = sonido_base * gm.perfil.configuracion.volumen_musica;
	}
}
