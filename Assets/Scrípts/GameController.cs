using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject startPoint;
    public GameObject endPoint;
    public int currentCharacterNum = 1;

    public List<GameObject> clonePool;
    void Start()
    {

    }

    void Update()
    {
        // tusa basildiginda klonlarin olusturulmasi saglandi.
        // ileride degistirilecek
        // klone havuzundaki aktif olmayan ilk klonu konumlandirip aktif hale getirilmesi saglandi instantiate komutu performans icin kullanilmayacak
        if (Input.GetMouseButtonDown(1))
        {
            foreach (GameObject clone in clonePool)
            {
                if (!clone.activeInHierarchy)
                {
                    
                    clone.transform.position = startPoint.transform.position;
                    clone.SetActive(true);

                    // Karakter sayisini tutan degisken artirildi
                    currentCharacterNum++;


                    break;
                }
            }

            // Instantiate(clonePrefab, startPoint.transform.position, Quaternion.identity);

        }
    }


    // puan kapilarindan gectiginde kapidaki isleme gore klon ekleyen veya cikartan fonksiyon eklendi
    public void cloneManager(string operation,Transform position) {

        int  newCharacterNum; // islem sonrasi yeni klon sayisini tutar 
        int change ;        // yeni klon sayisi ile eski sayi arasindaki farki tutar
        switch (operation)
        {
            case "x2":
                newCharacterNum = currentCharacterNum *2;
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

            /////////////////////////////////////////////////////
            ///

            case "+3":
                newCharacterNum = currentCharacterNum + 3;
                change = newCharacterNum - currentCharacterNum;

                if (change > 0)
                {
                    for (int i = 0; i < change; i++)
                    {
                        addClone(position);
                        Debug.Log("clone eklendi");
                    }
                }
                else if (change < 0) {
                    for (int i = 0; i > change; i--)
                    {
                        removeClone();
                        Debug.Log("clone cikartildi");
                    }
                }
                
                break;
            /////////////////////////////////////////////////////////
            ///

            case "-2":
                newCharacterNum = currentCharacterNum - 2;
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

            case "/3":
                newCharacterNum = currentCharacterNum /3;
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

            case "-10":
                newCharacterNum = currentCharacterNum -10;
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

            case "/5":
                newCharacterNum = ((currentCharacterNum - 1)    / 5    )+1 ; // yukari yuvarlanmasi icin bolme islemi birazcik degistirildi
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
        Debug.Log("karakter sayisi:  "+currentCharacterNum );
    }

    // bir adet klon ekleyen fonksiyon eklendi
    private void addClone(Transform position)
    {
        foreach (GameObject clone in clonePool)
        {
            if (!clone.activeInHierarchy)
            {

                clone.transform.position = position.transform.position;
                clone.SetActive(true);

                // Karakter sayisini tutan degisken artirildi
                currentCharacterNum++;


                break;
            }
        }
    }

    // bir adet klon cikartan fonksiyon eklendi
    private void removeClone()
    {
        if (currentCharacterNum>1) // 1 den daha az olmasi engelleniyor
        {


            foreach (GameObject clone in clonePool)
            {
                if (clone.activeInHierarchy)
                {

                    clone.SetActive(false);

                    // Karakter sayisini tutan degisken azaltildi
                    currentCharacterNum--;


                    break;
                }
            }
        }

    }
}
