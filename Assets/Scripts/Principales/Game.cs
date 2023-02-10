using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Game : MonoBehaviour
{
	public DataBase db;
	public Log log;
	public Config config;
	public Perfil_Usuario perfil;
    public Server server;

    public bool modo_teclado;
    public bool en_linea;

    void Awake()
    {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_WEBGL
        modo_teclado = true;
#elif UNITY_ANDROID
        modo_teclado = false;
#endif
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        bool existe = config.verificar_carpetas();
        db.obtener_ruta_base_datos();
        if (!existe)
        {
#if UNITY_ANDROID
            db.inicializar_db();
#endif
        }
        
        Perfil_Usuario per = FindObjectOfType<Perfil_Usuario> ();
		if (per!=null)
        {
			perfil = per;
		}
        else
        {
			GameObject g = new GameObject ();
			per  =g.AddComponent<Perfil_Usuario> ();
			g.name="Perfil_Jugador";
			perfil = per;
			per.cargar_data ();
            DontDestroyOnLoad(g);

            login();
        }
	}

    public async void login()
    {
        perfil.cargar_default_textos();
        perfil.cargar_default_jugar();

        var lista = new int[] {7,13,19,26,32,36 };
        int anterior = 0;
        for (int i = 1; i <= 6; i++)
        {  
            var item = lista[i - 1];
            for (int e = anterior; e < item; e++)
            {
                perfil.cargar_default_estado_municipio( i, e+1);
            }
            anterior = item;
        }

        bool login=await perfil.login("1", "1" );
        if (login)
        {
            print("Inicio de sesión");
        }

        //perfil.nivel_seleccionado = perfil.municipios.Find(x => x.estado_id == 5 && x.municipio_id == 24);
    }

    private int cantidad = 0;

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            cantidad = 0;   
            string escena = SceneManager.GetActiveScene().name;
            string ruta_carpeta = Application.streamingAssetsPath + "/Capturas/";
            var archivos=Directory.EnumerateFiles(ruta_carpeta);
            foreach (var file in archivos)
            {
                if (file.Contains(".meta"))
                    continue;
                if (file.Contains(escena))
                {
                    var s= file.Split('/');
                    var nom = s[s.Length - 1];
                    cantidad++;
                }
            }
            cantidad++;
            string nombre = escena + "_" + cantidad+"_.png";
            ScreenCapture.CaptureScreenshot( ruta_carpeta+nombre );
            print(nombre);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPaused = true;
#endif
        }
    }
}
