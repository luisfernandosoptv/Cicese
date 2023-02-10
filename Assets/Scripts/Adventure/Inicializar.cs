using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Inicializar : MonoBehaviour
{
    [SerializeField] private InputField nameInput;//Indicador de donde se escribe el nombre del jugador 
    [SerializeField] private InputField apellidoPInput;
    [SerializeField] private InputField apellidoMInput;
    [SerializeField] private InputField edadInput;
    public Dropdown estadoInput;

    [SerializeField] private InputField municipioInput;
    [SerializeField] private InputField localidadInput;
    string Vacio="";
    
    [SerializeField] public string NombreJugador;//Variable que guarda el nombre del jugador 
    [SerializeField] public string ApellidoP;
    [SerializeField] public string ApellidoM;
    [SerializeField] public string Edad;
    [SerializeField] public int EstadoIndice;
    [SerializeField] public string Estado;
    [SerializeField] public string Municipio;
    [SerializeField] public string Localidad;
    public GameObject MenuRegistro;
    public GameObject MenuPersonaje;
    public GameObject BtnJugar;
    

    public GameObject Pers1;
    public GameObject Pers2;
    public GameObject Pers3;
    public GameObject Pers4;
    public GameObject Pers5;
    public GameObject Pers6;
    public GameObject Pers7;
    public GameObject Pers8;
    public GameObject Pers9;
    public GameObject Pers10;
    public GameObject Pers11;
    public GameObject Pers12;
    public GameObject Pers13;
    public GameObject Pers14;

    private string dbName = "URI=file:Prueba.db";//nombre del archivo en base de datos

    public bool P1;
    public bool P2;
    public bool P3;
    public bool P4;
    public bool P5;
    public bool P6;
    public bool P7;
    public bool P8;
    public bool P9;
    public bool P10;
    public bool P11;
    public bool P12;
    public bool P13;
    public bool P14;
    // Start is called before the first frame update
    private void awake() 
    {
        P1 = PlayerPrefs.GetInt("SeleccionePersonaje1") == 1;
        P2 = PlayerPrefs.GetInt("SeleccionePersonaje2") == 1;
        P3 = PlayerPrefs.GetInt("SeleccionePersonaje3") == 1;
        P4 = PlayerPrefs.GetInt("SeleccionePersonaje4") == 1;
        P5 = PlayerPrefs.GetInt("SeleccionePersonaje5") == 1;
        P6 = PlayerPrefs.GetInt("SeleccionePersonaje6") == 1;
        P7 = PlayerPrefs.GetInt("SeleccionePersonaje7") == 1;
        P8 = PlayerPrefs.GetInt("SeleccionePersonaje8") == 1;
        P9 = PlayerPrefs.GetInt("SeleccionePersonaje9") == 1;
        P10 = PlayerPrefs.GetInt("SeleccionePersonaje10") == 1;
        P11 = PlayerPrefs.GetInt("SeleccionePersonaje11") == 1;
        P12 = PlayerPrefs.GetInt("SeleccionePersonaje12") == 1;
        P13 = PlayerPrefs.GetInt("SeleccionePersonaje13") == 1;
        P14 = PlayerPrefs.GetInt("SeleccionePersonaje14") == 1;
    }
    void Start() {
        LimpiarCampos();
        Regresar(); 
        ComienzaPrefs();
       
    }   

    public void registro()
    {
        
        AsignaPlayerprefs();
        if(nameInput.text==""||apellidoPInput.text==""||apellidoMInput.text==""||edadInput.text==""||municipioInput.text==""||localidadInput.text=="")
        {
            CambiarColor();
        }
        else
        {
            NombreJugador=nameInput.text;
            ApellidoP=apellidoPInput.text;
            ApellidoM=apellidoMInput.text;
            Edad=edadInput.text;
            Municipio=municipioInput.text;
            Localidad=localidadInput.text;
            EstadoIndice=estadoInput.value;
            MenuRegistro.SetActive(false);
            MenuPersonaje.SetActive(true);
            DesactivaImagenes();
        }

    }




    // Update is called once per frame
    void Update()
    {
        
        if(P1 == false  && P2 == false  && P3 == false&& P4 == false&& P5 == false&& P6 == false&& P7 == false&& P8 == false&& P9 == false&& P10 == false&& P11 == false&& P12 == false&& P13 == false&& P14 == false)
        {
            P1 = true;
        }

    }
    public void Regresar(){
        MenuRegistro.SetActive(true);
        MenuPersonaje.SetActive(false);
    }
    private void CambiarColor(){
        nameInput.image.color = Color.red;
        apellidoPInput.image.color = Color.red;
        apellidoMInput.image.color = Color.red;
        edadInput.image.color = Color.red;
        municipioInput.image.color = Color.red;
        localidadInput.image.color = Color.red;
    }
   void AsignaPlayerprefs() {
        PlayerPrefs.SetString("NameInput", nameInput.text);
        PlayerPrefs.GetString("ApellidoPInput",apellidoPInput.text);
        PlayerPrefs.GetString("ApellidoMInput", apellidoMInput.text);
        PlayerPrefs.GetString("EdadInput",edadInput.text);
        PlayerPrefs.GetString("MunicipioInput", municipioInput.text);
        PlayerPrefs.GetString("LocalidadInput", localidadInput.text);
        PlayerPrefs.GetString("EstadoInput", Estado);
    }

    void ComienzaPrefs() {
        nameInput.text=PlayerPrefs.GetString("NameInput");
        apellidoPInput.text=PlayerPrefs.GetString("ApellidoPInput");
        apellidoMInput.text=PlayerPrefs.GetString("ApellidoMInput");
        edadInput.text=PlayerPrefs.GetString("EdadInput");
        Vacio=PlayerPrefs.GetString("EstadoInput");
        municipioInput.text=PlayerPrefs.GetString("MunicipioInput");
        localidadInput.text=PlayerPrefs.GetString("LocalidadInput");
    }
    private void DesactivaImagenes() 
    {
        Pers1.SetActive(false);
        Pers2.SetActive(false);
        Pers3.SetActive(false);
        Pers4.SetActive(false);
        Pers5.SetActive(false);
        Pers6.SetActive(false);
        Pers7.SetActive(false);
        Pers8.SetActive(false);
        Pers9.SetActive(false);
        Pers10.SetActive(false);
        Pers11.SetActive(false);
        Pers12.SetActive(false);
        Pers13.SetActive(false);
        Pers14.SetActive(false);
    }

    public void ActivaP1() 
    {
        DesactivaImagenes();
        perDesactivados();
        P1 = true;
        Pers1.SetActive(true);
        }
    
    public void ActivaP2() 
    {
        DesactivaImagenes();
        perDesactivados();
        P2 = true;
        Pers2.SetActive(true);
    }

        public void ActivaP3() 
    {
        DesactivaImagenes();
        perDesactivados();
        P3 = true;
        Pers3.SetActive(true);  
    }
        public void ActivaP4() 
    {
        DesactivaImagenes();
        perDesactivados();
        P4 = true;
        Pers4.SetActive(true); 
    }
        public void ActivaP5() 
    {
        DesactivaImagenes();
        perDesactivados();
        P5 = true;
        Pers5.SetActive(true);
    }
        public void ActivaP6() 
    {
        DesactivaImagenes();
        perDesactivados();
        P6 = true;
        Pers6.SetActive(true); 
    }
        public void ActivaP7() 
    {
        DesactivaImagenes();
        perDesactivados();
        P7 = true;
        Pers7.SetActive(true);  
    }
        public void ActivaP8() 
    {
        DesactivaImagenes();
        perDesactivados();
        P8 = true;
        Pers8.SetActive(true);  
    }
        public void ActivaP9() 
    {
        DesactivaImagenes();
        perDesactivados();
        P9 = true;
        Pers9.SetActive(true);  
    }
        public void ActivaP10() 
    {
        DesactivaImagenes();
        perDesactivados();
        P10 = true;
        Pers10.SetActive(true); 
    }
        public void ActivaP11() 
    {
        DesactivaImagenes();
        perDesactivados();
        P11 = true;
        Pers11.SetActive(true);  
    }
        public void ActivaP12() 
    {
        DesactivaImagenes();
        perDesactivados();
        P12 = true;
        Pers12.SetActive(true); 
    }
        public void ActivaP13() 
    {
        DesactivaImagenes();
        perDesactivados();
        P13 = true;
        Pers13.SetActive(true);     
    }
        public void ActivaP14() 
    {
        DesactivaImagenes();
        perDesactivados();
        P14 = true;
        Pers14.SetActive(true);   
    }
    public void Jugar () {
        Guardar();
        ubicaPersonajes();
        VerificaEstado();
        IngresaDatos();
    }

    private void ubicaPersonajes() {
        Pers1.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers2.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers3.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers4.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers5.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers6.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers7.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers8.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers9.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers10.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers11.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers12.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers13.transform.localScale =new Vector2 (1.2f,1.2f);
        Pers14.transform.localScale =new Vector2 (1.2f,1.2f);
    }
    private void perDesactivados(){
        BtnJugar.SetActive(true);
        P1 = false;
        P2 = false;
        P3 = false;
        P4 = false;
        P5 = false;
        P6 = false;
        P7 = false;
        P8 = false;
        P9 = false;
        P10 = false;
        P11 = false;
        P12 = false;
        P13 = false;
        P14 = false;
    }

    private void VerificaEstado()
    {Estado=Vacio;

         using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (IDbCommand dbCmd= connection.CreateCommand())
                {
                    string sqlQuery = "SELECT * FROM estados WHERE estado_id='" + (EstadoIndice+1) + "'";
                    dbCmd.CommandText=sqlQuery;
                    using(IDataReader reader = dbCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Estado=reader.GetString(1);
                            Debug.Log(reader.GetString(1));
                            // connection.Close();
                            // reader.Close();
                        }
                    }
                }
                // connection.Close();
            }
    }
    private void IngresaDatos()
    {
          using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO usuarios(nombre, apellidoP, apellidoM, edad, estado, municipio, localidad) VALUES ('" + NombreJugador + "','" + ApellidoP + "','" + ApellidoM + "','" + Edad + "','" + Estado + "','" + Municipio + "','" + Localidad + "')";
                    Debug.Log(NombreJugador);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
    }
    private void  LimpiarCampos() {
            nameInput.text="";
            apellidoPInput.text="";
            apellidoMInput.text="";
            edadInput.text="";
            municipioInput.text="";
            localidadInput.text="";
    }
    public void Guardar()
    {
        PlayerPrefs.SetInt("SeleccionePersonaje1", P1 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje2", P2 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje3", P3 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje4", P4 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje5", P5 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje6", P6 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje7", P7 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje8", P8 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje9", P9 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje10", P10 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje11", P11 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje12", P12 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje13", P13 ? 1 : 0);
        PlayerPrefs.SetInt("SeleccionePersonaje14", P14 ? 1 : 0);
    }

    
    public void CerrarJuego()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else 
        Application.Quit();
    #endif
    }

 

}