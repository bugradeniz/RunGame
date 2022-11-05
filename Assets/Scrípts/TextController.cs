using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Util;
public class TextController : MonoBehaviour
{
    public string TextId;
    Pref pf = new Pref();

    // Use this for initialization
    void Start()
    {
        /*I18n.changeLanguage("TR");*/

        setText();

    }
    public string getLanguageText()
    {

        string lang = pf.getS("Language");
        switch (lang)
        {
            case "EN":
                return "ENGLISH";
            case "TR":
                return "TÜRKÇE";
            default:
                return "";
        }

    }
    public void setText()
    {
        if (GetComponent<Text>() != null)
        {
            Text text = GetComponent<Text>();

            if (TextId == "ISOCode")
                text.text = I18n.GetLanguage();
            else if (TextId == "lang")
                text.text = getLanguageText();
            else
                text.text = I18n.Fields[TextId];
        }
        if (GetComponent<TMP_Text>() != null)
        {
            TMP_Text text = GetComponent<TMP_Text>();

            if (TextId == "ISOCode")
                text.SetText(I18n.GetLanguage());
            else if (TextId == "lang")
                text.SetText(getLanguageText());
            else
                text.SetText(I18n.Fields[TextId]);

        }
    }

   
}