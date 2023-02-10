using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CambioEscena : MonoBehaviour
{
    public GameObject Funcion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CambiarEscena()
    {
        SceneManager.LoadScene("Escena1");
        Funcion.GetComponent<Inicializar>().Guardar();
    }
}
