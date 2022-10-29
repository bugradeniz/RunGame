using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

public class ChooseLevelController : MonoBehaviour
{
    Pref pf = new Pref(); // kutuphanemizdeki  player prefs kontrolcu sinifina erisildi.
    public GameObject[] levelButtons;// level buttonlarinin listesi tutuluyor. level miktarina gore artirilabilir.
    public int lastLevel;//en son kalinan leveli tutan degisken
    public Sprite lockedLevelSprite;//son kalinan levelden sonraki levellerin erisimi engellendiginde kilitli gosteren sprite.
    void Start()
    {
        lastLevel=pf.getI("LastLevel"); // kutuphane ile playerprefden son kalinan level bilgisi alindi
        setLevelButtons();// butonlarin otomatik olarak duruma gore kurulumunu yapan fonksiyon.
    }


    // sirayla levelleri numaralarini ve kilit durumlarini gosteren ve numaralarina gore butonlara listener  atayan fonksiyon
    void setLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < lastLevel)// son kalinan levele kadar olan butonlar burada kuruluyor.
            {
                int levelIndex = i + 1;//level butonlari listede 0 dan basladigi icin index 1 artirildi.
                levelButtons[i].transform.GetChild(0).GetComponent<Text>().text = "" + (levelIndex); // her butonun icindeki text objesinin text degerine level indexi atandi
                levelButtons[i].GetComponent<Button>().onClick.AddListener(delegate { selectedLevelLoader(levelIndex); });// her butonun basildiginda calisan fonksiyonu,
                                                                                                                          // level indexini deger olarak alan selectedLevelLoader(..) olarak atandi
                                                                                                                          // bu fonksiyon asagida.
            }
            else//son levelden sonraki leveller , yani kilitli olmasi gereken leveller burada kuruluyor.
            {

                levelButtons[i].GetComponent<Image>().sprite = lockedLevelSprite;//elimizdeki kilit spritei buton resmina atandi.
                levelButtons[i].GetComponent<Button>().interactable = false;//buton dukunulmaz hale geldi
                levelButtons[i].GetComponentInChildren<Text>().text = "";// icindeki text degerinin bos oldugundan emin olundu.
            }
        }

    }


    // Aktif butonlara kendi indexi ile cagirilan fonksiyon.
    public void selectedLevelLoader(int index)
    {
        SceneManager.LoadScene(index+4);// leveller build settings de 5 den basladgi icin gerekli index +4 ile donduruldu
    }

    public void back()
    {
        SceneManager.LoadScene(0); // geri tusunun calistirdigi fonksiyon.ana menuye donuyor.
    }
}
