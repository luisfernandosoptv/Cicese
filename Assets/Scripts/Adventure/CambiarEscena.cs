using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CambiarEscena : MonoBehaviour
{

    public int numeroEscenaPlay;
    public int numeroEscenaConfiguracion;
    public int numeroEscenaCreditos;


   
    public void CargarPlay()
    {
        CargaNivel.NivelCarga(numeroEscenaPlay); 
    }
        public void CargarCreditos()
    {
        CargaNivel.CreditosCarga(numeroEscenaCreditos);
    }
    public void CargarConfiguracion()
    {
        CargaNivel.ConfiguracionCarga(numeroEscenaConfiguracion);
    }

    
}
