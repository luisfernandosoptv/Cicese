using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Perfil_Usuario_Default : Perfil_Usuario_Configuracion
{
	public Dictionary<int, DB_Alimento> alimentos = new Dictionary<int, DB_Alimento>();
	public Dictionary<int, DB_Animal> animales = new Dictionary<int, DB_Animal>();
	public Dictionary<int, DB_Arma> armas = new Dictionary<int, DB_Arma>();
	public List<DB_Conversacion> conversaciones = new List<DB_Conversacion>();
	public List<DB_Conversacion_Usuario> conversaciones_usuarios = new List<DB_Conversacion_Usuario>();
	public Dictionary<int, DB_Curacion> curaciones = new Dictionary<int, DB_Curacion>();
	public List<DB_Curacion_Enfermedad> curaciones_enfermedades = new List<DB_Curacion_Enfermedad>();
	public List<DB_Derivado_Elemento> derivados_elementos = new List<DB_Derivado_Elemento>();
	public Dictionary<int, DB_Diario_Viaje> diarios_viajes = new Dictionary<int, DB_Diario_Viaje>();
	public Dictionary<int, DB_Diario_Viaje_Usuario> diarios_viajes_usuarios = new Dictionary<int, DB_Diario_Viaje_Usuario>();
	public Dictionary<int, DB_Enemigo> enemigos = new Dictionary<int, DB_Enemigo>();
	public Dictionary<int,DB_Enfermedad> enfermedades = new Dictionary<int, DB_Enfermedad>();
	public Dictionary<int, DB_Enfermedad_Usuario> enfermedades_usuario = new Dictionary<int, DB_Enfermedad_Usuario>();
	public Dictionary<int,DB_Estado> estados= new Dictionary<int, DB_Estado>();
	public List<DB_Estado_Desbloqueado> estados_desbloqueados = new List<DB_Estado_Desbloqueado>();
	public List<DB_Estrella> estrellas = new List<DB_Estrella>();
	public Dictionary<int, DB_Extra> extras = new Dictionary<int, DB_Extra>();
	public Dictionary<int, DB_Planta_Medicinal> plantas_medicinales = new Dictionary<int, DB_Planta_Medicinal>();
	public Dictionary<int,DB_Extra_Usuario> extras_usuarios = new Dictionary<int,DB_Extra_Usuario>();
	public List<DB_Inventario_Usuario> inventario_usuario = new List<DB_Inventario_Usuario>();
	public List<DB_Municipio> municipios = new List<DB_Municipio>();
	public Dictionary<int,DB_Objeto_Especial> objetos_especiales = new Dictionary<int, DB_Objeto_Especial>();
	public Dictionary<int,DB_Objeto_Espiritual> objetos_espirituales = new Dictionary<int, DB_Objeto_Espiritual>();
	public List<DB_Objetivo> objetivos = new List<DB_Objetivo>();
	public List<DB_Puntaje> puntajes = new List<DB_Puntaje>();
	public List<DB_Textos> textos = new List<DB_Textos>();
	public List<DB_Animal_Enfermedad> animales_enfermedades = new List<DB_Animal_Enfermedad>();
	public DB_Usuario usuario = new DB_Usuario();
	public Dictionary<int, DB_Vestimenta> vestimentas = new Dictionary<int, DB_Vestimenta>();
	public DB_Municipio nivel_seleccionado=new DB_Municipio();
	public Dictionary<int, DB_Sitio_Importante> sitios_importantes = new Dictionary<int, DB_Sitio_Importante>();
	public Dictionary<int, DB_Temas_por_Nivel> temas_por_nivel = new Dictionary<int, DB_Temas_por_Nivel>();
	public Dictionary<int, DB_Quiz_Pregunta> quiz_preguntas = new Dictionary<int, DB_Quiz_Pregunta>();
	public Dictionary<int, DB_Quiz_Respuesta> quiz_respuestas = new Dictionary<int, DB_Quiz_Respuesta>();
	public Dictionary<int, DB_Quiz_Pista> quiz_pistas = new Dictionary<int, DB_Quiz_Pista>();
	public List<DB_Explicacion_Inicio> explicacion_inicio = new List<DB_Explicacion_Inicio>();

	public bool ultimo_juego_ganado;
	public string token = "";
	public string tipo_Session = "";
	public string cadena_aux = "";
	public string tipo_nivel_seleccionado = "JUEGO";

	private Game gm;

	public string nombre_archivo
	{
		get
		{
			return string.Format("{0}_{1}_{2}_{3}", usuario.usuario_id, token, System.DateTime.Now.ToString("yy-MM-dd"), UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
	}

	public async void cargar_default_estado_municipio(int estado_id,int municipio_id)
	{
        if (objetivos.Exists(x=>x.municipio_id==municipio_id))
			return;
        
		var registros = await gm.db.getQuery("select * from quiz_pregunta where municipio_id="+municipio_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Quiz_Pregunta.create(registros, i);
			quiz_preguntas.Add(obj.pregunta_id, obj);

			var registros_2 = await gm.db.getQuery("select * from quiz_respuesta where pregunta_id="+obj.pregunta_id);
			for (int e = 0; e < registros_2.Count; e++)
			{
				var obj_2 = DB_Quiz_Respuesta.create(registros_2, e);
				quiz_respuestas.Add(obj_2.respuesta_id, obj_2);
			}

			var registros_3 = await gm.db.getQuery("select * from quiz_pista where pregunta_id=" + obj.pregunta_id);
			for (int e = 0; e < registros_3.Count; e++)
			{
				var obj_3 = DB_Quiz_Pista.create(registros_3, e);
				quiz_pistas.Add(obj_3.pista_id, obj_3);
			}
		}
		
		registros = await gm.db.getQuery("select * from temas_por_nivel where municipio_id="+municipio_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Temas_por_Nivel.create(registros, i);
			temas_por_nivel.Add(obj.tema_id, obj);
		}

		registros = await gm.db.getQuery("select * from objetivos where municipio_id=" + municipio_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Objetivo.create(registros, i);
			objetivos.Add(obj);
		}
	}

	public async void cargar_default_tienda()
	{
		if (curaciones.Count > 0)
			return;
		var registros = await gm.db.getQuery("select * from curaciones");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Curacion.create(registros, i);
			curaciones.Add(obj.curacion_id, obj);
		}

		registros = await gm.db.getQuery("select * from curaciones_enfermedades");
		for (int i = 0; i < registros.Count; i++)
		{
			var curacion_id = registros.get(i, 0).ToInt();
			var enfermedad_id = registros.get(i, 1).ToInt();

			var obj = new DB_Curacion_Enfermedad();
			obj.curacion_id = curacion_id;
			obj.enfermedad_id = enfermedad_id;
			curaciones_enfermedades.Add(obj);
		}

		registros = await gm.db.getQuery("select * from derivados_elementos");
		for (int i = 0; i < registros.Count; i++)
		{
			var cantidad = registros.get(i, 0).ToFloat();
			var derivado_id = registros.get(i, 1).ToInt();
			var tipo_derivado = registros.get(i, 2).ToString();
			var grupo = registros.get(i, 3).ToString();
			var costo = registros.get(i, 4).ToFloat();
			var elemento_id = registros.get(i, 5).ToInt();
			var tipo_elemento = registros.get(i, 6).ToString();

			var obj = new DB_Derivado_Elemento();
			obj.cantidad = cantidad;
			obj.derivado_id = derivado_id;
			obj.tipo_derivado = tipo_derivado;
			obj.grupo = grupo;
			obj.costo = costo;
			obj.elemento_id = elemento_id;
			obj.tipo_elemento = tipo_elemento;

			derivados_elementos.Add(obj);
		}
	}

	public async void cargar_default_textos( )
	{
		if (textos.Count > 0)
			return;
		var registros = await gm.db.getQuery("select * from textos");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = new DB_Textos();
			obj.texto_id = registros.get(i, 0).ToInt();
			obj.lugar = registros.get(i, 1).ToString();
			obj.contenido_esp = registros.get(i, 2).ToString();
			textos.Add(obj);
		}
	}

	public async void cargar_default( )
	{
		gm = FindObjectOfType<Game> ();

		var registros = await gm.db.getQuery("select * from alimentos");
        for (int i = 0; i < registros.Count; i++)
        {
			var obj = DB_Alimento.create(registros, i);
			alimentos.Add(obj.alimento_id,obj);
        }

		registros = await gm.db.getQuery("select * from animales");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Animal.create(registros, i);
			animales.Add(obj.animal_id,obj);
		}

		registros = await gm.db.getQuery("select * from armas");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Arma.create(registros, i);
			armas.Add(obj.arma_id,obj);
		}

		registros = await gm.db.getQuery("select * from enfermedades");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Enfermedad.create(registros, i);
			enfermedades.Add(obj.enfermedad_id, obj);
		}

		registros = await gm.db.getQuery("select * from enemigos");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Enemigo.create(registros, i);
			enemigos.Add(obj.enemigo_id, obj);
		}

		registros = await gm.db.getQuery("select * from animales_enfermedades");
		for (int i = 0; i < registros.Count; i++)
		{
			var animal_id = registros.get(i, 0).ToInt();
			var enfermedad_id = registros.get(i, 1).ToInt();

			var obj = new DB_Animal_Enfermedad();
			obj.animal_id = animal_id;
			obj.enfermedad_id = enfermedad_id;
			animales_enfermedades.Add(obj);
		}

		registros = await gm.db.getQuery("select * from objetos_especiales");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Objeto_Especial.create(registros, i);
			objetos_especiales.Add(obj.objeto_especial_id,obj);
		}

		registros = await gm.db.getQuery("select * from objetos_espirituales");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Objeto_Espiritual.create(registros, i);
			objetos_espirituales.Add(obj.objeto_espiritual_id,obj);
		}

		registros = await gm.db.getQuery("select * from vestimentas");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Vestimenta.create(registros, i);
			vestimentas.Add(obj.vestimenta_id,obj);
		}

		registros = await gm.db.getQuery("select * from plantas_medicinales");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Planta_Medicinal.create(registros, i);
			plantas_medicinales.Add(obj.planta_medicinal_id, obj);
		}

		base.cargar_default_configuracion();

		reset_default();
		
	}

	public async Task<bool> cargar_default_login( )
	{
		if(gm==null)
			gm = FindObjectOfType<Game>();

		if (extras.Count > 0)
			return false;

		var registros = await gm.db.getQuery("select * from conversaciones");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Conversacion.create(registros, i);
			conversaciones.Add(obj);
		}

		registros = await gm.db.getQuery("select * from estados");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Estado.create(registros, i);
			estados.Add(obj.estado_id, obj);
		}

		registros = await gm.db.getQuery("select * from municipios");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Municipio.create(registros, i);
			municipios.Add(obj);
		}

		registros = await gm.db.getQuery("select * from extras");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Extra.create(registros, i);
			extras.Add(obj.extra_id, obj);
		}

		nivel_seleccionado = municipios.First();
		return true;
	}

	public async void cargar_default_jugar( )
	{
		if (gm == null)
			gm = FindObjectOfType<Game>();

		if (diarios_viajes.Count > 0)
			return;

		var registros = await gm.db.getQuery("select * from diarios_viajes");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Diario_Viaje.create(registros, i);
			diarios_viajes.Add(obj.diario_viaje_id, obj);
		}

		registros = await gm.db.getQuery("select * from explicacion_inicio");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Explicacion_Inicio.create(registros, i);
			explicacion_inicio.Add(obj);
		}

		registros = await gm.db.getQuery("select * from sitios_importantes");
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Sitio_Importante.create(registros, i);
			sitios_importantes.Add(obj.sitio_importante_id, obj);
		}
	}

	public void reset_default()
	{
		estados_desbloqueados.Clear();
		extras_usuarios.Clear();
		inventario_usuario.Clear();
		enfermedades_usuario.Clear();
		puntajes.Clear();
		usuario.clear();
		estrellas.Clear();
		diarios_viajes_usuarios.Clear();

		base.reset_default_registros();

		tipo_Session="SINSESION";
		nivel_seleccionado = municipios.Count>0?municipios.First():null;
	}

	public async Task<bool> login_default(string id_="INVITADO", string contraseña="1")
	{
		if(gm==null)
			gm = FindObjectOfType<Game> ();

		string id_encontrar = id_;
		if (id_.Equals("INVITADO"))
        {
			await gm.db.Query("insert into usuarios(nombres,contraseña) values('INVITADO" + Random.Range(100, 999) + "','1')");
			id_encontrar = (await gm.db.getQuery("select max(usuario_id) from usuarios")).get(0, 0).ToString();
		}

		bool data = await cargar_datos_sesion_default(id_encontrar, contraseña);
		if (!data)
		{
			reset_default();
			return false;
		}
		tipo_Session = id_.Equals ("INVITADO") ? "INVITADO" : "REGISTRADO";
		return true;
	}

	private async Task<bool> cargar_datos_sesion_default(string id_,string contraseña)
	{
		if(gm==null)
			gm = FindObjectOfType<Game> ();

		bool login=await usuario.login(int.Parse (id_),contraseña);
		if (!login)
			return false;

		await cargar_default_login();

		var registros = await gm.db.getQuery("select * from estados_desbloqueados where usuario_id=" + usuario.usuario_id );
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Estado_Desbloqueado.create(registros, i);
			estados_desbloqueados.Add(obj);
		}

		registros = await gm.db.getQuery("select * from puntajes where usuario_id=" + usuario.usuario_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Puntaje.create(registros, i);
			puntajes.Add(obj);
		}

		registros = await gm.db.getQuery("select * from estrellas where usuario_id=" + usuario.usuario_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Estrella.create(registros, i);
			estrellas.Add(obj);
		}

		registros = await gm.db.getQuery("select * from extras_usuarios where usuario_id=" + usuario.usuario_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Extra_Usuario.create(registros, i);
			extras_usuarios.Add(obj.extra_id,obj);
		}

		registros = await gm.db.getQuery("select * from inventario_usuarios where usuario_id="+usuario.usuario_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var obj = DB_Inventario_Usuario.create(registros, i);
			inventario_usuario.Add(obj);
		}

		registros = await gm.db.getQuery("select * from enfermedades_usuarios where usuario_id=" + usuario.usuario_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var enfermedad_id = registros.get(i, 0).ToInt();
			var usuario_id = registros.get(i, 1).ToInt();
			var cantidad = registros.get(i, 2).ToInt();

			var obj = new DB_Enfermedad_Usuario();
			obj.enfermedad_id = enfermedad_id;
			obj.usuario_id = usuario_id;
			obj.cantidad = cantidad;
			enfermedades_usuario.Add(obj.enfermedad_id,obj);
		}

		registros = await gm.db.getQuery("select * from diarios_viajes_usuarios where usuario_id=" + usuario.usuario_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var diario_viaje_id = registros.get(i, 0).ToInt();
			var usuario_id = registros.get(i, 1).ToInt();
			var obtenido = registros.get(i, 2).ToInt()==1;

			var obj = new DB_Diario_Viaje_Usuario();
			obj.diario_viaje_id = diario_viaje_id;
			obj.usuario_id = usuario_id;
			obj.obtenido = obtenido;
			diarios_viajes_usuarios.Add(diario_viaje_id, obj);
		}

		registros = await gm.db.getQuery("select * from conversaciones_usuarios where usuario_id=" + usuario.usuario_id);
		for (int i = 0; i < registros.Count; i++)
		{
			var conversacion_grupo = registros.get(i, 0).ToInt();
			var usuario_id = registros.get(i, 1).ToInt();

			var obj = new DB_Conversacion_Usuario();
			obj.conversacion_grupo = conversacion_grupo;
			obj.usuario_id = usuario_id;
			conversaciones_usuarios.Add(obj);
		}

		await base.cargar_datos_sesion_default_registros(usuario.usuario_id);


		nivel_seleccionado = municipios.First();
		token = Miscelaneo._Varios.cadenaAleatoria ();
		return true;
	}
}