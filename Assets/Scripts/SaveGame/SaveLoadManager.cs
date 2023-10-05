using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using System.ComponentModel;


public class SaveLoadManager : MonoBehaviour
{
    
    string filePath;
    Player PlayerSave;
    public List<Enemy> EnemySaves = new List<Enemy>();

    private void Start()
    {
        var directory = Application.persistentDataPath + "saves";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        filePath = directory + "/save.gamesave";  //С:\Users\Remix74\AppData\LocalLow\DefaultCompany\ShooterTestsaves путь до файла у меня на компьютере
        

        EnemySaves = Spawner._enemiesList;
        PlayerSave = FindObjectOfType<Player>();
    }


    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();

        save.SaveEnemies(EnemySaves);
        
        save.SavePlayer(PlayerSave);

        bf.Serialize(fs, save);
        fs.Close();
        Debug.Log($"Save");
        
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);
        fs.Close();


        //Enemy
        int i = 0;
        foreach (var enemy in save.EnemiesData)
        {
            
            EnemySaves[i].GetComponent<Enemy>().LoadData(enemy);
            i++;
        }

        //Player

        foreach (var player in save.PlayerData)
        {
            PlayerSave.GetComponent<Player>().LoadData(player);
        }
          
        Debug.Log($"Load");
    }

    
}

[Serializable]
public class Save
{

    
    [Serializable]
    public struct vec3
    {
        public float x, y, z;

        public vec3(float x,  float y, float z)
        {
            this.x = x; 
            this.y = y; 
            this.z = z;
        }
    }

    //Монстры

    [Serializable]
    public struct EnemySaveData
    {
        public vec3 Position;
        public int CurrentHeath;
        public bool IsDie;

        public EnemySaveData(vec3 pos, int heath, bool isDie)
        {
            Position = pos;
            CurrentHeath = heath;
            IsDie = isDie;
            
            
            
        }
    }

    public List<EnemySaveData> EnemiesData = new List<EnemySaveData>();
    //public int CountEnemySaveData = new int();

    public void SaveEnemies(List<Enemy> enemies)
    {
        foreach (var go in enemies)
        {
            var enemy = go.GetComponent<Enemy>();
            



            vec3 pos = new vec3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            int hp = enemy.CurrentHeath;
            bool isdie = enemy.IsDie;

            Debug.Log($"hp{hp} enemy.CurrentHeath{enemy.CurrentHeath} isdie{isdie}");

            EnemiesData.Add(new EnemySaveData(pos, hp, isdie));
            
            
        }

        
    }



    //Игрок
    [Serializable]
    public struct PlayerSaveData
    {
        public vec3 Position;
        public int CurrentHeath;


        public PlayerSaveData(vec3 pos, int heath)
        {
            Position = pos;
            CurrentHeath = heath;

        }

    }

    public List<PlayerSaveData> PlayerData = new List<PlayerSaveData>();



    public void SavePlayer(Player player)
    {


        var go = player.GetComponent<Player>();

        vec3 pos = new vec3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            
        int hp = player.CurrentHeath;

        PlayerData.Add(new PlayerSaveData(pos, hp));

        


    }



}

    //Инвентарь


































