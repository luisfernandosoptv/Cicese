using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _Cambiar_Escena : MonoBehaviour 
{
	private Game gm;
	private Animator animator;
	private AsyncOperation carga;
	private bool cambiando;

	void Start () 
	{
		gm = FindObjectOfType<Game>();
		animator = GetComponent<Animator>();
	}

	public void cambiar_escena(string nombre_escena, bool oscurecer = true)
	{
		if (cambiando)
			return;
		gm.log.guardar_accion("CAMBIAR_ESCENA",nombre_escena,oscurecer);
		if (!oscurecer)
		{
			SceneManager.LoadScene(nombre_escena,LoadSceneMode.Single);
		}
		animator.SetBool("aparecer",true);
		carga=SceneManager.LoadSceneAsync(nombre_escena, LoadSceneMode.Single);
		carga.allowSceneActivation = false;
		StartCoroutine(cargar_escena());
		cambiando = true;
	}

	IEnumerator cargar_escena()
    {
		while (carga.progress<0.9f)
		{
			yield return new WaitForSeconds(0.1f);
		}
		carga.allowSceneActivation = true;
    }
}
