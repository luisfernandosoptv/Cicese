using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Perfil_Usuario_Configuracion : Perfil_Usuario_Registros
{
	private Game gm;

    public DB_Configuracion configuracion=new DB_Configuracion();

	public async void cargar_default_configuracion()
	{
		gm = FindObjectOfType<Game>();

		var registros = await gm.db.getQuery("select * from _configuracion where configuracion_id=1");
		for (int i = 0; i < registros.Count; i++)
		{
			configuracion = DB_Configuracion.create(registros,i);
		}
	}
}
