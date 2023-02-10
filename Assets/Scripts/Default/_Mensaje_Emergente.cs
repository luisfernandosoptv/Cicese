using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _Mensaje_Emergente : MonoBehaviour 
{
	public Text txtcuerpo;
	public Image imgSi, imgNo;
	private Animator animator;
	private _Sonidos sonidos;

	void Awake()
	{
		sonidos = FindObjectOfType<_Sonidos> ();
		animator = GetComponent<Animator> ();

	}

	public void updateFields(string rediSi="",string rediNo="",string cuerpo_="",bool cambiarSi=true,bool cambiarNo=true)
	{
		imgSi.name = rediSi;
		imgNo.name = rediNo;
		txtcuerpo.text = cuerpo_;
		imgSi.tag=(cambiarSi)?"Cambiar":"Untagged";
		imgNo.tag=(cambiarNo)?"Cambiar":"Untagged";
	}

	public void show()
	{
		if(sonidos==null)
			sonidos = FindObjectOfType<_Sonidos>();
		sonidos.play (0);
		animator.SetBool ("Mostrar",true);
	}
	public void hide()
	{
		if (sonidos == null)
			sonidos = FindObjectOfType<_Sonidos>();
		sonidos.play (1);
		animator.SetBool ("Mostrar",false);
	}
}