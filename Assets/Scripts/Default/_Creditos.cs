using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class _Creditos : MonoBehaviour
{
	private Game gm;

    public Text txt_nombre;
    public GameObject btn;

    public GameObject creditos;

    void Awake()
    {
    	gm=FindObjectOfType<Game>();

        string nombre = Application.productName;
        string version = Application.version;

        txt_nombre.text = string.Format("{0} v{1}\n2022 © Copyright CICESE Todos los Derechos Reservados", nombre, version);

        btn.SetActive(SceneManager.GetActiveScene().name.Contains("Inicio"));
    }

    public void validar_seleccion(GameObject col)
    {
    	if (col.name.Equals("Ver_Creditos"))
        {
            gm.log.guardar_accion("CREDITOS", "VER_CREDITOS");
            creditos.SetActive(true);
        }
        else if (col.name.Equals("Cerrar_Creditos"))
        {
            gm.log.guardar_accion("CREDITOS", "CERRAR_CREDITOS");
            creditos.SetActive(false);
        }
    }
}
