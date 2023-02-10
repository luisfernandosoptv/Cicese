using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class _Redireccionamiento : MonoBehaviour 
{

	private Game gm;
	private _Cambiar_Escena cambiar_escena;
	private _Sonidos sonidos;

	void Start()
	{
		gm =  FindObjectOfType<Game> ();
		cambiar_escena = FindObjectOfType<_Cambiar_Escena>();
		sonidos = FindObjectOfType<_Sonidos>();
	}

    public void validar_seleccion(GameObject col)
    {
    	if(col.tag.Equals("Cambiar"))
		{
			sonidos.playBoton();
			gm.log.guardar_accion("CAMBIAR_A", col.name);
			cambiar_escena.cambiar_escena(col.name);
		}
        if(col.name.Equals("Salir"))
		{
			sonidos.playBoton();
			gm.log.guardar_accion("SALIR");
			Application.Quit ();
        }
    }
}
