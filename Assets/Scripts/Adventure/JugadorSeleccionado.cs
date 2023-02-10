using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorSeleccionado : MonoBehaviour
{
    public GameObject Personaje1;
    public GameObject Personaje2;
    public GameObject Personaje3;
    public GameObject Personaje4;
    public GameObject Personaje5;
    public GameObject Personaje6;
    public GameObject Personaje7;
    public GameObject Personaje8;
    public GameObject Personaje9;
    public GameObject Personaje10;
    public GameObject Personaje11;
    public GameObject Personaje12;
    public GameObject Personaje13;
    public GameObject Personaje14;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        
        if (P1 == true)
        {
            Personaje1.SetActive(true);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P2 == true)
        {
            Personaje2.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P3 == true)
        {
            Personaje3.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P4 == true)
        {
            Personaje4.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P5 == true)
        {
            Personaje5.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P6 == true)
        {
            Personaje6.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P7 == true)
        {
            Personaje7.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P8 == true)
        
        {
            Personaje8.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P9 == true)
        {
            Personaje9.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P10 == true)
        {
            Personaje10.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P11 == true)
        {
            Personaje11.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje12);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P12 == true)
        {
            Personaje12.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje13);
            Destroy(Personaje14);
        }
                if (P13 == true)
        {
            Personaje13.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje14);
        }
                if (P14 == true)
        {
            Personaje14.SetActive(true);
            Destroy(Personaje1);
            Destroy(Personaje2);
            Destroy(Personaje3);
            Destroy(Personaje4);
            Destroy(Personaje5);
            Destroy(Personaje6);
            Destroy(Personaje7);
            Destroy(Personaje8);
            Destroy(Personaje9);
            Destroy(Personaje10);
            Destroy(Personaje11);
            Destroy(Personaje12);
            Destroy(Personaje13);
        }

    }
}