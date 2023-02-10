using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Tipo_Elemento
{
	public static string ALIMENTO = "ALIMENTO";
	public static string ALIMENTO_FRUTA = "ALIMENTO-FRUTA";
	public static string ALIMENTO_VERDURA = "ALIMENTO-VERDURA";
	public static string ALIMENTO_MAIZ = "ALIMENTO-MAIZ";
	public static string ARMA = "ARMA";
	public static string CURACION = "CURACION";
	public static string ENFERMEDAD = "ENFERMEDAD";
	public static string MATERIAL = "MATERIAL";
	public static string OBJETO_ESPECIAL = "OBJETO_ESPECIAL";
	public static string OBJETO_ESPIRITUAL = "OBJETO_ESPIRITUAL";
	public static string PLANTA_MEDICINAL = "PLANTA_MEDICINAL";
	public static string VESTIMENTA = "VESTIMENTA";
}

public class Tipo_Diario_Viaje
{
	public static string ALIMENTO = "ALIMENTO";
	public static string ANIMAL = "ANIMAL";
	public static string AUTORIDAD = "AUTORIDAD";
	public static string CULTURA = "CULTURA";
	public static string DEIDAD = "DEIDAD";
	public static string HERRAMIENTA = "HERRAMIENTA";
	public static string OFICIO = "OFICIO";
	public static string OFRENDA = "OFRENDA";
	public static string PLATILLO = "PLATILLO";
	public static string POSTRE = "POSTRE";
	public static string SITIO_SAGRADO = "SITIO_SAGRADO";
}

[Serializable]
public class DB_Alimento
{
	private Game gm;
	public int alimento_id;
	public string categoria;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public int puntaje;
	public int energia;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string recolectado;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int alimento_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.alimento_id = alimento_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from alimentos where alimento_id={0} ", alimento_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Alimento create(ListQuery registros, int i)
	{
		DB_Alimento alimento = new DB_Alimento();
		alimento.alimento_id = registros.get(i, 0).ToInt();
		alimento.categoria = registros.get(i, 1).ToString();
		alimento.nombre_singular_esp = registros.get(i, 2).ToString();
		alimento.nombre_plural_esp = registros.get(i, 3).ToString();
		alimento.nombre_singular_wix = registros.get(i, 4).ToString();
		alimento.nombre_plural_wix = registros.get(i, 5).ToString();
		alimento.puntaje = registros.get(i, 6).ToInt();
		alimento.energia = registros.get(i, 7).ToInt();
		alimento.ruta_imagen = registros.get(i, 8).ToString();
		alimento.recolectado = registros.get(i, 9).ToString();
		alimento.secuencia_audios = registros.get(i, 10).ToString();
		if (alimento.secuencia_audios != null || alimento.secuencia_audios.Length > 0)
			alimento.secuencia_audios_lista = alimento.secuencia_audios.Split(',').ToList();
		return alimento;
	}

	public void clear()
	{
		alimento_id = 0;
		categoria = nombre_singular_esp = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = ruta_imagen = recolectado = "";
		puntaje = energia = 0;
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}

[Serializable]
public class DB_Animal
{
	private Game gm;
	public int animal_id;
	public string categoria;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public int vida;
	public int puntaje;
	public float daño_salud;
	public float daño_energia;
	public bool da_carne;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}

	public void init(int animal_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.animal_id = animal_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from animales where animal_id={0} ", animal_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Animal create(ListQuery registros, int i)
	{
		DB_Animal animal = new DB_Animal();
		animal.animal_id = registros.get(i, 0).ToInt();
		animal.categoria = registros.get(i, 1).ToString();
		animal.nombre_singular_esp = registros.get(i, 2).ToString();
		animal.nombre_plural_esp = registros.get(i, 3).ToString();
		animal.nombre_singular_wix = registros.get(i, 4).ToString();
		animal.nombre_plural_wix = registros.get(i, 5).ToString();
		animal.vida = registros.get(i, 6).ToInt();
		animal.puntaje = registros.get(i, 7).ToInt();
		animal.daño_salud = registros.get(i, 8).ToFloat();
		animal.daño_energia = registros.get(i, 9).ToFloat();
		animal.da_carne = registros.get(i, 10).ToInt() == 1;
		animal.ruta_imagen = registros.get(i, 11).ToString();
		return animal;
	}

	public void clear()
	{
		da_carne = false;
		animal_id = vida = puntaje = 0;
		daño_salud = daño_energia = 0;
		ruta_imagen = "";
		categoria = nombre_singular_esp = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = "";
	}
}

[Serializable]
public class DB_Animal_Enfermedad
{
	private Game gm;
	public int animal_id;
	public int enfermedad_id;
}

[Serializable]
public class DB_Arma
{
	private Game gm;
	public int arma_id;
	public string categoria;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public int nivel;
	public int puntaje;
	public int resistencia;
	public float daño;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string recolectado;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int arma_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.arma_id = arma_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from armas where arma_id={0} ", arma_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Arma create(ListQuery registros, int i)
	{
		DB_Arma arma = new DB_Arma();
		arma.arma_id = registros.get(i, 0).ToInt();
		arma.categoria = registros.get(i, 1).ToString();
		arma.nombre_singular_esp = registros.get(i, 2).ToString();
		arma.nombre_plural_esp = registros.get(i, 3).ToString();
		arma.nombre_singular_wix = registros.get(i, 4).ToString();
		arma.nombre_plural_wix = registros.get(i, 5).ToString();
		arma.nivel = registros.get(i, 6).ToInt();
		arma.puntaje = registros.get(i, 7).ToInt();
		arma.resistencia = registros.get(i, 8).ToInt();
		arma.daño = registros.get(i, 9).ToFloat();
		arma.ruta_imagen = registros.get(i, 10).ToString();
		arma.recolectado = registros.get(i, 11).ToString();
		arma.secuencia_audios = registros.get(i, 12).ToString();
		if (arma.secuencia_audios != null || arma.secuencia_audios.Length > 0)
			arma.secuencia_audios_lista = arma.secuencia_audios.Split(',').ToList();
		return arma;
	}

	public void clear()
	{
		arma_id = nivel = puntaje = resistencia = 0;
		nombre_singular_esp = categoria = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = ruta_imagen = recolectado = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}


[Serializable]
public class DB_Conversacion
{
	private Game gm;
	public int conversacion_id;
	public string municipio;
	public string categoria;
	public int grupo;
	public string genero;
	public string nombre_quien_habla;
	public string tema;
	public string dialogo;
	public string botones;
	public string ruta_imagen_quien_habla;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen_quien_habla);
			}
			return imagen_;
		}
	}
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int conversacion_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.conversacion_id = conversacion_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from conversaciones where conversacion_id={0} ", conversacion_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Conversacion create(ListQuery registros, int i)
	{
		DB_Conversacion conversacion = new DB_Conversacion();
		conversacion.conversacion_id = registros.get(i, 0).ToInt();
		conversacion.municipio = registros.get(i, 1).ToString();
		conversacion.categoria = registros.get(i, 2).ToString();
		conversacion.grupo = registros.get(i, 3).ToInt();
		conversacion.genero = registros.get(i, 4).ToString();
		conversacion.nombre_quien_habla = registros.get(i, 5).ToString();
		conversacion.tema = registros.get(i, 6).ToString();
		conversacion.dialogo = registros.get(i, 7).ToString();
		conversacion.botones = registros.get(i, 8).ToString();
		conversacion.ruta_imagen_quien_habla = registros.get(i, 9).ToString();
		conversacion.secuencia_audios = registros.get(i, 10).ToString();
		if (conversacion.secuencia_audios != null || conversacion.secuencia_audios.Length > 0)
			conversacion.secuencia_audios_lista = conversacion.secuencia_audios.Split(',').ToList();
		return conversacion;
	}

	public void clear()
	{
		conversacion_id = grupo = 0;
		categoria = municipio = nombre_quien_habla = dialogo = botones = ruta_imagen_quien_habla = tema = "";
		secuencia_audios = genero = "";
		secuencia_audios_lista = null;
	}
}

[Serializable]
public class DB_Conversacion_Usuario
{
	private Game gm;
	public int conversacion_grupo;
	public int usuario_id;

	public async Task<bool> save()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("insert into conversaciones_usuarios values ({0},{1})", conversacion_grupo, usuario_id);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		conversacion_grupo = usuario_id = 0;
	}
}

[Serializable]
public class DB_Curacion
{
	private Game gm;
	public int curacion_id;
	public string categoria;
	public string nombre_singular_esp;
	public int salud;
	public int energia;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}

	public void init(int curacion_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.curacion_id = curacion_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from curaciones where curacion_id={0} ", curacion_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Curacion create(ListQuery registros, int i)
	{
		DB_Curacion curacion = new DB_Curacion();
		curacion.curacion_id = registros.get(i, 0).ToInt();
		curacion.categoria = registros.get(i, 1).ToString();
		curacion.nombre_singular_esp = registros.get(i, 2).ToString();
		curacion.salud = registros.get(i, 3).ToInt();
		curacion.energia = registros.get(i, 4).ToInt();
		curacion.ruta_imagen = registros.get(i, 5).ToString();
		return curacion;
	}

	public void clear()
	{
		curacion_id = salud = energia = 0;
		nombre_singular_esp = ruta_imagen = categoria = "";
	}
}


[Serializable]
public class DB_Curacion_Enfermedad
{
	private Game gm;
	public int curacion_id;
	public int enfermedad_id;
}

[Serializable]
public class DB_Derivado_Elemento
{
	private Game gm;
	public float cantidad;
	public int derivado_id;
	public string tipo_derivado;
	public string grupo;
	public float costo;
	public int elemento_id;
	public string tipo_elemento;

	public void clear()
	{
		grupo = "";
		costo = 0;
		cantidad = 0;
		elemento_id = 0;
		tipo_elemento = "";
		derivado_id = 0;
		tipo_derivado = grupo = "";
	}
}


[Serializable]
public class DB_Diario_Viaje
{
	private Game gm;
	public int diario_viaje_id;
	public int orden_aparicion;
	public int elemento_id;
	public string accion;
	public string tipo_elemento;
	public string tipo;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public string texto;
	public string nivel;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int diario_viaje_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.diario_viaje_id = diario_viaje_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from diarios_viajes where diario_viaje_id={0} ", diario_viaje_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Diario_Viaje create(ListQuery registros, int i)
	{
		DB_Diario_Viaje diario = new DB_Diario_Viaje();
		diario.diario_viaje_id = registros.get(i, 0).ToInt();
		diario.orden_aparicion = registros.get(i, 1).ToInt();
		diario.elemento_id = registros.get(i, 2).ToInt();
		diario.accion= registros.get(i, 3).ToString();
		diario.tipo_elemento = registros.get(i, 4).ToString();
		diario.tipo = registros.get(i, 5).ToString();
		diario.nivel = registros.get(i, 6).ToString();
		diario.nombre_singular_esp = registros.get(i, 7).ToString();
		diario.nombre_plural_esp = registros.get(i, 8).ToString();
		diario.nombre_singular_wix = registros.get(i, 9).ToString();
		diario.nombre_plural_wix = registros.get(i, 10).ToString();
		diario.texto = registros.get(i, 11).ToString();
		diario.ruta_imagen = registros.get(i, 12).ToString();
		diario.secuencia_audios = registros.get(i, 13).ToString();
		if (diario.secuencia_audios != null || diario.secuencia_audios.Length > 0)
			diario.secuencia_audios_lista = diario.secuencia_audios.Split(',').ToList();
		return diario;
	}

	public void clear()
	{
		diario_viaje_id = elemento_id = orden_aparicion = 0;
		nombre_singular_esp=accion = tipo = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = ruta_imagen = nivel = "";
		secuencia_audios = tipo_elemento = "";
		secuencia_audios_lista = null;
	}
}


[Serializable]
public class DB_Diario_Viaje_Usuario
{
	private Game gm;
	public int diario_viaje_id;
	public int usuario_id;
	public bool obtenido;

	public void init(int usuario_id, int diario_viaje_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.diario_viaje_id = diario_viaje_id;
		this.usuario_id = usuario_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from diarios_viajes_usuarios where diario_viaje_id={0} and usuario_id={1}", diario_viaje_id, usuario_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Diario_Viaje_Usuario create(ListQuery registros, int i)
	{
		DB_Diario_Viaje_Usuario diario = new DB_Diario_Viaje_Usuario();
		diario.diario_viaje_id = registros.get(i, 0).ToInt();
		diario.usuario_id = registros.get(i, 1).ToInt();
		diario.obtenido = registros.get(i, 2).ToInt() == 1;
		return diario;
	}


	public async Task<bool> save()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("insert into diarios_viajes_usuarios values ({0},{1},{2})",
			diario_viaje_id, usuario_id, obtenido ? 1 : 0);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		diario_viaje_id = usuario_id = 0;
		obtenido = false;
	}
}

[Serializable]
public class DB_Enemigo
{
	private Game gm;
	public int enemigo_id;
	public string nombre_singular_esp;
	public int vida;
	public int puntaje;
	public float daño_salud;
	public float daño_energia;

	public void init(int enemigo_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.enemigo_id = enemigo_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from enemigos where enemigo_id={0} ", enemigo_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Enemigo create(ListQuery registros, int i)
	{
		DB_Enemigo enemigo = new DB_Enemigo();
		enemigo.enemigo_id = registros.get(i, 0).ToInt();
		enemigo.nombre_singular_esp = registros.get(i, 1).ToString();
		enemigo.vida = registros.get(i, 2).ToInt();
		enemigo.puntaje = registros.get(i, 3).ToInt();
		enemigo.daño_salud = registros.get(i, 4).ToFloat();
		enemigo.daño_energia = registros.get(i, 5).ToFloat();
		return enemigo;
	}

	public void clear()
	{
		enemigo_id = vida = puntaje = 0;
		daño_salud = daño_energia = 0;
		nombre_singular_esp = "";
	}
}

[Serializable]
public class DB_Enfermedad
{
	private Game gm;
	public int enfermedad_id;
	public string categoria;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public float energia;
	public float salud;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string recolectado;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int enfermedad_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.enfermedad_id = enfermedad_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from enfermedades where enfermedad_id={0} ", enfermedad_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Enfermedad create(ListQuery registros, int i)
	{
		DB_Enfermedad enfermedad = new DB_Enfermedad();
		enfermedad.enfermedad_id = registros.get(i, 0).ToInt();
		enfermedad.categoria = registros.get(i, 1).ToString();
		enfermedad.nombre_singular_esp = registros.get(i, 2).ToString();
		enfermedad.nombre_plural_esp = registros.get(i, 3).ToString();
		enfermedad.nombre_singular_wix = registros.get(i, 4).ToString();
		enfermedad.nombre_plural_wix = registros.get(i, 5).ToString();
		enfermedad.energia = registros.get(i, 6).ToFloat();
		enfermedad.salud = registros.get(i, 7).ToFloat();
		enfermedad.ruta_imagen = registros.get(i, 8).ToString();
		enfermedad.recolectado = registros.get(i, 9).ToString();
		enfermedad.secuencia_audios = registros.get(i, 10).ToString();
		if (enfermedad.secuencia_audios != null || enfermedad.secuencia_audios.Length > 0)
			enfermedad.secuencia_audios_lista = enfermedad.secuencia_audios.Split(',').ToList();
		return enfermedad;
	}

	public void clear()
	{
		enfermedad_id = 0;
		energia = salud = 0;
		recolectado = secuencia_audios = "";
		secuencia_audios_lista = null;
		nombre_singular_esp = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = ruta_imagen = "";
	}
}

[Serializable]
public class DB_Enfermedad_Usuario
{
	private Game gm;
	public int enfermedad_id;
	public int usuario_id;
	public int cantidad;

	public void init(int enfermedad_id, int usuario_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.enfermedad_id = enfermedad_id;
		this.usuario_id = usuario_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from enfermedades_usuarios where enfermedad_id={0} and usuario_id={1} ", enfermedad_id, usuario_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Enfermedad_Usuario create(ListQuery registros, int i)
	{
		DB_Enfermedad_Usuario enfermedad = new DB_Enfermedad_Usuario();
		enfermedad.enfermedad_id = registros.get(i, 0).ToInt();
		enfermedad.usuario_id = registros.get(i, 1).ToInt();
		enfermedad.cantidad = registros.get(i, 2).ToInt();
		return enfermedad;
	}

	public async Task<bool> save()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("insert into enfermedades_usuarios values({0},{1},{2})", enfermedad_id, usuario_id, cantidad);
		return await gm.db.Query(query);
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("update enfermedades_usuarios set cantidad={0} where enfermedad_id={1} and usuario_id={2}", cantidad, enfermedad_id, usuario_id);
		return await gm.db.Query(query);
	}

	public async Task<bool> delete()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("delete from enfermedades_usuarios where enfermedad_id={0} and usuario_id={1}", enfermedad_id, usuario_id);
		return await gm.db.Query(query);
	}


	public void clear()
	{
		enfermedad_id = usuario_id = 0;
	}
}

[Serializable]
public class DB_Estado
{
	private Game gm;
	public int estado_id;
	public string nombre_esp;

	public void init(int estado_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.estado_id = estado_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from estados where estado_id={0} ", estado_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Estado create(ListQuery registros, int i)
	{
		DB_Estado estado = new DB_Estado();
		estado.estado_id = registros.get(i, 0).ToInt();
		estado.nombre_esp = registros.get(i, 1).ToString();
		return estado;
	}

	public void clear()
	{
		estado_id = 0;
		nombre_esp = "";
	}
}

[Serializable]
public class DB_Estado_Desbloqueado
{
	private Game gm;
	public int usuario_id;
	public int estado_id;
	public int municipio_id;
	public bool juego_desbloqueado;
	public bool quiz_desbloqueado;
	public bool juego_completado;
	public bool quiz_completado;

	public void init(int usuario_id, int estado_id, int municipio_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.usuario_id = usuario_id;
		this.estado_id = estado_id;
		this.municipio_id = municipio_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from estados_desbloqueados where estado_id={0} and municipio_id={1} and usuario_id={2} ", estado_id, municipio_id, usuario_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Estado_Desbloqueado create(ListQuery registros, int i)
	{
		DB_Estado_Desbloqueado estado = new DB_Estado_Desbloqueado();
		estado.usuario_id = registros.get(i, 0).ToInt();
		estado.estado_id = registros.get(i, 1).ToInt();
		estado.municipio_id = registros.get(i, 2).ToInt();
		estado.juego_desbloqueado = registros.get(i, 3).ToInt() == 1;
		estado.quiz_desbloqueado = registros.get(i, 4).ToInt() == 1;
		estado.juego_completado = registros.get(i, 5).ToInt() == 1;
		estado.quiz_completado = registros.get(i, 6).ToInt() == 1;
		return estado;
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("update estados_desbloqueados set juego_desbloqueado={4}, quiz_desbloqueado={3},juego_completado={5},quiz_completado={6} where estado_id={2} and municipio_id={1} and usuario_id={0}",
			usuario_id, municipio_id, estado_id, quiz_desbloqueado ? 1 : 0, juego_desbloqueado ? 1 : 0, juego_completado ? 1 : 0, quiz_completado ? 1 : 0);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		estado_id = estado_id = municipio_id = 0;
		juego_desbloqueado = quiz_desbloqueado = juego_completado = quiz_completado = false;
	}
}

[Serializable]
public class DB_Estrella
{
	private Game gm;
	public int estado_id;
	public int municipio_id;
	public string tipo;
	public int usuario_id;
	public int cantidad;

	public void init(int estado_id, int municipio_id, int usuario_id, string tipo)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.estado_id = estado_id;
		this.municipio_id = municipio_id;
		this.usuario_id = usuario_id;
		this.tipo = tipo;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from estrellas where estado_id={0} and municipio_id={1} and usuario_id={2} and tipo='{3}'", estado_id, municipio_id, usuario_id, tipo));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Estrella create(ListQuery registros, int i)
	{
		DB_Estrella estrella = new DB_Estrella();
		estrella.estado_id = registros.get(i, 0).ToInt();
		estrella.municipio_id = registros.get(i, 1).ToInt();
		estrella.tipo = registros.get(i, 2).ToString();
		estrella.usuario_id = registros.get(i, 3).ToInt();
		estrella.cantidad = registros.get(i, 4).ToInt();
		return estrella;
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("update estrellas set cantidad={0} where estado_id={1} and municipio_id={2} and usuario_id={3} and tipo='{4}'", cantidad, estado_id, municipio_id, usuario_id, tipo);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		estado_id = municipio_id = usuario_id = cantidad = 0;
		tipo = "";
	}
}


[Serializable]
public class DB_Explicacion_Inicio
{
	private Game gm;
	public int estado_id;
	public int municipio_id;
	public string nombre_esp;
	public string descripcion_alimentos;
	public string descripcion_animales;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;
	public string ruta_imagen1;
	public string ruta_imagen2;
	public string ruta_imagen3;
	private Sprite imagen_1;
	public Sprite imagen1
	{
		get
		{
			if (imagen_1 == null)
			{
				imagen_1 = Resources.Load<Sprite>(ruta_imagen1);
			}
			return imagen_1;
		}
	}
	private Sprite imagen_2;
	public Sprite imagen2
	{
		get
		{
			if (imagen_2 == null)
			{
				imagen_2 = Resources.Load<Sprite>(ruta_imagen2);
			}
			return imagen_2;
		}
	}
	private Sprite imagen_3;
	public Sprite imagen3
	{
		get
		{
			if (imagen_3 == null)
			{
				imagen_3 = Resources.Load<Sprite>(ruta_imagen3);
			}
			return imagen_3;
		}
	}
	public string ruta_imagenComida1;
	public string ruta_imagenComida2;
	public string ruta_imagenComida3;
	private Sprite imagenComida_1;
	public Sprite imagenComida1
	{
		get
		{
			if (imagenComida_1 == null)
			{
				imagenComida_1 = Resources.Load<Sprite>(ruta_imagenComida1);
			}
			return imagenComida_1;
		}
	}
	private Sprite imagenComida_2;
	public Sprite imagenComida2
	{
		get
		{
			if (imagenComida_2 == null)
			{
				imagenComida_2 = Resources.Load<Sprite>(ruta_imagenComida2);
			}
			return imagenComida_2;
		}
	}
	private Sprite imagenComida_3;
	public Sprite imagenComida3
	{
		get
		{
			if (imagenComida_3 == null)
			{
				imagenComida_3 = Resources.Load<Sprite>(ruta_imagenComida3);
			}
			return imagenComida_3;
		}
	}

	public void init(int estado_id, int municipio_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.estado_id = estado_id;
		this.municipio_id = municipio_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from explicacion_inicio where estado_id={0} and municipio_id={1} ", estado_id, municipio_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}

	}
	public static DB_Explicacion_Inicio create(ListQuery registros, int i)
	{
		DB_Explicacion_Inicio explicacion = new DB_Explicacion_Inicio();

		explicacion.estado_id = registros.get(i, 0).ToInt();
		explicacion.municipio_id = registros.get(i, 1).ToInt();
		explicacion.nombre_esp = registros.get(i, 2).ToString();
		explicacion.descripcion_alimentos = registros.get(i, 3).ToString();
		explicacion.descripcion_animales = registros.get(i, 4).ToString();
		explicacion.secuencia_audios = registros.get(i, 5).ToString();
		if (explicacion.secuencia_audios != null || explicacion.secuencia_audios.Length > 0)
			explicacion.secuencia_audios_lista = explicacion.secuencia_audios.Split(',').ToList();
		explicacion.ruta_imagen1 = registros.get(i, 6).ToString();
		explicacion.ruta_imagen2 = registros.get(i, 7).ToString();
		explicacion.ruta_imagen3 = registros.get(i, 8).ToString();
		explicacion.ruta_imagenComida1 = registros.get(i, 9).ToString();
		explicacion.ruta_imagenComida2 = registros.get(i, 10).ToString();
		explicacion.ruta_imagenComida3 = registros.get(i, 11).ToString();

		return explicacion;
	}

	public void clear()
	{
		estado_id = municipio_id = 0;
		nombre_esp = descripcion_alimentos = descripcion_animales = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
		ruta_imagenComida1 = ruta_imagen1 = "";
		ruta_imagenComida2 = ruta_imagen2 = "";
		ruta_imagenComida3 = ruta_imagen3 = "";
	}
}

[Serializable]
public class DB_Extra
{
	private Game gm;
	public int extra_id;
	public string nombre;

	public void init(int extra_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.extra_id = extra_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from extras where extra_id={0}", extra_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Extra create(ListQuery registros, int i)
	{
		DB_Extra extra = new DB_Extra();
		extra.extra_id = registros.get(i, 0).ToInt();
		extra.nombre = registros.get(i, 1).ToString();
		return extra;
	}

	public void clear()
	{
		extra_id = 0;
		nombre = "";
	}
}

[Serializable]
public class DB_Extra_Usuario
{
	private Game gm;
	public int usuario_id;
	public int extra_id;
	public bool completado;

	public void init(int usuario_id, int extra_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.usuario_id = usuario_id;
		this.extra_id = extra_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from extras_usuarios where usuario_id={0} and extra_id={1}", usuario_id, extra_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Extra_Usuario create(ListQuery registros, int i)
	{
		DB_Extra_Usuario extra = new DB_Extra_Usuario();
		extra.usuario_id = registros.get(i, 0).ToInt();
		extra.extra_id = registros.get(i, 1).ToInt();
		extra.completado = registros.get(i, 2).ToInt() == 1;
		return extra;
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("update extras_usuarios set completado={1} where usuario_id={0} and extra_id={2}", usuario_id, completado ? 1 : 0, extra_id);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		extra_id = 0;
		completado = false;
	}
}

[Serializable]
public class DB_Inventario_Usuario
{
	private Game gm;
	public int usuario_id;
	public int elemento_id;
	public string tipo;
	public int cantidad;
	public float resistencia;
	public bool equipado;

	public void init(int usuario_id, int elemento_id, string tipo)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.usuario_id = usuario_id;
		this.elemento_id = elemento_id;
		this.tipo = tipo;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from inventario_usuarios where elemento_id={0} and usuario_id={1} and tipo='{2}'", elemento_id, usuario_id, tipo));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Inventario_Usuario create(ListQuery registros, int i)
	{
		DB_Inventario_Usuario inventario = new DB_Inventario_Usuario();
		inventario.usuario_id = registros.get(i, 0).ToInt();
		inventario.elemento_id = registros.get(i, 1).ToInt();
		inventario.tipo = registros.get(i, 2).ToString();
		inventario.cantidad = registros.get(i, 3).ToInt();
		inventario.resistencia = registros.get(i, 4).ToFloat();
		inventario.equipado = registros.get(i, 5).ToInt() == 1;
		return inventario;
	}

	public async Task<bool> save()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("insert into inventario_usuarios(usuario_id,elemento_id,tipo,cantidad,resistencia,equipado) values({0},{1},'{2}',{3},{4},{5})", usuario_id, elemento_id, tipo, cantidad, resistencia, equipado ? 1 : 0);
		return await gm.db.Query(query);
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("update inventario_usuarios set cantidad={2}, resistencia={4}, equipado={5} where elemento_id={1} and tipo='{3}' and usuario_id={0} ", usuario_id, elemento_id, cantidad, tipo, resistencia, equipado ? 1 : 0);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		usuario_id = 0;
		elemento_id = 0;
		cantidad = 0;
		resistencia = 0;
		tipo = "";
		equipado = false;
	}
}

[Serializable]
public class DB_Municipio
{
	private Game gm;
	public int estado_id;
	public int municipio_id;
	public string nombre_esp;
	public string descripcion_esp;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int estado_id, int municipio_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.estado_id = estado_id;
		this.municipio_id = municipio_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from municipios where estado_id={0} and municipio_id={1} ", estado_id, municipio_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Municipio create(ListQuery registros, int i)
	{
		DB_Municipio municipio = new DB_Municipio();
		municipio.estado_id = registros.get(i, 0).ToInt();
		municipio.municipio_id = registros.get(i, 1).ToInt();
		municipio.nombre_esp = registros.get(i, 2).ToString();
		municipio.descripcion_esp = registros.get(i, 3).ToString();
		municipio.secuencia_audios = registros.get(i, 4).ToString();
		if (municipio.secuencia_audios != null || municipio.secuencia_audios.Length > 0)
			municipio.secuencia_audios_lista = municipio.secuencia_audios.Split(',').ToList();
		return municipio;
	}

	public void clear()
	{
		estado_id = municipio_id = 0;
		nombre_esp = descripcion_esp = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}


[Serializable]
public class DB_Objetivo
{
	private Game gm;
	public int objetivo_id;
	public int estado_id;
	public int municipio_id;
	public string municipio;
	public string tipo;
	public int elemento_id;
	public int cantidad;
	public string accion;
	public string texto;

	public void init(int objetivo_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.objetivo_id = objetivo_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from objetivos where objetivo_id={0} ", objetivo_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Objetivo create(ListQuery registros, int i)
	{
		DB_Objetivo objetivo = new DB_Objetivo();
		objetivo.objetivo_id = registros.get(i, 0).ToInt();
		objetivo.estado_id = registros.get(i, 1).ToInt();
		objetivo.municipio_id = registros.get(i, 2).ToInt();
		objetivo.municipio = registros.get(i, 3).ToString();
		objetivo.tipo = registros.get(i, 4).ToString();
		objetivo.elemento_id = registros.get(i, 5).ToInt();
		objetivo.cantidad = registros.get(i, 6).ToInt();
		objetivo.accion = registros.get(i, 7).ToString();
		objetivo.texto = registros.get(i, 8).ToString();
		return objetivo;
	}

	public void clear()
	{
		objetivo_id = estado_id = municipio_id = elemento_id = cantidad = 0;
		tipo = accion = texto = municipio = "";
	}
}

[Serializable]
public class DB_Objeto_Especial
{
	private Game gm;
	public int objeto_especial_id;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public int puntaje;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string recolectado;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int objeto_especial_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.objeto_especial_id = objeto_especial_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from objetos_especiales where objeto_especial_id={0} ", objeto_especial_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Objeto_Especial create(ListQuery registros, int i)
	{
		DB_Objeto_Especial especial = new DB_Objeto_Especial();
		especial.objeto_especial_id = registros.get(i, 0).ToInt();
		especial.nombre_singular_esp = registros.get(i, 1).ToString();
		especial.nombre_plural_esp = registros.get(i, 2).ToString();
		especial.nombre_singular_wix = registros.get(i, 3).ToString();
		especial.nombre_plural_wix = registros.get(i, 4).ToString();
		especial.puntaje = registros.get(i, 5).ToInt();
		especial.ruta_imagen = registros.get(i, 6).ToString();
		especial.recolectado = registros.get(i, 7).ToString();
		especial.secuencia_audios = registros.get(i, 8).ToString();
		if (especial.secuencia_audios != null || especial.secuencia_audios.Length > 0)
			especial.secuencia_audios_lista = especial.secuencia_audios.Split(',').ToList();
		return especial;
	}

	public void clear()
	{
		objeto_especial_id = puntaje = 0;
		nombre_singular_esp = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = ruta_imagen = recolectado = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}

[Serializable]
public class DB_Objeto_Espiritual
{
	private Game gm;
	public int objeto_espiritual_id;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public int salud;
	public int energia;
	public int puntos;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string recolectado;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int objeto_espiritual_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.objeto_espiritual_id = objeto_espiritual_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from objetos_espirituales where objeto_espiritual_id={0} ", objeto_espiritual_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Objeto_Espiritual create(ListQuery registros, int i)
	{
		DB_Objeto_Espiritual espiritual = new DB_Objeto_Espiritual();
		espiritual.objeto_espiritual_id = registros.get(i, 0).ToInt();
		espiritual.nombre_singular_esp = registros.get(i, 1).ToString();
		espiritual.nombre_plural_esp = registros.get(i, 2).ToString();
		espiritual.nombre_singular_wix = registros.get(i, 3).ToString();
		espiritual.nombre_plural_wix = registros.get(i, 4).ToString();
		espiritual.salud = registros.get(i, 5).ToInt();
		espiritual.energia = registros.get(i, 6).ToInt();
		espiritual.puntos = registros.get(i, 7).ToInt();
		espiritual.ruta_imagen = registros.get(i, 8).ToString();
		espiritual.recolectado = registros.get(i, 9).ToString();
		espiritual.secuencia_audios = registros.get(i, 10).ToString();
		if (espiritual.secuencia_audios != null || espiritual.secuencia_audios.Length > 0)
			espiritual.secuencia_audios_lista = espiritual.secuencia_audios.Split(',').ToList();
		return espiritual;
	}

	public void clear()
	{
		objeto_espiritual_id = 0;
		nombre_singular_esp = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = recolectado = ruta_imagen = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}


[Serializable]
public class DB_Planta_Medicinal
{
	private Game gm;
	public int planta_medicinal_id;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}

	public string recolectado;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int planta_medicinal_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.planta_medicinal_id = planta_medicinal_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from plantas_medicinales where planta_medicinal_id={0} ", planta_medicinal_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Planta_Medicinal create(ListQuery registros, int i)
	{
		DB_Planta_Medicinal planta = new DB_Planta_Medicinal();
		planta.planta_medicinal_id = registros.get(i, 0).ToInt();
		planta.nombre_singular_esp = registros.get(i, 1).ToString();
		planta.nombre_plural_esp = registros.get(i, 2).ToString();
		planta.nombre_singular_wix = registros.get(i, 3).ToString();
		planta.nombre_plural_wix = registros.get(i, 4).ToString();
		planta.ruta_imagen = registros.get(i, 5).ToString();
		planta.recolectado = registros.get(i, 6).ToString();
		planta.secuencia_audios = registros.get(i, 7).ToString();
		if (planta.secuencia_audios != null || planta.secuencia_audios.Length > 0)
			planta.secuencia_audios_lista = planta.secuencia_audios.Split(',').ToList();
		return planta;
	}
	public void clear()
	{
		planta_medicinal_id = 0;
		nombre_singular_esp = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = recolectado = ruta_imagen = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}

[Serializable]
public class DB_Puntaje
{
	private Game gm;
	public int usuario_id;
	public int estado_id;
	public int municipio_id;
	public int puntos;

	public void init(int usuario_id, int estado_id, int municipio_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.usuario_id = usuario_id;
		this.estado_id = estado_id;
		this.municipio_id = municipio_id;
		refresh();
	}

	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from puntajes where municipio_id={1} and estado_id={2} and usuario_id={0} ", usuario_id, municipio_id, estado_id));
		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Puntaje create(ListQuery registros, int i)
	{
		DB_Puntaje puntaje = new DB_Puntaje();
		puntaje.usuario_id = registros.get(i, 0).ToInt();
		puntaje.estado_id = registros.get(i, 1).ToInt();
		puntaje.municipio_id = registros.get(i, 2).ToInt();
		puntaje.puntos = registros.get(i, 3).ToInt();
		return puntaje;
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("update puntajes set puntos={3} where municipio_id={2} and estado_id={1} and usuario_id={0}", usuario_id, estado_id, municipio_id, puntos);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		this.usuario_id = estado_id = municipio_id = puntos = 0;
	}
}

[Serializable]
public class DB_Registro_Juego
{
	private Game gm;
	public int registro_juego_id;
	public int usuario_id;
	public int estado_id;
	public int municipio_id;
	public int puntos;
	public int vidas;
	public int energia;
	public int salud;
	public DateTime fecha_inicio;
	public string fecha_inicio_s;
	public DateTime fecha_termino;
	public string fecha_termino_s;

	public void init(int registro_juego_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.registro_juego_id = registro_juego_id;
		refresh();
	}

	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from registro_juego where registro_juego_id={0} ", registro_juego_id));
		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Registro_Juego create(ListQuery registros, int i)
	{
		DB_Registro_Juego registro = new DB_Registro_Juego();
		registro.registro_juego_id = registros.get(i, 0).ToInt();
		registro.usuario_id = registros.get(i, 1).ToInt();
		registro.estado_id = registros.get(i, 2).ToInt();
		registro.municipio_id = registros.get(i, 3).ToInt();
		registro.puntos = registros.get(i, 4).ToInt();
		registro.vidas = registros.get(i, 5).ToInt();
		registro.energia = registros.get(i, 6).ToInt();
		registro.salud = registros.get(i, 7).ToInt();
		registro.fecha_inicio_s = registros.get(i, 8).ToString();
		registro.fecha_termino_s = registros.get(i, 9).ToString();
		return registro;
	}

	public async Task<bool> save()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format(@"
			insert into registro_juego(usuario_id,estado_id,municipio_id,puntos,vida,energia,salud,fecha_inicio,fecha_termino) 
			values({0},{1},{2},{3},{4},{5},{6},'{7}','{8}')", usuario_id, estado_id, municipio_id, puntos, vidas, energia, salud, fecha_inicio_s, fecha_termino_s);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		this.usuario_id = estado_id = municipio_id = vidas = energia = salud = 0;
	}
}


[Serializable]
public class DB_Registro_Sesion
{
	private Game gm;
	public int registro_sesion_id;
	public int sesion_id;
	public int usuario_id;
	public string fecha_inicio;
	public string fecha_cierre;
	public DateTime fecha_inicio_;
	public DateTime fecha_cierre_;

	public void init(int registro_sesion_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.registro_sesion_id = registro_sesion_id;
		refresh();
	}

	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from registro_sesion where registro_sesion_id={0}", registro_sesion_id));
		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Registro_Sesion create(ListQuery registros, int i)
	{
		DB_Registro_Sesion registro = new DB_Registro_Sesion();
		registro.registro_sesion_id = registros.get(i, 0).ToInt();
		registro.sesion_id = registros.get(i, 1).ToInt();
		registro.usuario_id = registros.get(i, 2).ToInt();
		registro.fecha_inicio = registros.get(i, 3).ToString();
		//fecha_inicio_ = DateTime.Parse(fecha_inicio);
		registro.fecha_cierre = registros.get(i, 4).ToString();
		//fecha_cierre_ = DateTime.Parse(fecha_cierre);
		return registro;
	}

	public async Task<bool> save()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("insert into registro_sesion(sesion_id,usuario_id) values({0},{1})", sesion_id, usuario_id);
		return await gm.db.Query(query);
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("update registro_sesion set fecha_cierre='{1}' where sesion_id={0} and usuario_id={2}", sesion_id, fecha_cierre, usuario_id);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		usuario_id = registro_sesion_id = sesion_id = 0;
		fecha_inicio = fecha_cierre = "";
	}
}

[Serializable]
public class DB_Textos
{
	private Game gm;
	public int texto_id;
	public string lugar;
	public string contenido_esp;
}


[Serializable]
public class DB_Usuario
{
	private Game gm;
	public int usuario_id;
	public int usuario_server_id;
	public int usuario_local_id;
	public string contraseña;
	public string nombres;
	public string apellido_paterno;
	public string apellido_materno;
	public string fecha_nacimiento;
	public DateTime fecha_nacimiento_;
	public string sexo;
	public string pais;
	public string estado;
	public string municipio;
	public string colonia;
	public string calles;
	public string escuela;
	public string grado;
	public string grupo;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Miscelaneo._Varios.obtener_sprite(gm.config.ruta_carpeta_imagenes + ruta_imagen + ".png");
			}
			return imagen_;
		}
	}
	public bool activo;

	public void init(int usuario_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.usuario_id = usuario_id;
		refresh();
	}

	public void init(int usuario_id, string contraseña)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.usuario_id = usuario_id;
		this.contraseña = contraseña;
		refresh();
	}

	public async Task<bool> login(int usuario_id, string contraseña)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();

		var cadena = "select * from usuarios where usuario_id=" + usuario_id + " and contraseña='" + contraseña + "'";
		var registros = await gm.db.getQuery(cadena);
		if (registros == null || registros.Count == 0)
		{
			clear();
			return false;
		}

		this.usuario_id = registros.get(0, 0).ToInt();
		this.usuario_server_id = registros.get(0, 1).ToInt();
		this.usuario_local_id = registros.get(0, 2).ToInt();
		this.contraseña = registros.get(0, 3).ToString();
		this.nombres = registros.get(0, 4).ToString();
		this.apellido_paterno = registros.get(0, 5).ToString();
		this.apellido_materno = registros.get(0, 6).ToString();
		this.fecha_nacimiento = registros.get(0, 7).ToString();
		this.sexo = registros.get(0, 8).ToString();
		this.pais = registros.get(0, 9).ToString();
		this.estado = registros.get(0, 10).ToString();
		this.municipio = registros.get(0, 11).ToString();
		this.colonia = registros.get(0, 12).ToString();
		this.calles = registros.get(0, 13).ToString();
		this.escuela = registros.get(0, 14).ToString();
		this.grado = registros.get(0, 15).ToString();
		this.grupo = registros.get(0, 16).ToString();
		this.ruta_imagen = registros.get(0, 17).ToString();
		return true;
	}


	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();

		var cadena = contraseña == null || contraseña.Equals("") ? "select * from usuarios where usuario_id=" + usuario_id : "select * from usuarios where usuario_id=" + usuario_id + " and contraseña='" + contraseña + "'";

		var registros = await gm.db.getQuery(cadena);
		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Usuario create(ListQuery registros, int i)
	{
		var gm = GameObject.FindObjectOfType<Game>();
		DB_Usuario usuario = new DB_Usuario();
		usuario.usuario_id = registros.get(i, 0).ToInt();
		usuario.usuario_server_id = registros.get(i, 1).ToInt();
		usuario.usuario_local_id = registros.get(i, 2).ToInt();
		usuario.contraseña = registros.get(i, 3).ToString();
		usuario.nombres = registros.get(i, 4).ToString();
		usuario.apellido_paterno = registros.get(i, 5).ToString();
		usuario.apellido_materno = registros.get(i, 6).ToString();
		usuario.fecha_nacimiento = registros.get(i, 7).ToString();
		usuario.sexo = registros.get(i, 8).ToString();
		usuario.pais = registros.get(i, 9).ToString();
		usuario.estado = registros.get(i, 10).ToString();
		usuario.municipio = registros.get(i, 11).ToString();
		usuario.colonia = registros.get(i, 12).ToString();
		usuario.calles = registros.get(i, 13).ToString();
		usuario.escuela = registros.get(i, 14).ToString();
		usuario.grado = registros.get(i, 15).ToString();
		usuario.grupo = registros.get(i, 16).ToString();
		usuario.ruta_imagen = registros.get(i, 17).ToString();
		usuario.activo = registros.get(i, 18).ToInt() == 1;
		return usuario;
	}

	public async Task<bool> save()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format(@"
		insert into usuarios(
			usuario_local_id,
			contraseña,
			nombres,
			apellido_paterno,
			apellido_materno,
			fecha_nacimiento,
			sexo,
			pais,
			estado,
			municipio,
			colonia,
			calles,
			escuela,
			grado,
			grupo,
			ruta_imagen,
			activo
		) values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}',{16})",
			usuario_id,
			contraseña,
			nombres,
			apellido_paterno,
			apellido_materno,
			fecha_nacimiento,
			sexo,
			pais,
			estado,
			municipio,
			colonia,
			calles,
			escuela,
			grado,
			grupo,
			ruta_imagen,
			activo ? 1 : 0);
		return await gm.db.Query(query);
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format(@"
		update usuarios set
			usuario_local_id={0},
			contraseña='{1}',
			nombres='{2}',
			apellido_paterno='{3}',
			apellido_materno='{4}',
			fecha_nacimiento='{5}',
			sexo='{6}',
			pais='{7}',
			estado='{8}',
			municipio='{9}',
			colonia='{10}',
			calles='{11}',
			escuela='{12}',
			grado='{13}',
			grupo='{14}',
			ruta_imagen='{15}',
			activo={16} where usuario_id={17} ",
			usuario_local_id,
			contraseña,
			nombres,
			apellido_paterno,
			apellido_materno,
			fecha_nacimiento,
			sexo,
			pais,
			estado,
			municipio,
			colonia,
			calles,
			escuela,
			grado,
			grupo,
			ruta_imagen,
			activo ? 1 : 0,
			usuario_id);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		usuario_id = 0;
		usuario_server_id = 0;
		usuario_local_id = 0;
		contraseña = "";
		nombres = "";
		apellido_paterno = "";
		apellido_materno = "";
		fecha_nacimiento = "";
		sexo = "";
		pais = "";
		estado = "";
		municipio = "";
		colonia = "";
		calles = "";
		escuela = "";
		grado = "";
		grupo = "";
		ruta_imagen = "";
		activo = false;
	}
}


[Serializable]
public class DB_Vestimenta
{
	private Game gm;
	public int vestimenta_id;
	public string categoria;
	public string nombre_singular_esp;
	public string nombre_plural_esp;
	public string nombre_singular_wix;
	public string nombre_plural_wix;
	public int nivel;
	public int puntaje;
	public int energia;
	public int salud;
	public float velocidad;
	public float saltos;
	public float resistencia;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int vestimenta_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.vestimenta_id = vestimenta_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from vestimentas where vestimenta_id={0} ", vestimenta_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Vestimenta create(ListQuery registros, int i)
	{
		DB_Vestimenta vestimenta = new DB_Vestimenta();
		vestimenta.vestimenta_id = registros.get(i, 0).ToInt();
		vestimenta.categoria = registros.get(i, 1).ToString();
		vestimenta.nombre_singular_esp = registros.get(i, 2).ToString();
		vestimenta.nombre_plural_esp = registros.get(i, 3).ToString();
		vestimenta.nombre_singular_wix = registros.get(i, 4).ToString();
		vestimenta.nombre_plural_wix = registros.get(i, 5).ToString();
		vestimenta.nivel = registros.get(i, 6).ToInt();
		vestimenta.puntaje = registros.get(i, 7).ToInt();
		vestimenta.salud = registros.get(i, 8).ToInt();
		vestimenta.velocidad = registros.get(i, 9).ToFloat();
		vestimenta.saltos = registros.get(i, 10).ToFloat();
		vestimenta.resistencia = registros.get(i, 11).ToFloat();
		vestimenta.ruta_imagen = registros.get(i, 12).ToString();
		vestimenta.secuencia_audios = registros.get(i, 13).ToString();
		if (vestimenta.secuencia_audios != null || vestimenta.secuencia_audios.Length > 0)
			vestimenta.secuencia_audios_lista = vestimenta.secuencia_audios.Split(',').ToList();
		return vestimenta;
	}

	public void clear()
	{
		velocidad = saltos = 0;
		vestimenta_id = nivel = puntaje = energia = salud = 0;
		nombre_singular_esp = nombre_plural_esp = nombre_singular_wix = nombre_plural_wix = ruta_imagen = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}

[Serializable]
public class DB_Ranking
{
	private Game gm;
	public int usuario_id;
	public string nombres;
	public string apellido_paterno;
	public string apellido_materno;
	public string ruta_imagen;
	public int cantidad;
	public int estado_id;
	public int municipio_id;
	public int posicion;

	public void init(int estado_id, int municipio_id, int usuario_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.estado_id = estado_id;
		this.municipio_id = municipio_id;
		this.usuario_id = usuario_id;
		refresh();
	}

	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = "";

		if (estado_id == -1)
		{
			query = "select * from vista_posicion_global";
		}
		else
		{
			if (municipio_id < 0)
			{
				query = string.Format("select * from vista_posicion_estado_{0}_global", estado_id);
			}
			else
			{
				query = string.Format("select * from vista_posicion_estado_{0}_municipio_{1}", estado_id, municipio_id);
			}
		}

		var registros = await gm.db.getQuery(query);
		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Ranking create(ListQuery registros, int i)
	{
		DB_Ranking ranking = new DB_Ranking();
		ranking.usuario_id = registros.get(i, 0).ToInt();
		ranking.nombres = registros.get(i, 1).ToString();
		ranking.apellido_paterno = registros.get(i, 2).ToString();
		ranking.apellido_materno = registros.get(i, 3).ToString();
		ranking.ruta_imagen = registros.get(i, 15).ToString();
		ranking.cantidad = registros.get(i, 16).ToInt();
		ranking.estado_id = registros.get(i, 17).ToInt();
		ranking.municipio_id = registros.get(i, 17).ToInt();
		ranking.posicion = registros.get(i, 18).ToInt();
		return ranking;
	}

	public void clear()
	{
		this.usuario_id = posicion = estado_id = municipio_id = posicion = cantidad = 0;
		ruta_imagen = nombres = apellido_materno = apellido_paterno = "";
	}
}

[Serializable]
public class DB_Configuracion
{
	private Game gm;
	public int configuracion_id;
	public int version;
	public float volumen_musica;
	public float volumen_efectos;
	public float volumen_habla;

	public void init(int configuracion_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.configuracion_id = configuracion_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from _Configuracion where configuracion_id={0} ", configuracion_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}
	public static DB_Configuracion create(ListQuery registros, int i)
	{
		DB_Configuracion configuracion = new DB_Configuracion();
		configuracion.configuracion_id = registros.get(i, 0).ToInt();
		configuracion.version = registros.get(i, 1).ToInt();
		configuracion.volumen_musica = registros.get(i, 2).ToFloat();
		configuracion.volumen_efectos = registros.get(i, 3).ToFloat();
		configuracion.volumen_habla = registros.get(i, 4).ToFloat();
		return configuracion;
	}

	public async Task<bool> update()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		string query = string.Format("update _Configuracion set version={0}, volumen_musica={1}, volumen_efectos={2}, volumen_habla={3} where configuracion_id={4}", version, volumen_musica, volumen_efectos, volumen_habla, configuracion_id);
		return await gm.db.Query(query);
	}

	public void clear()
	{
		configuracion_id = version = 0;
		volumen_efectos = volumen_musica = volumen_habla = 0;
	}
}
[Serializable]
public class DB_Quiz_Pregunta
{
	private Game gm;
	public int pregunta_id;
	public string municipio_id;
	public string titulo;
	public string texto_pregunta;
	public int puntos_primera;
	public int puntos_segunda;
	public int tipo_de_pregunta;
	public int dificultad;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;
	public List<string> municipio_id_lista;

	public void init(int pregunta_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.pregunta_id = pregunta_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from quiz_pregunta where pregunta_id={0} ", pregunta_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Quiz_Pregunta create(ListQuery registros, int i)
	{
		DB_Quiz_Pregunta pregunta = new DB_Quiz_Pregunta();
		pregunta.pregunta_id = registros.get(i, 0).ToInt();
		pregunta.municipio_id = registros.get(i, 1).ToString();
		pregunta.titulo = registros.get(i, 2).ToString();
		pregunta.texto_pregunta = registros.get(i, 3).ToString();
		pregunta.puntos_primera = registros.get(i, 4).ToInt();
		pregunta.puntos_segunda = registros.get(i, 5).ToInt();
		pregunta.tipo_de_pregunta = registros.get(i, 6).ToInt();
		pregunta.dificultad = registros.get(i, 7).ToInt();
		pregunta.ruta_imagen = registros.get(i, 8).ToString();
		pregunta.secuencia_audios = registros.get(i, 9).ToString();
		if (pregunta.secuencia_audios != null || pregunta.secuencia_audios.Length > 0)
			pregunta.secuencia_audios_lista = pregunta.secuencia_audios.Split(',').ToList();
		if (pregunta.municipio_id != null || pregunta.municipio_id.Length > 0)
			pregunta.municipio_id_lista = pregunta.municipio_id.Split(',').ToList();
		return pregunta;
	}

	public void clear()
	{
		pregunta_id = puntos_primera = puntos_segunda =  tipo_de_pregunta = dificultad = 0;
		ruta_imagen = titulo = texto_pregunta = "";
		municipio_id = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
		municipio_id_lista = null;
	}

}

[Serializable]
public class DB_Quiz_Pista
{
	private Game gm;
	public int pista_id;
	public int pregunta_id;
	public string texto;
	public int costo;
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int pista_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.pista_id = pista_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from quiz_pista where pista_id={0} ", pista_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Quiz_Pista create(ListQuery registros, int i)
	{
		DB_Quiz_Pista pista = new DB_Quiz_Pista();
		pista.pista_id = registros.get(i, 0).ToInt();
		pista.pregunta_id = registros.get(i, 1).ToInt();
		pista.texto = registros.get(i, 2).ToString();
		pista.costo = registros.get(i, 3).ToInt();
		pista.secuencia_audios = registros.get(i, 4).ToString();
		if (pista.secuencia_audios != null || pista.secuencia_audios.Length > 0)
			pista.secuencia_audios_lista = pista.secuencia_audios.Split(',').ToList();
		return pista;
	}

	public void clear()
	{
		pregunta_id = costo = pista_id = 0;
		texto = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}

[Serializable]
public class DB_Quiz_Respuesta
{
	private Game gm;
	public int respuesta_id;
	public int pregunta_id;
	public string texto;
	public int tipo_respuesta;
	public string ruta_imagen;
	public string nombre;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}

	public void init(int respuesta_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.respuesta_id = respuesta_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from quiz_respuesta where respuesta_id={0} ", respuesta_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Quiz_Respuesta create(ListQuery registros, int i)
	{
		DB_Quiz_Respuesta respuesta = new DB_Quiz_Respuesta();
		respuesta.respuesta_id = registros.get(i, 0).ToInt();
		respuesta.pregunta_id = registros.get(i, 1).ToInt();
		respuesta.texto = registros.get(i, 2).ToString();
		respuesta.tipo_respuesta = registros.get(i, 3).ToInt();
		respuesta.ruta_imagen = registros.get(i, 4).ToString();
		respuesta.nombre = registros.get(i, 5).ToString();
		return respuesta;
	}
	public void clear()
	{
		pregunta_id = respuesta_id = tipo_respuesta = 0;
		nombre = ruta_imagen = texto = "";
	}
}
[Serializable]
public class DB_Sitio_Importante
{
	private Game gm;
	public int sitio_importante_id;
	public string titulo;
	public string descripcion;
	public string despedida;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public string secuencia_audios;
	public List<string> secuencia_audios_lista;

	public void init(int sitio_importante_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.sitio_importante_id = sitio_importante_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from sitios_importantes where sitio_importante_id={0} ", sitio_importante_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Sitio_Importante create(ListQuery registros, int i)
	{
		DB_Sitio_Importante sitio = new DB_Sitio_Importante();
		sitio.sitio_importante_id = registros.get(i, 0).ToInt();
		sitio.titulo = registros.get(i, 1).ToString();
		sitio.descripcion = registros.get(i, 2).ToString();
		sitio.ruta_imagen = registros.get(i, 3).ToString();
		sitio.secuencia_audios = registros.get(i, 4).ToString();
		if (sitio.secuencia_audios != null || sitio.secuencia_audios.Length > 0)
			sitio.secuencia_audios_lista = sitio.secuencia_audios.Split(',').ToList();
		return sitio;

	}
	public void clear()
	{
		sitio_importante_id = 0;
		ruta_imagen = titulo = descripcion = despedida = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
	}
}
[Serializable]
public class DB_Temas_por_Nivel
{
	private Game gm;
	public int municipio_id;
	public int tema_id;
	public string descripcion;
	public string secuencia_audios;
	public string ruta_imagen;
	private Sprite imagen_;
	public Sprite imagen
	{
		get
		{
			if (imagen_ == null)
			{
				imagen_ = Resources.Load<Sprite>(ruta_imagen);
			}
			return imagen_;
		}
	}
	public List<string> secuencia_audios_lista;

	public void init(int tema_id)
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		this.tema_id = tema_id;
		refresh();
	}
	public async void refresh()
	{
		if (gm == null)
			gm = GameObject.FindObjectOfType<Game>();
		var registros = await gm.db.getQuery(string.Format("select * from temas_por_nivel where tema_id ={0} ", tema_id));

		if (registros == null || registros.Count == 0)
		{
			clear();
			return;
		}
	}

	public static DB_Temas_por_Nivel create(ListQuery registros, int i)
	{
		DB_Temas_por_Nivel temas = new DB_Temas_por_Nivel();

		temas.municipio_id = registros.get(i, 0).ToInt();
		temas.tema_id = registros.get(i, 1).ToInt();
		temas.descripcion = registros.get(i, 2).ToString();
		temas.ruta_imagen = registros.get(i, 3).ToString();
		temas.secuencia_audios = registros.get(i, 4).ToString();
		if (temas.secuencia_audios != null || temas.secuencia_audios.Length > 0)
			temas.secuencia_audios_lista = temas.secuencia_audios.Split(',').ToList();
		return temas;
	}
	public void clear()
	{
		municipio_id = tema_id = 0;
		descripcion = "";
		secuencia_audios = "";
		secuencia_audios_lista = null;
		ruta_imagen = "";
	}
}
