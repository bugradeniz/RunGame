using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;
namespace Util
{

    public class Pref
    {
        // Varsayilan veriler

        Dictionary<string, int> defaultDatasI = new Dictionary<string, int>(){
            {"defaultLastLevel", 1},
            {"defaultCurrentHat", 0},
            {"defaultCurrentStick", 0},
            {"defaultCurrentSkin", 0},
            {"defaultLanguageIndex", 0},
            {"defaultIACount", 0},
            {"defaultPoint", 200}
        };
        Dictionary<string, float> defaultDatasF = new Dictionary<string, float>(){
            {"defaultMenuSound", 0.5f},
            {"defaultFXSound", 0.5f},
            {"defaultGameSound", 0.5f},
        };
        Dictionary<string, string> defaultDatasS = new Dictionary<string, string>(){
            {"defaultLanguage", "EN"},
            {"defaultSomething", "Something"},

        };



        // alinan string anahtarin PP de olup olmadigi kontrol ediliyor.
        public bool hasK(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        // alinan string anahtari ve integer degeri PP de kayit ediyor. zaten varsa uzerine yazilir.
        public void setI(string key, int data)
        {
            PlayerPrefs.SetInt(key, data);
        }
        // alinan string anahtari PP de var ise  degeri donduruyor.bu anahtar yok ise varsayilan degerde yeni key olusturup o  degeri donduruyor. 
        public int getI(string key)
        {
            if (hasK(key)) return PlayerPrefs.GetInt(key);
            else
            {

                setI((key), defaultDatasI[("default" + key)]);

                return PlayerPrefs.GetInt(key);
            }
        }


        // alinan string anahtari ve float degeri PP de kayit ediyor. zaten varsa uzerine yazilir.
        public void setF(string key, float data)
        {
            PlayerPrefs.SetFloat(key, data);
        }
        public float getF(string key)
        {
            if (hasK(key)) return PlayerPrefs.GetFloat(key);
            else
            {

                setF((key), defaultDatasF[("default" + key)]);
                return PlayerPrefs.GetFloat(key);
            }
        }

        // alinan string anahtari ve string degeri PP de kayit ediyor. zaten varsa uzerine yazilir.
        public void setS(string key, string data)
        {
            PlayerPrefs.SetString(key, data);
        }
        // alinan string anahtari PP de var ise  degeri donduruyor.bu anahtar yok ise varsayilan degerde yeni key olusturup o  degeri donduruyor. 
        public string getS(string key)
        {

            if (hasK(key)) return PlayerPrefs.GetString(key);
            else
            {

                setS((key), defaultDatasS[("default" + key)]);
                return PlayerPrefs.GetString(key);
            }
        }


    }

    [Serializable]
    public class HatItem // esya bilgilerini tutan obje
    {
        public int groupId;
        public int itemId;
        public List<string> names;
        public string name
        {
            get
            {
                Pref pf = new Pref();

                return names[pf.getI("LanguageIndex")];
            }
            private set { }
        }
        public int price;
        public bool sold;



    }
    [Serializable]
    public class StickItem
    {
        public int groupId;
        public int itemId;
        public List<string> names;
        public string name
        {
            get
            {
                Pref pf = new Pref();

                return names[pf.getI("LanguageIndex")];
            }
            private set { }
        }
        public int price;
        public bool sold;
    }
    [Serializable]
    public class SkinItem
    {
        public int groupId;
        public int itemId;
        public List<string> names;    // esyalar iclerinde her dil icin farkli bir isim tutuyor. tuttugu isimlerin indexi dil indexi ile ayni olmali !!!
        public string name
        {
            get
            {
                Pref pf = new Pref();

                return names[pf.getI("LanguageIndex")];  // name cekildigi zaman listeden dil indexindeki isim cekiliyor!
            }
            private set { }
        }
        public int price;
        public bool sold;

    }
    [Serializable]
    public class Items
    {
        public List<HatItem> hatItems;
        public List<StickItem> stickItems;
        public List<SkinItem> skinItems;



    }
    public class Data
    {
        // gelistirilecek

        public Items items;// esya objelerinden olusan liste


        // belirli dosyayi okuyup icindeki listeyi yukaridaki listeye atayan fonksiyon
        public void read()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemsData.gd"))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.OpenRead(Application.persistentDataPath + "/ItemsData.gd");
                    items = (Items)bf.Deserialize(file);
                    file.Close();
                }
                catch (System.IO.IOException)
                {

                    throw;
                }
            }



        }
        // yukaridaki listeyi belirli dosyaya yazarak kaydeden fonksiyon
        public void write()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemsData.gd"))
            {

                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemsData.gd");
                    bf.Serialize(file, items);
                    file.Close();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }


        }

        //yukaridaki listeyi yeni olusturdugu dosyaya yazarak kayit eden fonksiyon.
        public void create()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemsData.gd");
                bf.Serialize(file, items);
                file.Close();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }

    public class AdManager
    {
        Pref pf = new Pref();
        private InterstitialAd iA;
        private RewardedAd rA;
        public void requestIA()
        {
            string adId;
#if UNITY_ANDROID
            adId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
adId = "ca-app-pub-3940256099942544/4411468910";
#else
adId = "unexpected-Platform";
#endif
            iA = new InterstitialAd(adId);
            AdRequest req = new AdRequest.Builder().Build();
            iA.LoadAd(req);

            iA.OnAdClosed += iAClosed;
        }
        void iAClosed(object sender, EventArgs args)
        {
            iA.Destroy();
            requestIA();
        }
        public void showCountedIA()
        {
            if (pf.getI("IACount") >= 2)
            {
                if (iA.IsLoaded())
                {
                    iA.Show();
                    pf.setI("IACount", 0);
                }
                else
                {
                    iA.Destroy();
                    requestIA();
                }
            }
            else
            {
                pf.setI("IACount", pf.getI("IACount" ) + 1);
            }

        }
        public void showIA()
        {

            if (iA.IsLoaded())
            {
                iA.Show();

            }
            else
            {
                iA.Destroy();
                requestIA();
            }


        }

        public void requestRA()
        {

            string adId;
#if UNITY_ANDROID
            adId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
adId = "ca-app-pub-3940256099942544/1712485313";
#else
adId = "unexpected-Platform";
#endif
            rA = new RewardedAd(adId);
            AdRequest req = new AdRequest.Builder().Build();
            rA.LoadAd(req);

            rA.OnAdClosed += rAClosed;
            rA.OnUserEarnedReward += rAEarned;
            rA.OnAdLoaded += rALoaded;
        }

        private void rALoaded(object sender, EventArgs e)
        {
            Debug.Log("reklam yuklendi");
        }

        private void rAEarned(object sender, Reward e)
        {
            string type = e.Type;
            double amount = e.Amount;
            Debug.Log("odul alindi: " +type+"---"+amount);
        }

        private void rAClosed(object sender, EventArgs e)
        {
            requestIA();
            Debug.Log("reklam kapatildi");
        }
        public void showRA()
        {

            if (rA.IsLoaded())
            {
                rA.Show();

            }
            else
            {
                rA.Destroy();
                requestRA();
            }


        }
    }

}

