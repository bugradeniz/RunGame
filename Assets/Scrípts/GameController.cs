using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [Header("Controllers")]
    public CameraControl cameraControl;
    public CharacterControl characterControl;
    public EnemyController enemyController;

    [Header("Reference Points")]
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject finishLine;

    [Header("UI")]
    public Slider progressBar;


    [Header("Pools")]
    public List<GameObject> clonePool;
    public List<GameObject> enemyPool;
    public List<GameObject> dieEffectPool;
    public List<GameObject> bornEffectPool;
    public List<GameObject> splashEffectPool;
    [Header("Level Data")]
    public int currentCharacterNum = 1;
    public int enemyNum;
    float maxDistance;
    public bool isEnding;

    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("MenuSound"));
        maxDistance = finishLine.transform.position.z - characterControl.transform.position.z;
        // oyun sonundaki dusmanlar otomatik olusturuluyor.
        createEnemies();
    }

    // Belirlenen deger kadar dusman olusturan fonksiyon.
    public void createEnemies()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            enemyPool[i].SetActive(true);
        }
    }

    void Update()
    {
        progressBar.value = 1 - (getCurrentDistance() / maxDistance);


        // son bolumune gelindiginde icine girilen if
        if (isEnding)
        {
            // dusman biterse kazanilip son gorevi deaktif.
            if (enemyNum == 0)
            {
                win();
                isEnding = false;
            }// dusman bitmezse ve karakter sayimiz 1 e duserse kaybedilip son gorevi deaktif ediliyor.
            else if (currentCharacterNum == 1)
            {
                lose();
                isEnding = false;
            }


        }
        else
        {


            // tusa basildiginda klonlarin olusturulmasi saglandi.
            // ileride kaldirilacak
            if (Input.GetMouseButtonDown(1))
            {

                addClone(startPoint.transform);
            }


        }


    }

    public float getCurrentDistance()// Bitis cizgisine olan anlik uzaklik donduren fonksiyon.
    {

        return finishLine.transform.position.z - characterControl.transform.position.z;
    }


    // kaybedildiginde calisan fonksiyon. gelistirilecek
    public void lose()
    {
        stopEnemies();
        Debug.Log("You Lose");
    }

    // kazanildiginda calisan fonksiyon. gelistirilecek
    public void win()
    {
        stopClones();
        Debug.Log("You Win");

    }


    // oyun bitiminde devreye giren fonksiyon
    // oyun sonu gorevini aktif ediyor, kameranin oyun son gorevini aktif ediyor. ana karakterin oyun sonu gorevini aktif ediyor. 
    // dusmanlari aktif ediyor.
    public void ending()
    {
        isEnding = true;
        cameraControl.ending();
        characterControl.ending();
        activateEnemies();
        Vector3 temp = characterControl.transform.GetChild(3).transform.position;
        characterControl.transform.GetChild(3).transform.position = new Vector3(temp.x, temp.y, temp.z + 3);
    }

    // klonlari durduran fonksiyon.
    private void stopClones()
    {
        foreach (GameObject clone in clonePool)
        {
            if (clone.activeInHierarchy)
            {




                if (clone.GetComponent<CloneControl>() != null)
                {
                    clone.GetComponent<CloneControl>().stopRuning();

                }
                else if (clone.GetComponent<FreeCloneControl>() != null)
                {
                    clone.GetComponent<FreeCloneControl>().stopRuning();
                }


            }
        }
    }
    // dusmanlari durduran fonksiyon.
    private void stopEnemies()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (enemy.activeInHierarchy)
            {

                enemy.GetComponent<EnemyController>().stopRuning();



            }
        }
    }

    // serbest klon tarafindan cagirildigi zaman kendisini klon havuzuna ekliyip karakter sayisini arttiran fonksiyon.
    public void addFreeClone(GameObject freeClone)
    {

        clonePool.Add(freeClone);
        currentCharacterNum++;
        bornEffect(freeClone.transform);
    }

    // dusmanlari aktif hale getiren kosmayi baslatan fonksiyon
    private void activateEnemies()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (enemy.activeInHierarchy)
            {

                enemy.GetComponent<EnemyController>().startRuning();



            }
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

                    }
                }
                else if (change < 0)                    // eger fark sifirdan kucukse fark sayisi kadar klon cikartiliyor.
                {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();

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

                    }
                }
                else if (change < 0)
                {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();

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

                    }
                }
                else if (change < 0)
                {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();

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

                    }
                }
                else if (change < 0)
                {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();

                    }
                }

                break;
            /////////////////////////////////////////////////////////
            ///



            default:

                break;
        }

    }

    // bir adet klon ekleyen fonksiyon eklendi
    // klone havuzundaki aktif olmayan ilk klonu konumlandirip aktif hale getirilmesi saglandi instantiate komutu performans icin kullanilmayacak
    public void addClone(Transform position)
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

    //Spesifik bir klonun belirli bir sebepten olmesi ile klonu cikartan ve o sebebe gore effekt olusturan fonksiyon
    public void removeClone(GameObject clone, string reason)
    {

        clone.SetActive(false);
        currentCharacterNum--; // Karakter sayisini tutan degisken azaltildi

        if (reason == "Hammer")
        {

            hammerEffect(clone.transform);
        }
        else if (reason == "Enemy")
        {
            dieEffect(clone.transform);

            enemyNum--;
        }
        else
        {

            dieEffect(clone.transform);
        }



    }

    //dusmanin kendini yok etmesi icin cagirdigi fonksiyon 
    public void removeEnemy(GameObject clone)
    {

        clone.SetActive(false);
        dieEffect(clone.transform);




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
