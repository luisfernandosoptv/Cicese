using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class Perfil_Usuario_Registros : MonoBehaviour
{
	private Game gm;

	public List<DB_Registro_Sesion> registros_sesion = new List<DB_Registro_Sesion>();
	public DB_Registro_Sesion sesion_actual = new DB_Registro_Sesion();
	public DB_Registro_Sesion sesion_anterior = new DB_Registro_Sesion();

	public void reset_default_registros()
	{
		registros_sesion.Clear();
		sesion_actual.clear();
		sesion_actual.clear();
	}

	public async Task<bool> cargar_datos_sesion_default_registros(int usuario_id)
	{
		if(gm==null)
			gm = FindObjectOfType<Game>();

		var registros = await gm.db.getQuery("select * from registro_sesion where usuario_id=" + usuario_id);
		if (registros != null)
		{
			for (int i = 0; i < registros.Count; i++)
			{
				DB_Registro_Sesion registro = DB_Registro_Sesion.create(registros, i);
				registros_sesion.Add(registro);
			}
		}

		sesion_anterior = registros_sesion.OrderByDescending(x => x.registro_sesion_id).ToList().First();

		sesion_actual = new DB_Registro_Sesion();
		sesion_actual.sesion_id = sesion_anterior.sesion_id+ 1;
		sesion_actual.usuario_id = usuario_id;
		await sesion_actual.save();

		return true;
	}
}
