using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Util;
public class GameController : MonoBehaviour
{

    Pref pf = new Pref();
    [Header("Controllers")]
    public CameraControl cameraControl;
    public CharacterControl characterControl;
    public EnemyController enemyController;

    [Header("Reference Points")]
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject finishLine;

    [Header("UI")]
    public Slider gameSoundBar;
    public Slider fxSoundBar;
    public Slider progressBar;
    public GameObject settingsPanel;
    public GameObject exitPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject loadingPanel;
    public Slider loadingBar;

    [Header("Sound")]
    public AudioSource gameSound;
    public AudioSource buttonSound;
    public AudioSource[] fxSounds;
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
    int levelIndex => SceneManager.GetActiveScene().buildIndex - 4;

    void Start()
    {
        // baslangicta butun sesleri kayitli degerlere cekiyor.

        for (int i = 0; i < dieEffectPool.Count; i++)
        {
            dieEffectPool[i].GetComponent<AudioSource>().volume = pf.getF("FXSound");
        }
        for (int i = 0; i < bornEffectPool.Count; i++)
        {
            bornEffectPool[i].GetComponent<AudioSource>().volume = pf.getF("FXSound");
        }
        for (int i = 0; i < splashEffectPool.Count; i++)
        {
            splashEffectPool[i].GetComponent<AudioSource>().volume = pf.getF("FXSound");
        }
        buttonSound.volume = pf.getF("FXSound");

        gameSound.volume = pf.getF("GameSound");


        //ayarlar ses barlarinin duzeyini kayitli degere cekiyor.

        fxSoundBar.value = pf.getF("FXSound");
        gameSoundBar.value = pf.getF("GameSound");




        // ana menu muzigini tutan yok olmaz objeyi yok ediyor.
        Destroy(GameObject.FindGameObjectWithTag("MenuSound"));

        // karakterin baslangic pozisyonu ile bitis cizgisi arasindaki mesafe olculuyor.

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
        // bitise kalan mesafeye gore ilerleme bari guncelleniyor.
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
        Invoke(nameof(loseDelay), 3f);

    }
    public void loseDelay()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }



    // kazanildiginda calisan fonksiyon. gelistirilecek
    public void win()
    {
        if (levelIndex == pf.getI("LastLevel"))
        {
            pf.setI("LastLevel", levelIndex + 1);
        }
        stopClones();

        Invoke(nameof(winDelay), 3f);

    }
    public void winDelay()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);

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

    //$$$$$$$$$$$$$$$$$$$$ BARLAR

    // oyun ici muzik sesini degistiriyor
    public void gameBar()
    {
        pf.setF("GameSound", gameSoundBar.value);
        gameSound.volume = gameSoundBar.value;
    }

    // butun efektlerin icindeki ses elemanlarinin degerini degistiriyor.
    public void fxBar()
    {
        pf.setF("FXSound", fxSoundBar.value);
        for (int i = 0; i < dieEffectPool.Count; i++)
        {
            dieEffectPool[i].GetComponent<AudioSource>().volume = pf.getF("FXSound");
        }
        for (int i = 0; i < bornEffectPool.Count; i++)
        {
            bornEffectPool[i].GetComponent<AudioSource>().volume = pf.getF("FXSound");
        }
        for (int i = 0; i < splashEffectPool.Count; i++)
        {
            splashEffectPool[i].GetComponent<AudioSource>().volume = pf.getF("FXSound");
        }
        buttonSound.volume = pf.getF("FXSound"); // ayarlar butonunun sesini de guncelliyor

    }

    ////$$$$$$$$$$$$$$$  BUTONLAR
    ///
    public void settingsButton()
    {
        buttonSound.Play();
        Time.timeScale = 0;
        settingsPanel.SetActive(true);
    }
    public void replayButton()
    {
        buttonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
    public void playButton()
    {
        buttonSound.Play();
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
    }
    public void homeButton()
    {
        buttonSound.Play();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;


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

    // Aktif butonlara kendi indexi ile cagirilan fonksiyon.
    public void nextLevelLoader()
    {
        buttonSound.Play();
        Time.timeScale = 1;
        StartCoroutine(loadAsync(levelIndex+5));     // leveller build settings de 5 den basladgi icin bir sonraki level +5 ile donduruluyor.
    }
    public void exitButton()
    {
        buttonSound.Play();
        exitPanel.SetActive(true);
    }
    public void yesButton()
    {
        buttonSound.Play();
        Application.Quit();
    }
    public void noButton()
    {
        buttonSound.Play();
        exitPanel.SetActive(false);
    }

}
