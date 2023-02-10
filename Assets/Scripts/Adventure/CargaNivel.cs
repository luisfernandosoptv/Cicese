using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CargaNivel
{
    public static int siguienteNivel; 

    public static void NivelCarga(int nombre)
    {
        siguienteNivel = nombre;
        SceneManager.LoadScene(1);
    }
        public static void CreditosCarga(int nombre)
    {
        siguienteNivel = nombre;
        SceneManager.LoadScene(2);
    }
        public static void ConfiguracionCarga(int nombre)
    {
        siguienteNivel = nombre;
        SceneManager.LoadScene(3);
    }
}
