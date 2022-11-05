using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

public class CustomizeController : MonoBehaviour
{
    Pref pf = new Pref();// kutuphanemizdeki  player prefs kontrolcu sinifina erisildi.
    Data dt = new Data();// kutuphanemizdeki veri kontrolcu sinifina erisildi.

    public AudioSource[] buttonSound;

    public Text pointText;// mevcut puani gosteren text


    public GameObject[] hats;// karakterin sapkalarinin tutuldugu liste.objeler editor ile karakterin icinde gerekli konuma getirildi. pasif olarak tutuluyorlar.
                             // konumu:Character/hips/Spine/Spine1/Spine2/Neck/Head//HeadTop_End/
    public Text hatText;//gosterilen sapkanin ismini tutan degisken
    int hatIndex;// gosterilen sapkanin indexini tutan degisken


    public GameObject[] sticks;// karakterin sopalarinin tutuldugu liste.objeler editor ile karakterin icinde gerekli konuma getirildi. pasif olarak tutuluyorlar.
                               //// konumu:Character/hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftHand/LeftHadIndex1/LeftHandIndex2/
    public Text stickText;//gosterilen sopanin ismini tutan degisken
    int stickIndex;//gosterilen sopanin indexini tutan degisken


    public SkinnedMeshRenderer characterSkin;//karakterin materialini degistirilmek icin renderini tutan deger 
    public Material[] skins;// karakter kiyafetlerini tutan liste.
    public Text skinText;//gosterilen kiyafetin adini gosteren text.
    int skinIndex;//gosterilen kiyafetin indexini tutan degisken.

    public Button[] buyButtons;
    public Button[] equiptButtons;
    public Button[] unequiptButtons;


    public Button[] rightButtons;//bir sonraki esyayi gosteren butonlar. [0]=sapka,[1]=sopa,[2]=kiyafet
    public Button[] leftButtons;// bir onceki esyayi gosteren butonlar. [0]=sapka,[1]=sopa,[2]=kiyafet

    [Header("Write Data Here")] // burada esya bilgilerini dosya olarak kaydetmek icin objeler bulunuyor. kolayca ekleyip cikartmak icin gelistirilecekk

    public Items items;         // esya listelerini tutan obje..
    public bool save = false;   // editor icinde listedeki degisiklikleri kaydetmek icin buton olarak kullaniliyor.



    void Start()
    {
        // burada dosya okuma ve yazma olusturma isllemleri donuyor suan biraz karisik

        /*dt.items = items;

        dt.create();
*/
        dt.read();
        items = dt.items;

        for (int i = 0; i < buttonSound.Length; i++)
        {

            buttonSound[i].volume = pf.getF("FXSound");
        }




        pf.setI("Point", 300); // testler icin basta 300 papel veriyor.




        showItemsAndTexts();//ilk sahne acildiginda mevcut esyalar giydiriliyor

        updateButtons();// ilk sahne acildiginda butonlarin kullanilabilirlik durumunu indexlere gore ayarliyor

    }

    void Update()
    {
        // save butonuna (boolean tick) basildiginda items listesini kutuphane yardimi ile kayit ediyor.
        if (save && items != null)
        {
            dt.items = items;
            dt.write();
            save = false;
            Debug.Log("KAYIT EDILDI!!!!");
        }// kayit edilmek istenen obje bos olmamali....
        else if (save)
        {
            save = false;
            Debug.Log("HATA.... BOS VERI KAYIT ETME...");

        }
    }

    //------------------------- BUTONLAR

    public void backButton()
    {
        buttonSound[0].Play();
        SceneManager.LoadScene(0);
    }
    public void refreshButton()
    {
        buttonSound[1].Play();
        showItemsAndTexts(); // mevcut esyalar giydiriliyor

        updateButtons();//  butonlarin kullanilabilirlik durumunu indexlere gore ayarliyor
    }
    public void buyButton(int key)
    {
        
        switch (key)// her bir buy button bir anahtar degiskeni gonderiyor. bu degisken buton listese indexleri ile ayni. [0]=sapka,[1]=sopa,[2]=kiyafet
        {
            case 0: //button sapka butonu ile burada calisiyor.Obje burada sapka

                if (pf.getI("Point") >= items.hatItems[hatIndex].price)                   //Puanimiz sapkanin fiyatina yetiyorsa satin alma gerceklesiyor 
                {
                    buttonSound[2].Play();
                    items.hatItems[hatIndex].sold = true;                               //kutuphane ile cektigimiz sapkalardan secili sapkanin satin alindi bilgisi true olarak degistiriliyor.
                    int newPoint = pf.getI("Point") - items.hatItems[hatIndex].price;     //
                    pf.setI("Point", newPoint);                                          // yeni puan guncelleniyor.
                    updateButtons();                                                    // update buttons fonksiyon butun butonlari olmasi gerektigi duruma sokuyor.
                }
                else
                {
                    Debug.Log("pis fakir");                                             // para yetmiyorsa yapilacaklar
                }
                break;
            case 1://button sopa butonu ile burada calisiyor.Obje burada sopa.$$$$$$$$$$$$  USTTEKI ILE AYNI
                if (pf.getI("Point") >= items.stickItems[stickIndex].price)
                {
                    buttonSound[2].Play();
                    items.stickItems[stickIndex].sold = true;
                    int newPoint = pf.getI("Point") - items.stickItems[stickIndex].price;
                    pf.setI("Point", newPoint);
                    updateButtons();
                }
                else
                {
                    Debug.Log("pis fakir");
                }

                break;
            case 2://button kiyafet butonu ile burada calisiyor.$$$$$$$$$$$$$$$$$$$$$$$$$$$  USTTEKI ILE AYNI
                if (pf.getI("Point") >= items.skinItems[skinIndex].price)
                {
                    buttonSound[2].Play();
                    items.skinItems[skinIndex].sold = true;
                    int newPoint = pf.getI("Point") - items.skinItems[skinIndex].price;
                    pf.setI("Point", newPoint);
                    updateButtons();
                }
                else
                {
                    Debug.Log("pis fakir");
                }

                break;
            default:
                break;
        }
    }
    public void equiptButton(int key)
    {
        buttonSound[1].Play();
        switch (key)    // her bir buy button bir anahtar degiskeni gonderiyor. bu degisken buton listese indexleri ile ayni. [0]=sapka,[1]=sopa,[2]=kiyafet
        {
            case 0:     //button sapka butonu ile burada calisiyor.Obje burada sapka


                pf.setI("CurrentHat", hatIndex);                                    // kutuphanedeki player pref classi ile  sahip olunan sapka indexini secili sapka indexi yapiyoruz.

                updateButtons();                                                    // update buttons fonksiyon butun butonlari olmasi gerektigi duruma sokuyor.




                break;
            case 1:     //button sopa butonu ile burada calisiyor.Obje burada sopa.$$$$$$$$$$$$  USTTEKI ILE AYNI
                pf.setI("CurrentStick", stickIndex);

                updateButtons();
                break;
            case 2:     //button kiyafet butonu ile burada calisiyor.$$$$$$$$$$$$$$$$$$$$$$$$$$$  USTTEKI ILE AYNI
                pf.setI("CurrentSkin", skinIndex);

                updateButtons();

                break;
            default:
                break;
        }
    }
    public void unequiptButton(int key)
    {
        buttonSound[1].Play();
        switch (key)    // her bir buy button bir anahtar degiskeni gonderiyor. bu degisken buton listese indexleri ile ayni. [0]=sapka,[1]=sopa,[2]=kiyafet
        {
            case 0:     //button sapka butonu ile burada calisiyor.Obje burada sapka


                pf.setI("CurrentHat", 0);                                           // kutuphanedeki player pref classi ile  sahip olunan sapka indexini default yani 0 yapiyoruz.

                updateButtons();                                                    // update buttons fonksiyon butun butonlari olmasi gerektigi duruma sokuyor.




                break;
            case 1:     //button sopa butonu ile burada calisiyor.Obje burada sopa.$$$$$$$$$$$$  USTTEKI ILE AYNI
                pf.setI("CurrentStick", 0);

                updateButtons();
                break;
            case 2:     //button kiyafet butonu ile burada calisiyor.$$$$$$$$$$$$$$$$$$$$$$$$$$$  USTTEKI ILE AYNI
                pf.setI("CurrentSkin", 0);

                updateButtons();

                break;
            default:
                break;
        }
    }
    public void rightButton(int key)
    {
        buttonSound[0].Play();
        switch (key)// her bir sag button bir anahtar degiskeni gonderiyor. bu degisken buton listese indexleri ile ayni. [0]=sapka,[1]=sopa,[2]=kiyafet
        {
            case 0: //button sapka butonu ile burada calisiyor.Obje burada sapka

                hats[hatIndex].SetActive(false);                            //mevcut indexteki obje once pasif hale getiriliyor.
                hatIndex++;                                                 // sonra index 1 artiriliyor.
                hatIndex = Mathf.Min(hatIndex, hats.Length - 1);            //indexin olmasi gereken degerleri asmasi engelleniyor.
                hats[hatIndex].SetActive(true);                             // yeni indexteki obje aktif hale getiriliyor.
                hatText.text = items.hatItems[hatIndex].name;               // ONEMLI ######## Item kayitlarindaki yeni indexteki objenin ismi text icine yazdiriliyor. Hats indexleri ile items.hatsItems listesindeki indexler suanda esit.

                updateButtons();                                            // her buton isleminin sonunda butonlarin durumunu guncellemek icin buton guncelleme fonksiyonu cagiriliyor


                break;
            case 1://button sopa butonu ile burada calisiyor.Obje burada sopa.  USTTEKI ILE AYNI
                sticks[stickIndex].SetActive(false);

                stickIndex++;
                stickIndex = Mathf.Min(stickIndex, sticks.Length - 1);

                sticks[stickIndex].SetActive(true);
                stickText.text = items.stickItems[stickIndex].name;

                updateButtons();


                break;
            case 2://button kiyafet butonu ile burada calisiyor.

                skinIndex++;                                                //index azaltiliyor.
                skinIndex = Mathf.Min(skinIndex, skins.Length - 1);           //indexin olmasi gereken degerleri asmasi engelleniyor.
                characterSkin.material = skins[skinIndex];                  //yeni indexteki material tuttugumuz karakter renderer in materialine ataniyor.

                skinText.text = items.skinItems[skinIndex].name;                      //yeni indexteki materialin ismi kiyafet textine yazdiriliyor

                updateButtons();                                            // her buton isleminin sonunda butonlarin durumunu guncellemek icin buton guncelleme fonksiyonu cagiriliyor


                break;
            default:
                break;
        }




    }
    public void leftButton(int key)
    {
        buttonSound[0].Play();
        // buradaki bilgilendirme satirlari disinda gerisi yukaridaki ile ayni


        switch (key)// her bir sol button bir anahtar degiskeni gonderiyor. bu degisken buton listese indexleri ile ayni. [0]=sapka,[1]=sopa,[2]=kiyafet
        {
            case 0://button sapka butonu ile burada calisiyor.
                hats[hatIndex].SetActive(false);

                hatIndex--;                                                 // burada 1 azaltilma var
                hatIndex = Mathf.Max(hatIndex, 0);                          // sinirlar azaltilma oldugu icin 0

                hats[hatIndex].SetActive(true);
                hatText.text = items.hatItems[hatIndex].name;

                updateButtons();

                break;
            case 1://button sopa butonu ile burada calisiyor. 
                sticks[stickIndex].SetActive(false);

                stickIndex--;                                                   // burada 1 azaltilma var
                stickIndex = Mathf.Max(stickIndex, 0);                          // sinirlar azaltilma oldugu icin 0

                sticks[stickIndex].SetActive(true);
                stickText.text = items.stickItems[stickIndex].name;

                updateButtons();


                break;
            case 2://button kiyafet butonu ile burada calisiyor.

                skinIndex--;                                                    // burada 1 azaltilma var
                skinIndex = Mathf.Max(skinIndex, 0);                            // sinirlar azaltilma oldugu icin 0
                characterSkin.material = skins[skinIndex];

                skinText.text = items.skinItems[skinIndex].name;

                updateButtons();

                break;
            default:
                break;
        }

    }

    //------------BUTONLARI GUNCELLEYEN FONKSIYONLAR
    void updateButtons()
    {
        for (int i = 0; i < rightButtons.Length; i++)// once butun buttonlar kullanilabilir hale getiriliyor.
        {
            rightButtons[i].interactable = true;
            leftButtons[i].interactable = true;
        }


        //Daha sonra har bir index degeri ayri ayri kontrol edilip. degere gore gerekli buton kullanilamaz hale getiriliyor.


        if (hatIndex == hats.Length - 1)           //Ornegin burada eger sapka indexi listenin olabilecek son indexine gelmis ise
        {                                          //
            rightButtons[0].interactable = false;  // bir sonraki sapkayi getiren button (rightButtons[0], sifirinci eleman sapka icindi) kullanilmaz hale getiriliyor.
        }                                          // 
        if (stickIndex == sticks.Length - 1)
        {
            rightButtons[1].interactable = false;
        }
        if (skinIndex == skins.Length - 1)
        {
            rightButtons[2].interactable = false;
        }
        if (hatIndex == 0)
        {
            leftButtons[0].interactable = false;
        }
        if (stickIndex == 0)
        {
            leftButtons[1].interactable = false;
        }
        if (skinIndex == 0)
        {
            leftButtons[2].interactable = false;
        }

        if (!items.hatItems[hatIndex].sold)                                                                   // burada buybutton[0] yani sapka satin alma butonunu aktif veya pasif olmasi durumunu kontrolu icin
        {                                                                                                     // indexteki yani gosterilen sapka iteminin items verilerindeki satin alinma durumunu kontrol ediyor.
            activateButton("buy", 0);
            buyButtons[0].GetComponentInChildren<Text>().text = items.hatItems[hatIndex].price.ToString();     // item satilmamis ise buy butonu aktif oluyor, ve fiyat bilgisi gosteriliyor

        }
        else if (!(pf.getI("CurrentHat") == hatIndex))                                                          // pref verisindeki giyilen sapka indexinin gosterilin sapka indexi ile esit olmamasi durumunda
        {
            activateButton("equipt", 0);                                                                        // giyme butonu gozukuyor. burasi buy butonu gosterilmeyecekse gosteriliyor
        }
        else
        {
            activateButton("unequipt", 0);                                                                      //bu bolgede ise hem esya satin alinmis hem de kayitlarda giyilen esya olarak gozukuyorsa 
        }                                                                                                       // esyayi cikartma butonu devreye giriyor.
        if (!items.stickItems[stickIndex].sold)
        {
            activateButton("buy", 1);
            buyButtons[1].GetComponentInChildren<Text>().text = items.stickItems[stickIndex].price.ToString();  // $$$$$$$$  yukarinin aynisi tek fark sopa butonlari guncelleniyor.

        }
        else if (!(pf.getI("CurrentStick") == stickIndex))
        {
            activateButton("equipt", 1);
        }
        else
        {
            activateButton("unequipt", 1);
        }
        if (!items.skinItems[skinIndex].sold)
        {
            activateButton("buy", 2);
            buyButtons[2].GetComponentInChildren<Text>().text = items.skinItems[skinIndex].price.ToString();    //$$$$$$$$$$   yukarinin aynisi tek fark kkyafet butonlari guncelleniyor.


        }
        else if (!(pf.getI("CurrentSkin") == skinIndex))
        {
            activateButton("equipt", 2);
        }
        else
        {
            activateButton("unequipt", 2);
        }

        pointText.text = pf.getI("Point").ToString();           // burada puan bilgisini gosteren text guncelleniyor.
        dt.items = items;                                       // burada Data kutuphanesindeki esya bilgilerine elimizdeki  esya bilgileri yukeniyor
        dt.write();                                             // ve dosyaya yaziliyor.
    }
    void activateButton(string button, int index)
    {
        switch (button)
        {
            case "buy":
                buyButtons[index].gameObject.SetActive(true);
                equiptButtons[index].gameObject.SetActive(false);
                unequiptButtons[index].gameObject.SetActive(false);
                break;
            case "equipt":
                buyButtons[index].gameObject.SetActive(false);
                equiptButtons[index].gameObject.SetActive(true);
                unequiptButtons[index].gameObject.SetActive(false);
                break;
            case "unequipt":
                buyButtons[index].gameObject.SetActive(false);
                equiptButtons[index].gameObject.SetActive(false);
                unequiptButtons[index].gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }


    //bu fonksiyon ile sahip olunan esyalarin indexine gore esyalar aktif ve pasif hale getiriliyor
    void showItemsAndTexts()
    {



        for (int i = 0; i < hats.Length; i++)
        {
            hats[i].SetActive(false); // listedeki herbir obje sirasi geldiginde pasif hale getiriliyor.

            if (i == pf.getI("CurrentHat"))                         // eger index sahip olunan esya indexinde ise iceri giriyor
            {
                hatIndex = i;                                       //index ayarlaniyor
                hats[i].SetActive(true);                            // once esya aktif hale getiriliyor.
                hatText.text = items.hatItems[hatIndex].name;       // sonra esyanin ismi yaziliyor. isim kayitlardan aliniyor. 

            }

        }

        for (int i = 0; i < sticks.Length; i++)// sopalar icin de ayni sey gecerlli
        {
            sticks[i].SetActive(false);

            if (i == pf.getI("CurrentStick"))
            {
                stickIndex = i;
                sticks[i].SetActive(true);
                stickText.text = items.stickItems[stickIndex].name;


            }

        }


        for (int i = 0; i < skins.Length; i++) // kiyafetteki tek fark pasif hale getirme yok
        {


            if (i == pf.getI("CurrentSkin"))
            {
                skinIndex = i;
                characterSkin.material = skins[i];// ve tuttugumuz renderer objesine indexteki material ataniyor.
                skinText.text = items.skinItems[skinIndex].name;

                break;
            }


        }


    }







}



