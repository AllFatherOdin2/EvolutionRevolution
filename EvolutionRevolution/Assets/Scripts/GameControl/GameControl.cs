using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class GameControl : MonoBehaviour {

    public static GameControl control;

    public float health;
    public float maxHealth;
    public float exp;






    public float healthOverTime;


	void Awake () {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }

    }

    
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.dat");

        PlayerData data = new PlayerData();
        data.health = health;
        data.exp = exp;
        data.maxHealth = maxHealth;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            health = data.health;
            exp = data.exp;
        }
    }

    public void SetMaxHealthUp(float healthChange)
    {
        maxHealth = maxHealth + healthChange;
        health = maxHealth;
    }

    public void SetMaxHealthDown(float healthChange)
    {
        maxHealth = maxHealth - healthChange;
        health = maxHealth;
    }

    public IEnumerator HealOverTime(float healAmt, float healTime, float healTick)
    //healAmt: total amount to heal player; healTime: total time to heal player over (sec); healTick frequency to heal for each amount (sec)
    {
        for (float f = 0.0f; f < healTime; f += healTick)
        {
            if (health <= maxHealth)
            {
                float healOnTick = healTick / healTime;
                healOnTick *= healAmt;
                health += healOnTick;
                yield return new WaitForSeconds(healTick);
            }
        }
    }
}

[Serializable]
class PlayerData
{
    public float health;
    public float maxHealth;
    public float exp;
    //should do gets and sets and constructors
}