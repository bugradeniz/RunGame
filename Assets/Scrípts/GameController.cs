using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject startPoint;
    public GameObject endPoint;
    public int currentCharacterNum = 1;

    public List<GameObject> clonePool;
    public List<GameObject> dieEffectPool;
    public List<GameObject> bornEffectPool;
    public List<GameObject> splashEffectPool;
    void Start()
    {

    }

    void Update()
    {
        // tusa basildiginda klonlarin olusturulmasi saglandi.
        // ileride degistirilecek
        

        if (Input.GetMouseButtonDown(1))
        {

            addClone(startPoint.transform);
            
        }
    }


    // puan kapilarindan gectiginde kapidaki isleme ve sayiya gore klon ekleyen veya cikartan fonksiyon eklendi
    public void cloneManager(int value, string operation, Transform position)
    {

        int newCharacterNum; // islem sonrasi yeni klon sayisini tutar 
        int change;        // yeni klon sayisi ile eski sayi arasindaki farki tutar
        switch (operation)
        {
            // carpma islemi   
            case "Multiplication":
                newCharacterNum = currentCharacterNum * value;   // 1. asama islem yapilip yeni sayi bulunuyor
                change = newCharacterNum - currentCharacterNum;   //2. asama yeni sayi ile eskisi arasindaki fark bulunuyor

                if (change > 0)                     //3. asama eger fark sifirdan buyukse fark sayisi kadar klon ekleniyor.
                {
                    for (int i = 0; i < change; i++)
                    {
                        addClone(position);                 
                        Debug.Log("clone eklendi");
                    }
                }
                else if (change < 0)                    // eger fark sifirdan kucukse fark sayisi kadar klon cikartiliyor.
                {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();
                        Debug.Log("clone cikartildi");
                    }
                }
                break;

            /////////////////////////////////////////////////////
            ///
            

                // toplama islemi
            case "Addition":
                newCharacterNum = currentCharacterNum + value; //islem
                change = newCharacterNum - currentCharacterNum;

                if (change > 0)
                {
                    for (int i = 0; i < change; i++)
                    {
                        addClone(position);
                        Debug.Log("clone eklendi");
                    }
                }
                else if (change < 0)
                {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();
                        Debug.Log("clone cikartildi");
                    }
                }

                break;
            /////////////////////////////////////////////////////////
            ///

                // cikarma islemi
            case "Subtraction":
                newCharacterNum = currentCharacterNum - value; // islem
                change = newCharacterNum - currentCharacterNum;

                if (change > 0)
                {
                    for (int i = 0; i < change; i++)
                    {
                        addClone(position);
                        Debug.Log("clone eklendi");
                    }
                }
                else if (change < 0)
                {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();
                        Debug.Log("clone cikartildi");
                    }
                }

                break;
            /////////////////////////////////////////////////////////
            ///

                // bolme islemi
            case "Division":
                newCharacterNum = ((currentCharacterNum - 1) / value) + 1; // yukari yuvarlanmasi icin bolme islemi birazcik degistirildi
                change = newCharacterNum - currentCharacterNum;

                if (change > 0)
                {
                    for (int i = 0; i < change; i++)
                    {
                        addClone(position);
                        Debug.Log("clone eklendi");
                    }
                }
                else if (change < 0)
                {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();
                        Debug.Log("clone cikartildi");
                    }
                }

                break;
            /////////////////////////////////////////////////////////
            ///







            default:
                Debug.Log("hic bir case e girmedi");
                break;
        }
        Debug.Log("karakter sayisi:  " + currentCharacterNum);
    }

    // bir adet klon ekleyen fonksiyon eklendi
    // klone havuzundaki aktif olmayan ilk klonu konumlandirip aktif hale getirilmesi saglandi instantiate komutu performans icin kullanilmayacak
    private void addClone(Transform position)
    {
        foreach (GameObject clone in clonePool)
        {
            if (!clone.activeInHierarchy)
            {

                clone.transform.position = position.transform.position;
                clone.SetActive(true);
                bornEffect(clone.transform);

                // Karakter sayisini tutan degisken artirildi
                currentCharacterNum++;


                break;
            }
        }
    }

    // bir adet klon cikartan fonksiyon eklendi. islem kapilerindaki yok olmalarda kullaniliyor
    // klone havuzundaki aktif olan ilk klonudeaktif hale getirilmesi saglandi destroy komutu performans icin kullanilmayacak
    private void removeClone()
    {
        if (currentCharacterNum > 1) // 1 den daha az olmasi engelleniyor
        {


            foreach (GameObject clone in clonePool)
            {
                if (clone.activeInHierarchy)
                {

                    clone.SetActive(false);

                    dieEffect(clone.transform);


                    // Karakter sayisini tutan degisken azaltildi
                    currentCharacterNum--;


                    break;
                }
            }




        }

    }

    //Spesifik bir klonun herhangi bir sebepten olmesi ile klonu cikartan fonksiyon
    public void removeClone(GameObject clone,string reason)
    {

        clone.SetActive(false);
        currentCharacterNum--; // Karakter sayisini tutan degisken azaltildi
        if (reason=="Hammer")
        {
            hammerEffect(clone.transform);
        }
        else
        {
            dieEffect(clone.transform);
        }
        


    }


    //Belirli bir konumda  olme efekti gerceklestiren fonksiyon
    private void dieEffect(Transform transform)
    {
        foreach (GameObject effect in dieEffectPool)
        {
            if (!effect.activeInHierarchy)
            {


                effect.transform.position = transform.position;
                effect.SetActive(true);
                effect.GetComponent<ParticleSystem>().Play();


                break;
            }
        }
    }
    //Belirli bir konumda  yere yapisma efekti gerceklestiren fonksiyon
    private void hammerEffect(Transform transform)
    {
        foreach (GameObject effect in splashEffectPool)
        {
            if (!effect.activeInHierarchy)
            {


                effect.transform.position = transform.position;
                effect.SetActive(true);


                break;
            }
        }
    }



    //Belirli bir konumda  dogma efekti gerceklestiren fonksiyon
    private void bornEffect(Transform transform)
    {
        foreach (GameObject effect in bornEffectPool)
        {
            if (!effect.activeInHierarchy)
            {


                effect.transform.position = transform.position;
                effect.SetActive(true);
                effect.GetComponent<ParticleSystem>().Play();


                break;
            }
        }
    }
}
