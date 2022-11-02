using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            {"defaultPoint", 200}
        };
        Dictionary<string, float> defaultDatasF = new Dictionary<string, float>(){
            {"defaultMenuSound", 0.5f},
            {"defaultFXSound", 0.5f},
            {"defaultGameSound", 0.5f},
        };
        Dictionary<string, string> defaultDatasS = new Dictionary<string, string>(){
            {"example4", ""},
            {"example5", ""},
            {"example6", ""}
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
        public string name;
        public int price;
        public bool sold;

        

    }
    [Serializable]
    public class StickItem
    {
        public int groupId;
        public int itemId;
        public string name;
        public int price;
        public bool sold;
    }
    [Serializable]
    public class SkinItem
    {
        public int groupId;
        public int itemId;
        public string name;
        public int price;
        public bool sold;

    }
    [Serializable]
    public class Items
    {
        public List<HatItem>  hatItems;
        public List<StickItem>  stickItems;
        public List<SkinItem>  skinItems;

        

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
}

