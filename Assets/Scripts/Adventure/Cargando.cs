using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cargando : MonoBehaviour
{
    public int TiempoDeEspera=10;
    public Text texto;
    private void Start()
    {
        int nivelACargar = CargaNivel.siguienteNivel;
        StartCoroutine(IniciarCarga(nivelACargar));

    }

    IEnumerator IniciarCarga(int nivel)

    {
       
        yield return new WaitForSeconds(TiempoDeEspera);
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while(!operacion.isDone)
        {
            if(operacion.progress >= 0.9f)
            {
                texto.text = "Presiona una tecla para continuar";
                if(Input.anyKey)
                {
                    operacion.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
