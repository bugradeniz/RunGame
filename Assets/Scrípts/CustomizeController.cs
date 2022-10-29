using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class CustomizeController : MonoBehaviour
{
    Pref pf = new Pref();// kutuphanemizdeki  player prefs kontrolcu sinifina erisildi.
    Data dt =new Data();// kutuphanemizdeki veri kontrolcu sinifina erisildi.

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



    public Button[] rightButtons;//bir sonraki esyayi gosteren butonlar. [0]=sapka,[1]=sopa,[2]=kiyafet
    public Button[] leftButtons;// bir onceki esyayi gosteren butonlar. [0]=sapka,[1]=sopa,[2]=kiyafet

    [Header("Write Data Here")] // burada esya bilgilerini dosya olarak kaydetmek icin objeler bulunuyor. kolayca ekleyip cikartmak icin gelistirilecekk
    
    public List<Item> items;// esya listesi tutan obje.suan butun esyalar ayni listede.


    void Start()
    {
        // burada dosya okuma ve yazma olusturma isllemleri donuyor suan biraz karisik

        /*dt.items = items;

        dt.create();*/

        dt.read();

        items = dt.items;


        //burada muvcut esya veriler player prefden kutuphane yardimi ile cekiliyor.

        hatIndex = pf.getI("CurrentHat");
        stickIndex = pf.getI("CurrentStick");
        skinIndex = pf.getI("CurrentSkin");


        updateButtons();// ilk sahne acildiginda butonlarin kullanilabilirlik durumunu indexlere gore ayarliyor


       showItemsAndTexts();//ilk sahne acildiginda mevcut esyalar giydiriliyor

    }
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
    }
    public void rightButton(int key)
    {
        switch (key)// her bir sag button bir anahtar degiskeni gonderiyor. bu degisken buton listese indexleri ile ayni. [0]=sapka,[1]=sopa,[2]=kiyafet
        {
            case 0: //button sapka butonu ile burada calisiyor.Obje burada sapka

                hats[hatIndex].SetActive(false);                            //mevcut indexteki obje once pasif hale getiriliyor.
                hatIndex++;                                                 // sonra index 1 artiriliyor.
                hatIndex = Mathf.Min(hatIndex, hats.Length-1);              //indexin olmasi gereken degerleri asmasi engelleniyor.
                hats[hatIndex].SetActive(true);                             // yeni indexteki obje aktif hale getiriliyor.
                hatText.text = hats[hatIndex].gameObject.transform.name;    // yeni indexteki objenin ismi text icine yazdiriliyor.

                updateButtons();                                            // her buton isleminin sonunda butonlarin durumunu guncellemek icin buton guncelleme fonksiyonu cagiriliyor

               
                break;
            case 1://button sopa butonu ile burada calisiyor.Obje burada sopa.  USTTEKI ILE AYNI
                sticks[stickIndex].SetActive(false);                             

                stickIndex++;
                stickIndex = Mathf.Min(stickIndex, sticks.Length-1);

                sticks[stickIndex].SetActive(true);
                stickText.text = sticks[stickIndex].gameObject.transform.name;

                updateButtons();

               
                break;
            case 2://button kiyafet butonu ile burada calisiyor.

                skinIndex++;                                                //index azaltiliyor.
                skinIndex = Mathf.Min(skinIndex, skins.Length-1);           //indexin olmasi gereken degerleri asmasi engelleniyor.
                characterSkin.material = skins[skinIndex];                  //yeni indexteki material tuttugumuz karakter renderer in materialine ataniyor.

                skinText.text = skins[skinIndex].name;                      //yeni indexteki materialin ismi kiyafet textine yazdiriliyor

                updateButtons();                                            // her buton isleminin sonunda butonlarin durumunu guncellemek icin buton guncelleme fonksiyonu cagiriliyor


                break;
            default:
                break;
        }




    }
    
    public void leftButton(int key)
    {
        // buradaki bilgilendirme satirlari disinda gerisi yukaridaki ile ayni


        switch (key)// her bir sol button bir anahtar degiskeni gonderiyor. bu degisken buton listese indexleri ile ayni. [0]=sapka,[1]=sopa,[2]=kiyafet
        {
            case 0://button sapka butonu ile burada calisiyor.
                hats[hatIndex].SetActive(false);

                hatIndex--;                                                 // burada 1 azaltilma var
                hatIndex = Mathf.Max(hatIndex, 0);                          // sinirlar azaltilma oldugu icin 0
                                                                            
                hats[hatIndex].SetActive(true);
                hatText.text = hats[hatIndex].gameObject.transform.name;

                updateButtons();

                break;
            case 1://button sopa butonu ile burada calisiyor. 
                sticks[stickIndex].SetActive(false);

                stickIndex--;                                                   // burada 1 azaltilma var
                stickIndex = Mathf.Max(stickIndex, 0);                          // sinirlar azaltilma oldugu icin 0

                sticks[stickIndex].SetActive(true);
                stickText.text = sticks[stickIndex].gameObject.transform.name;

                updateButtons();

                
                break;
            case 2://button kiyafet butonu ile burada calisiyor.

                skinIndex--;                                                    // burada 1 azaltilma var
                skinIndex = Mathf.Max(skinIndex, 0);                            // sinirlar azaltilma oldugu icin 0
                characterSkin.material = skins[skinIndex];

                skinText.text = skins[skinIndex].name;

                updateButtons();

                break;
            default:
                break;
        }

    }



    // bu fonksiyon sadece baslangicta calisiyor.
    void showItemsAndTexts()// sahip olunan esyalarin indexine gore esyalar aktif ve pasif hale getiriliyor
    {


        
        for (int i = 0; i < hats.Length; i++) 
        {
            hats[i].SetActive(false); // listedeki herbir obje sirasi geldiginde pasif hale getiriliyor.

            if (i == pf.getI("CurrentHat")) // eger index sahip olunan esya indexinde ise iceri giriyor
            {
                hats[i].SetActive(true); // once esya aktif hale getiriliyor.
                hatText.text = items[i].name;// sonra esyanin ismi yaziliyor.

            }

        }
        
        for (int i = 0; i < sticks.Length; i++)// sopalar icin de ayni sey gecerlli
        {
            sticks[i].SetActive(false);

            if (i == pf.getI("CurrentStick"))
            {
                sticks[i].SetActive(true);
                stickText.text = sticks[i].name;

            }

        }
        
        
        for (int i = 0; i < skins.Length; i++) // kiyafetteki tek fark pasif hale getirme yok
        {


            if (i == pf.getI("CurrentSkin"))
            {

                characterSkin.material = skins[i];// ve tuttugumuz renderer objesine indexteki material ataniyor.
                skinText.text = skins[i].name;
                break;
            }


        }


    }







}



