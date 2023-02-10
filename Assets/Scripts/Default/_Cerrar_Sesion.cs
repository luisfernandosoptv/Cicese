using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class _Cerrar_Sesion : MonoBehaviour 
{
	private Game gm;
	private _Cambiar_Escena cambiar_escena;

	public _Mensaje_Emergente mensaje_cerrar;

	public string escena_cerrar_invitado;
	public string escena_cerrar_registrado;

	void Start()
	{
		gm=FindObjectOfType<Game>();
		cambiar_escena=FindObjectOfType<_Cambiar_Escena>();
	}

	public void validar_seleccion(GameObject col)
	{
		if (col.name.Equals("Cerrar"))
		{
			mostrar_mensaje_cerrar();
		}
		else if (col.name.Equals("Si_Cerrar"))
		{
			cambiar_escena.cambiar_escena(gm.perfil.tipo_Session=="INVITADO"? escena_cerrar_invitado:escena_cerrar_registrado);
		}
		else if (col.name.Equals("No_Cerrar"))
		{
			cerrar_mensaje_cerrar();
		}
	}

	void mostrar_mensaje_cerrar()
	{
		mensaje_cerrar.updateFields("Si_Cerrar", "No_Cerrar", "¿Estás seguro de querer cerrar sesión?", true, false);
		mensaje_cerrar.show();
		gm.log.guardar_accion("CERRAR", "MENSAJE CERRAR SESION");
	}

	void cerrar_mensaje_cerrar()
	{
		mensaje_cerrar.hide();
	}
}
