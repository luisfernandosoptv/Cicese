using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class _Sonidos_Hablar : MonoBehaviour
{
    protected Game gm;
    public AudioSource hablar;

    public string ruta_sonido_hombre_hablado;
    public string ruta_sonido_mujer_hablado;
    public string ruta_sonido_hombre_generado;
    public string ruta_sonido_mujer_generado;

    public AudioClip audio_en_turno(string nombre,string genero, bool generados)
    {
        if (gm == null)
            gm = FindObjectOfType<Game>();

        string[] no_validos = { "¿", "?", "!", "¡" };
        no_validos.ToList().ForEach(x => { nombre = nombre.Replace(x, ""); });

        string ruta = "";
        if (generados)
        {
            ruta=genero.Equals("Femenino") ? ruta_sonido_mujer_generado : ruta_sonido_hombre_generado;
        }
        else
        {
            ruta=genero.Equals("Femenino") ? ruta_sonido_mujer_hablado : ruta_sonido_hombre_hablado;
        }

        AudioClip clip = Resources.Load<AudioClip>($"{ruta}/{nombre}");
        return clip;
    }

    public void reproducir(string nombre, string genero,bool generados=false)
    {
        var clip = audio_en_turno(nombre,genero, generados);

        if (clip == null)
            return;

        hablar.clip = clip;
        hablar.volume = gm.perfil.configuracion.volumen_habla;
        hablar.Play();
    }
}
