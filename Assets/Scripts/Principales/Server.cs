using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public enum Tipos_Peticiones
{
	GET,
	POST
};

public class Server : MonoBehaviour
{
	private Game gm;
	public string ip_server = "";
	private UnityWebRequest request = null;

	void Start()
	{
		if (gm == null)
			gm = FindObjectOfType<Game>();
		ip_server = gm.config.direccion_servidor;
	}

	public async Task<string> enviar_peticion(Tipos_Peticiones method, string ruta, WWWForm form = null)
	{
		if (gm == null)
			gm = FindObjectOfType<Game>();
		ip_server = gm.config.direccion_servidor;

		string url = string.Format("http://{0}{1}", ip_server, ruta);

		if (method == Tipos_Peticiones.GET)
		{
			request = UnityWebRequest.Get(url);
		}
		else if (method == Tipos_Peticiones.POST)
		{
			request = UnityWebRequest.Post(url, form);
		}

		request.SetRequestHeader("Access-Control-Allow-Credentials", "true");
		request.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
		request.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
		request.SetRequestHeader("Access-Control-Allow-Origin", "*");

		string contenido = "";
		try
		{
			await request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.Log(request.error);
			}
			else
			{
				contenido = request.downloadHandler.text;
			}
		}
		catch (Exception ex)
		{
			print(ex);
		}
		finally
		{
			request.Dispose();
			request = null;
		}

		return contenido;
	}

	private void OnDestroy()
	{
		if (request != null)
			request.Dispose();
	}

	IEnumerator enviar_(UnityWebRequest request, System.Action<string> result)
	{
		yield return request.SendWebRequest();

		if (request.result != UnityWebRequest.Result.Success)
		{
			Debug.Log(request.error);
			result("");
		}
		else
		{
			result(request.downloadHandler.text);
		}
	}

}
public struct UnityWebRequestAwaiter : INotifyCompletion
{
	private UnityWebRequestAsyncOperation asyncOp;
	private Action continuation;

	public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
	{
		this.asyncOp = asyncOp;
		continuation = null;
	}

	public bool IsCompleted { get { return asyncOp.isDone; } }

	public void GetResult() { }

	public void OnCompleted(Action continuation)
	{
		this.continuation = continuation;
		asyncOp.completed += OnRequestCompleted;
	}

	private void OnRequestCompleted(AsyncOperation obj)
	{
		continuation?.Invoke();
	}
}

public static class ExtensionMethods
{
	public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
	{
		return new UnityWebRequestAwaiter(asyncOp);
	}
}