using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Perfil_Usuario : Perfil_Usuario_Default
{
	private Game gm;

	public void cargar_data()
	{
		gm = FindObjectOfType<Game> ();

		base.cargar_default();
	}

	public void reset()
	{
		base.reset_default();
	}

	public async Task<bool> login(string id_="INVITADO",string contraseña="1")
	{
		bool login_=await base.login_default(id_,contraseña);
		return login_;
	}

	public async void logout()
    {
		sesion_actual.fecha_cierre = System.DateTime.Now.ToString();
		await sesion_actual.update();

		reset();
    }
}
