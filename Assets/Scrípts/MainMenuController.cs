using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;
public class MainMenuController : MonoBehaviour
{
    Pref pf = new Pref(); // kutuphanedeki player prefs kontrolcu sinifina erisildi.

    public GameObject exitPanel;// cikis yapilsin mi sorusunu tutan panel.
    public AudioSource buttonSound;

    void Start()
    {

    }


    public void loadScene(int index)
    {
        buttonSound.Play();
        SceneManager.LoadScene(index);

    }
    public void playLevel()
    {
        
        loadScene(pf.getI("LastLevel") + 4);// build ayarlarinda levellar 5. siradan basliyor.---- level 1 = index 5

    }
    public void exit()
    {
        buttonSound.Play();
        exitPanel.SetActive(true);// cikis sorusunu soran paneli aktif hale getiren buton fonksiyonu.
    }
    public void exitAnswer(bool answer) // evet veya hayir buttonlarinin cevap gonderdigi fonksiyon
    {
        buttonSound.Play();
        if (answer)
        {

            Application.Quit(); //cevap evet ise oyundan cikiyor
        }
        else
        {
            exitPanel.SetActive(false);// cevap hayir ise paneli kapatip ana menuye geri donuyor.
        }
    }
}
