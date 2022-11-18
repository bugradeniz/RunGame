using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Util;
public class MainMenuController : MonoBehaviour
{
    Pref pf = new Pref(); // kutuphanedeki player prefs kontrolcu sinifina erisildi.
    AdManager aM = new AdManager();

    public GameObject exitPanel;// cikis yapilsin mi sorusunu tutan panel.
    public AudioSource buttonSound;

    public GameObject loadingPanel;
    public Slider loadingBar;


    void Start()
    {
        buttonSound.volume = pf.getF("FXSound");

        aM.requestRA();
        aM.showRA();

    }
    IEnumerator loadAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        loadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress + 0.1f;
            
            yield return null;
        }

    }

    public void loadScene(int index)
    {
        buttonSound.Play();
        StartCoroutine(loadAsync(index));

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
