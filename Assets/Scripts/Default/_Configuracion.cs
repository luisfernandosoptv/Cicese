using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class _Configuracion : MonoBehaviour
{
	private Game gm;
    private _Cambiar_Escena cambiar_escena;
    
    public _Mensaje_Emergente configuracion;

    public Slider slider_musica;
    public Slider slider_efectos;
    public Slider slider_habla;

    public AudioSource sonido_efectos;
    public AudioSource sonido_habla;

    void Awake()
    {
    	gm=FindObjectOfType<Game>();
        cambiar_escena = FindObjectOfType<_Cambiar_Escena>();
    }

    public void valor_cambiado(GameObject col)
    {
        if (col.name.Equals("slider_efectos_fondo"))
        {
            gm.log.guardar_accion("CONFIGURACION", "CAMBIO_MUSICA_FONDO",slider_musica.value);

            gm.perfil.configuracion.volumen_musica = slider_musica.value;
            sonido_habla.Stop();
            sonido_efectos.Stop();
        }
        else if (col.name.Equals("slider_efectos_sonido"))
        {
            gm.log.guardar_accion("CONFIGURACION", "CAMBIO_EFECTOS_SONIDO", slider_efectos.value);

            gm.perfil.configuracion.volumen_efectos = slider_efectos.value;

            sonido_efectos.volume = slider_efectos.value;
            if (!sonido_efectos.isPlaying)
                sonido_efectos.Play();
            sonido_habla.Stop();
        }
        else if (col.name.Equals("slider_efectos_habla"))
        {
            gm.log.guardar_accion("CONFIGURACION", "CAMBIO_EFECTOS_HABLA", slider_habla.value);

            gm.perfil.configuracion.volumen_habla = slider_habla.value;

            sonido_habla.volume = slider_habla.value;
            if (!sonido_habla.isPlaying)
                sonido_habla.Play();
            sonido_efectos.Stop();
        }
    }

    public async void validar_seleccion(GameObject col)
    {
        if (col.name.Equals("Ver_Configuracion")||col.name.Equals("abrir_configuracion"))
        {
            gm.log.guardar_accion("CONFIGURACION", "VER_CONFIGURACION");

            slider_musica.value = gm.perfil.configuracion.volumen_musica;
            slider_efectos.value = gm.perfil.configuracion.volumen_efectos;
            slider_habla.value = gm.perfil.configuracion.volumen_habla;

            sonido_habla.Stop();
            sonido_efectos.Stop();

            configuracion.show();
        }
        else if (col.name.Equals("Cerrar"))
        {
            sonido_habla.Stop();
            sonido_efectos.Stop();

            gm.log.guardar_accion("CONFIGURACION", "CERRAR_CONFIGURACION");
            configuracion.hide();

            actualizar_extras();
            actualizar_fondo();
            await gm.perfil.configuracion.update();
        }
        else if (col.name.Equals("Mapa"))
        {
            cambiar_escena.cambiar_escena("Mapa");
        }
        else if (col.name.Equals("Reiniciar"))
        {
            cambiar_escena.cambiar_escena(SceneManager.GetActiveScene().name);
        }
        else if (col.name.Equals("Inicio"))
        {
            cambiar_escena.cambiar_escena("Inicio");
        }
        else if (col.name.Equals("Registrar"))
        {
            cambiar_escena.cambiar_escena("Registrar");
        }
    }

    public virtual void actualizar_extras()
    {
    }

    public virtual void actualizar_fondo()
    {
    }
}
