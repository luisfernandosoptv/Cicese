using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Miscelaneo{
	public class _Varios : MonoBehaviour 
	{
		public static void sumar_texto(MonoBehaviour mono, int inicio, int limite, int incremento, float tiempo_ciclo, Text txt_mostrar, Action<bool> completo = null,string prefix="")
		{
			mono.StartCoroutine(sumar_texto_(inicio,limite, incremento, tiempo_ciclo, txt_mostrar, completo,prefix));
		}

		private static IEnumerator sumar_texto_(int inicio, int limite, int incremento, float tiempo_ciclo, Text txt_mostrar, Action<bool> completo = null,string prefix= "")
		{
			int temporal = 0;
			if(incremento>0)
            {
				temporal = inicio;
				for (int i = inicio; i <= limite; i += incremento)
				{
					txt_mostrar.text = temporal == 0 ? "0" : prefix + temporal.ToString("###,###,###,###");
					temporal += incremento;
					yield return new WaitForSeconds(tiempo_ciclo);
				}
				
				txt_mostrar.text = limite.ToString("###,###,###,###");
			}
            else
            {
				temporal = limite;
				for (int i = limite; i >= inicio; i += incremento)
				{
					txt_mostrar.text = temporal == 0 ? "0" : prefix + temporal.ToString("###,###,###,###");
					temporal += incremento;
					yield return new WaitForSeconds(tiempo_ciclo);
				}
				
				txt_mostrar.text = inicio.ToString("###,###,###,###");
			}

			
			if (completo != null)
				completo(temporal == limite);
		}

		public static void agregar_texto(
			MonoBehaviour mono, 
			Text txt,
			string texto,
			float frecuencia = 1,
			bool por_caracteres=false,
			AudioSource audio = null,
			float limpiar=-1,
			Action termino=null) 
		{
			mono.StopCoroutine("agregar");
			mono.StartCoroutine (agregar (txt, texto, frecuencia, por_caracteres, audio,limpiar, termino));
		}

		public static IEnumerator agregar (
			Text txt,
			string texto,
			float frecuencia,
			bool por_caracteres,
			AudioSource audio,
			float limpiar,
			Action termino
			) {

            if (por_caracteres)
            {
				for (int i = 0; i < texto.Length; i++)
				{
					string caracter = texto.Substring(i, 1);
					txt.text += caracter;
					if (audio != null)
					{
						if (!audio.isPlaying)
							audio.Play();
					}
					yield return new WaitForSecondsRealtime(frecuencia);
				}
            }
            else
            {
				var palabras = texto.Split(' ');
				for (int i = 0; i < palabras.Length; i++)
				{
					string palabra = palabras[i];
					txt.text += palabra+" ";
					if (audio != null)
					{
						if (!audio.isPlaying)
							audio.Play();
					}
					yield return new WaitForSecondsRealtime(frecuencia);
				}
			}
			
			if (limpiar != -1)
			{
				yield return new WaitForSecondsRealtime(limpiar);
				txt.text = "";
			}
			if(termino!=null)
				termino();
		}

		public static void appendText(string ruta, string contenido, bool use_default = true){
			var ruta_ = ruta;
			if (use_default)
				ruta_=Application.streamingAssetsPath+ruta;
			using (StreamWriter sw = new StreamWriter (ruta_,true,System.Text.Encoding.UTF8)) {
				sw.WriteLine (contenido);
			}
		}
		public static void cambiar_variable(MonoBehaviour mono, Action original, float duracion = 0)
		{
			mono.StartCoroutine(cambiar(original, duracion));
		}
		public static IEnumerator cambiar(Action original, float duracion = 0)
		{
			yield return new WaitForSecondsRealtime(duracion);
			original();
		}
		
		public static Sprite obtener_sprite(string ruta)
		{
			if (File.Exists (ruta)) 
			{
				byte[] fileData= File.ReadAllBytes (ruta);
				Texture2D txt = new Texture2D (16,16,TextureFormat.RGBA32, false);
				txt.LoadImage (fileData);
				txt.Apply();
				Rect rec = new Rect (0, 0, txt.width, txt.height);
				Sprite sp = Sprite.Create (txt, rec, new Vector2 (0, 0), 1);
				return sp;
			}
			return null;
		}

		public static string cadenaAleatoria(){
			string[] datos= {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
			string cad="";
			for(int i=0;i<=10;i++)
			{
				cad+=datos[UnityEngine.Random.Range(0,datos.Length)];
			}
			return cad;
		}
		public static string se(int segundos){
			int m=0;
			float s=0;
			string sd;

			m=segundos/60;
			s = segundos % 60;
			sd = s.ToString ();
			if (s <= 9) {
				sd = "0" + sd;
			}
			return m + ":" + sd;
		}

		public static List<T> desordenarLista<T>(List<T> input)
		{
			List<T> arr = input;
			List<T> arrDes = new List<T>();
			System.Random random = new System.Random();
			while (arr.Count > 0)
			{
				int val = random.Next(0, arr.Count);
				arrDes.Add(arr[val]);
				arr.RemoveAt(val);
			}

			return arrDes;
		}
		public static void disparar(MonoBehaviour mono,Transform proyectil,Transform destino,IEnumerator callback=null){
			mono.StartCoroutine (disparar_(proyectil,destino,mono,callback));
		}
		private static IEnumerator disparar_(Transform proyectil, Transform destino, MonoBehaviour mono = null, IEnumerator callback = null)
		{
			float target_Distance = Vector3.Distance(proyectil.transform.position, destino.position);
			float projectile_Velocity = target_Distance / (Mathf.Sin(2 * 30 * Mathf.Deg2Rad) / 25f);
			float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(30 * Mathf.Deg2Rad);
			float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(30 * Mathf.Deg2Rad);
			float flightDuration = target_Distance / Vx;

			proyectil.transform.rotation = Quaternion.LookRotation(destino.position - proyectil.transform.position);
			proyectil.transform.GetChild(0).rotation = Quaternion.LookRotation(new Vector3(0, 0, 0.0001f));
			proyectil.transform.SetParent(destino.transform);
			float elapse_time = 0;

			while (elapse_time < flightDuration)
			{
				if (!proyectil)
				{
					break;
				}
				proyectil.transform.Translate(0, (Vy - (25f * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
				elapse_time += Time.deltaTime;
				yield return null;
			}
			if (proyectil == null)
				yield return null;
			proyectil.transform.localPosition = Vector3.zero;
			proyectil.transform.rotation = Quaternion.identity;
			proyectil.transform.GetChild(0).rotation = Quaternion.identity;
			if (callback != null)
			{
				mono.StartCoroutine(callback);
			}
		}
		
		private static bool cond_extra = true;
		public static bool sucedio_evento(bool cond1, bool cond2)
		{
			if (cond1 && !cond2 && cond_extra)
				cond_extra = false;
			else if (!cond_extra)
			{
				if (cond2 && !cond1)
				{
					cond_extra = true;
					return true;
				}
			}
			return false;
		}


		public static string formato_puntos(int val)
		{
			return val==0?"0":val.ToString ("###,###,###,###");
		}
	}
}
