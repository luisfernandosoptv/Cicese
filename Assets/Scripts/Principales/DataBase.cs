using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Text;
using System.IO;
using Mono.Data.Sqlite;

public enum Tipo_Consulta
{
	LOCAL,
	PETICION,
	SOCKETIO
}

public class DataBase : MonoBehaviour
{
	private Game gm;
	private string ruta = "URI=file:Assets\\Data\\Game.db";
	public int actual_version;
	private bool cargada_inicial;

	public void crear_archivo(string ruta)
    {
		SqliteConnection.CreateFile(ruta);
	}

	public void obtener_ruta_base_datos()
	{
		if (gm == null)
			gm = FindObjectOfType<Game>();
		ruta = "URI=file:" + gm.config.ruta_base_datos;
	}

	public async void obtener_version_actual()
	{
		if (cargada_inicial)
			return;
		actual_version = (await getQuery("select Version from _Configuracion where configuracion_id=1" )).get(0, 0).ToInt();
		cargada_inicial = true;
	}

	public async Task<bool> Query(string query)
	{
		if (gm == null)
			gm = FindObjectOfType<Game>();

		Tipo_Consulta tipo = Tipo_Consulta.LOCAL;
		if (gm.en_linea)
		{
			tipo = Tipo_Consulta.PETICION;
		}

		int i = 0;
		if (tipo == Tipo_Consulta.LOCAL)
		{
			using (SqliteConnection c = new SqliteConnection(ruta))
			{
				c.Open();
				using (SqliteCommand cmd = new SqliteCommand(query, c))
				{
					i = cmd.ExecuteNonQuery();
				}
			}
		}
		else if (tipo == Tipo_Consulta.PETICION)
		{
			var values = new WWWForm();
			values.AddField("type", "query");
			values.AddField("query", query);
			await gm.server.enviar_peticion(Tipos_Peticiones.POST, "/API", values);
		}

		return (i == 1);
	}

	public async Task<ListQuery> getQuery(string query )
	{
		if (gm == null)
			gm = FindObjectOfType<Game>();
		ListQuery lsquery = new ListQuery();


		Tipo_Consulta tipo = Tipo_Consulta.LOCAL;
		if (gm.en_linea)
		{
			tipo = Tipo_Consulta.PETICION;
		}

		if (tipo == Tipo_Consulta.LOCAL)
		{
			using (SqliteConnection c = new SqliteConnection(ruta))
			{
				c.Open();
				using (SqliteCommand cmd = new SqliteCommand(query, c))
				{
					using (SqliteDataReader dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							Registro registro = new Registro();
							for (int i = 0; i < dr.FieldCount; i++)
							{
								registro.datos.Add(new Campo(dr.GetValue(i)));
							}
							lsquery.Add(registro);
						}
					}
				}
			}
		}
		else if (tipo == Tipo_Consulta.PETICION)
		{
			var values = new WWWForm();
			values.AddField("type", "select");
			values.AddField("query", query);

			var texto = await gm.server.enviar_peticion(Tipos_Peticiones.POST, "/API", values);

			JSONObject json = new JSONObject(texto);
			if(json)
            {
				lsquery = json_to_listquery(lsquery, json);
            }
		}
		return lsquery;
	}

	public static ListQuery json_to_listquery(ListQuery lsquery, JSONObject json)
	{
		var rows = json.GetField("rows").list;
		for (int i = 0; i < rows.Count; i++)
		{
			Registro registro = new Registro();
			var column = rows[i].list;
			for (int j = 0; j < column.Count; j++)
			{
				var data = column[j].ToString().Replace("\"", "");
				if (data.StartsWith(" ", StringComparison.Ordinal))
				{
					data = data.Substring(1, data.Length - 1);
				}
				registro.datos.Add(new Campo(data));
			}
			lsquery.Add(registro);
		}
		return lsquery;
	}

	public async void inicializar_db()
	{
		await Query(DataBase_Estructura.estructura_query);
		await Query(DataBase_Estructura.usuarios_default);
	}
}

public class ListQuery
{
	private List<Registro> registros = new List<Registro>();
	public int Count { get { return registros.Count; } }
	public void Add(Registro registro)
	{
		registros.Add(registro);
	}
	public Campo get(int registro, int campo)
	{
		return registros[registro].datos[campo];
	}
	public List<Registro> ToList()
	{
		return registros;
	}
}

public class Registro
{
	public List<Campo> datos = new List<Campo>();
}
public class Campo
{
	public object valor;

	public Campo(object dato)
	{
		valor = dato;
	}
	public int ToInt()
	{
		return valor.ToString() == "" || valor == null || valor.Equals("") || valor.Equals("null") ? 0 : int.Parse(valor.ToString());
	}
	public float ToFloat()
	{
		return valor.ToString() == "" || valor == null || valor.Equals("") || valor.Equals("null") ? 0 : float.Parse(valor.ToString());
	}
	public double ToDouble()
	{
		return valor.ToString() == "" || valor == null || valor.Equals("") || valor.Equals("null") ? 0 : double.Parse(valor.ToString());
	}
	public override string ToString()
	{
		return valor == null || valor.Equals("") ? "" : valor.ToString();
	}
}