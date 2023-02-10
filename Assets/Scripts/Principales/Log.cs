using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Log : MonoBehaviour 
{
    private Game gm;

    public void guardar_accion(params object[] valores)
    {
        if (gm == null)
            gm = FindObjectOfType<Game>();
        /*if (gm.perfil.usuario==null||gm.perfil.usuario.id_niño== 0)
            return;
        
        /*string escena = SceneManager.GetActiveScene().name;
        var ruta_sesion_local = gm.config.ruta_carpeta_logs_sesion + string.Format("{0}_{1}_{2}_{3}_local.csv", -1, gm.perfil.usuario.id_niño, gm.perfil.usuario.nombres, gm.perfil.token);

        string cad = string.Format("{0},{1},{2},{3},{4},{5},{6},",
                                    System.DateTime.Now,
                                    System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",System.Globalization.CultureInfo.InvariantCulture),
                                    System.DateTime.Now.Millisecond,
                                    -1,
                                    gm.perfil.usuario.id_niño,
                                    gm.perfil.token,
                                    SceneManager.GetActiveScene().name);
        
        foreach (var item in valores)
        {
            cad += item + ",";
        }

        cad = cad.Substring(0, cad.Length - 1);
        
        if(gm.config.existe_carpeta_logs_sesion)
            Miscelaneo._Varios.appendText(ruta_sesion_local, cad, false);*/
            
    }
}
