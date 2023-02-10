using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Config : MonoBehaviour
{
    private Game gm;

    public string ruta_base_datos;
    public bool existe_ruta_base_datos;

    public string ruta_tmp_acciones;
    public bool existe_ruta_tmp_acciones;

    public string ruta_carpeta_imagenes;
    public bool existe_ruta_carpeta_imagenes;

    public string ruta_carpeta_logs_sesion;
    public bool existe_carpeta_logs_sesion;

    public bool usar_online;

    public string prefijo_experimento;
    public string direccion_servidor;

    private Dictionary<string, string> dic_config = new Dictionary<string, string>();

    private string ruta_completa = "";

    public Text txt_salida;

    public bool verificar_carpetas()
    {

        if (gm == null)
            gm = FindObjectOfType<Game>();
#if !UNITY_EDITOR && UNITY_WEBGL
        gm.en_linea = true;
        direccion_servidor = "192.168.1.74:3000";
        return true;
#else

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        ruta_completa = Application.streamingAssetsPath;
#elif UNITY_ANDROID
        ruta_completa = Application.persistentDataPath;
#endif
        var archivo_configuracion = ruta_completa + "/config.txt";
        bool existe = File.Exists(archivo_configuracion);

        if (!existe)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            DirectoryCopy(Application.streamingAssetsPath, Application.persistentDataPath, true);
#elif UNITY_ANDROID
            string[] carpetas =
            {
                "/Archivos_Log",
                "/Capturas",
                "/Default",
                "/Default/Data",
                "/Default/Data/Actions",
                "/Default/Photos",
            };

            foreach (var carpeta in carpetas)
            {
                var ruta = Application.persistentDataPath + carpeta;
                if (Directory.Exists(ruta))
                    continue;
                Directory.CreateDirectory(ruta);
            }

            string[] archivos =
            {
                "/Default/Photos/default_H.png",
                "/Default/Photos/default_M.png",
            };

            string[] valores_niños =
            {
                DataBase_Estructura.img_niño,
                DataBase_Estructura.img_niña
            };

            for (int i = 0; i < archivos.Length; i++)
            {
                var archivo = archivos[i];
                var ruta = Application.persistentDataPath + archivo;
                if (File.Exists(ruta))
                    continue;
                File.WriteAllBytes(ruta, Convert.FromBase64String(valores_niños[i]));
            }

            var ruta_db = Application.persistentDataPath + @"/Default/Data/MundoWixarika.db";
            gm.db.crear_archivo(ruta_db);
#endif
        }
        cargar_configuracion();
        return existe;
#endif
    }

    public void cargar_configuracion()
    {
        string archivo_dir_carpeta = ruta_completa + "/config.txt";
        if (File.Exists(archivo_dir_carpeta))
        {
            dic_config.Clear();
            string[] lineas_archivo = File.ReadAllLines(archivo_dir_carpeta);

            foreach (var item in lineas_archivo)
            {
                string[] data = item.Split('=');
                dic_config.Add(data[0], data[1]);
            }


            ruta_base_datos = dic_config["ruta_base_datos"];
            existe_ruta_base_datos = File.Exists(ruta_base_datos);

            ruta_tmp_acciones = dic_config["ruta_tmp_acciones"];
            existe_ruta_tmp_acciones = File.Exists(ruta_tmp_acciones);

            ruta_carpeta_imagenes = dic_config["ruta_carpeta_imagenes"];
            existe_ruta_carpeta_imagenes = Directory.Exists(ruta_carpeta_imagenes);

            ruta_carpeta_logs_sesion = dic_config["ruta_carpeta_logs_sesion"];
            existe_carpeta_logs_sesion = Directory.Exists(ruta_carpeta_logs_sesion);

            usar_online = dic_config["usar_en_linea"] == "true";

            gm.en_linea = usar_online;

            direccion_servidor = dic_config["direccion_servidor"];
            prefijo_experimento = dic_config["prefijo_experimento"];
        }
        else
        {
            dic_config["ruta_base_datos"] = ruta_completa + @"/Default/Data/MundoWixarika.db";
            dic_config["ruta_tmp_acciones"] = ruta_completa + @"/Default/Data/Actions/actions.txt";
            dic_config["ruta_carpeta_imagenes"] = ruta_completa + @"/Default/Photos/";
            dic_config["ruta_carpeta_logs_sesion"] = ruta_completa + @"/Archivos_Log/";
            dic_config["direccion_servidor"] = "158.97.121.143:3000";
            dic_config["prefijo_experimento"] = "Normal";
#if UNITY_WEBGL
            dic_config["usar_en_linea"] = "true";
#else
            dic_config["usar_en_linea"] = "false";
#endif

            crear_archivo();
            cargar_configuracion();
        }
    }

    public void crear_archivo()
    {
        string archivo_dir_carpeta = ruta_completa + "/config.txt";
        if (File.Exists(archivo_dir_carpeta))
        {
            File.Delete(archivo_dir_carpeta);
        }
        using (StreamWriter sw = new StreamWriter(archivo_dir_carpeta))
        {
            sw.WriteLine(string.Format(@"ruta_base_datos={0}
ruta_tmp_acciones={1}
ruta_carpeta_imagenes={2}
ruta_carpeta_logs_sesion={3}
direccion_servidor={4}
prefijo_experimento={5}
usar_en_linea={6}",
              dic_config["ruta_base_datos"],
              dic_config["ruta_tmp_acciones"],
              dic_config["ruta_carpeta_imagenes"],
              dic_config["ruta_carpeta_logs_sesion"],
              dic_config["direccion_servidor"],
              dic_config["prefijo_experimento"],
              dic_config["usar_en_linea"]
            ));
        }
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();

        // If the destination directory doesn't exist, create it.
        Directory.CreateDirectory(destDirName);

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string tempPath = Path.Combine(destDirName, file.Name);
            file.CopyTo(tempPath, true);
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
            }
        }
    }

    private void agregar_salida(params object[] values)
    {
        if (txt_salida == null)
            return;
        for (int i = 0; i < values.Length; i++)
        {
            txt_salida.text += values[i]+",";
        }

        txt_salida.text += "\n";
    }

}
