using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    
    // baslangicta butun puan ekranlari isim ve etiketine gore yazilarini otomatik olusturuyor
    void Start()
    {
        switch (gameObject.transform.parent.gameObject.transform.parent.gameObject.tag)
        {

            case "Addition":
                gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "+" + transform.parent.transform.parent.name;
                break;

            case "Multiplication":
                gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "x" + transform.parent.transform.parent.name;
                break;

            case "Subtraction":
                gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "-" + transform.parent.transform.parent.name;
                break;

            case "Division":
                gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "/" + transform.parent.transform.parent.name;
                break;
            default:
                break;
        }
        
    }

    
}
