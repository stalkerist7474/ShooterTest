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
    public List<Item> DropSaves = new List<Item>();
    public List<ItemInventaryView> InventorySaves = new List<ItemInventaryView>();
    private bool dropSaved = false;
    private bool InventorySaved = false;

    private void Start()
    {
        var directory = Application.persistentDataPath + "saves";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        filePath = directory + "/save.gamesave";  //С:\Users\Remix74\AppData\LocalLow\DefaultCompany\ShooterTestsaves путь до файла у меня на компьютере
        

        EnemySaves = Spawner.EnemiesList;
        DropSaves = Spawner.DropOnGroundList;
        InventorySaves = Inventory.PlayerInventoryForCanvas;
        PlayerSave = FindObjectOfType<Player>();
    }

    

    public void Print()
    {
        foreach (var enemy in DropSaves)
        {
            Debug.Log($"DropSavesCount {DropSaves.Count}=======DropSaves {enemy.Name}");
        }

        foreach (var enemy in InventorySaves)
        {
            Debug.Log($"InventorySavesCount {InventorySaves.Count}========InventorySaves 0 {enemy.Name}");
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();

        save.SaveEnemies(EnemySaves);
        
        save.SavePlayer(PlayerSave);

        if(DropSaves.Count>0)
            save.SaveDrop(DropSaves);
            dropSaved = true;
            Debug.Log($"DropSaves");

        if (InventorySaves.Count > 0)
            save.SaveInventory(InventorySaves);
            InventorySaved = true;
            Debug.Log($"InventorySaves");

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

        //Drop

        if (dropSaved)
        {

            int y = 0;       
            foreach (var item in save.DropData)
            {
                

                DropSaves[y].GetComponent<Enemy>().LoadDataDrop(item);
                y++;
                Debug.Log($"Load1111");
                
            }
        }

        //Inventory
        if (!InventorySaved)
        {

            int q = 0;
            foreach (var obj in save.InventoryData)
            {

                InventorySaves[q].GetComponent<Inventory>().LoadData(obj);
                
                q++;
                Debug.Log($"Load2222");
            }

            


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

    //Дроп

    [Serializable]
    public struct DropSaveData
    {
        public vec3 Position;
        public int Id;
        

        public DropSaveData(vec3 pos, int id)
        {
            Position = pos;
            Id = id;
            
        }
    }

    public List<DropSaveData> DropData = new List<DropSaveData>();
    

    public void SaveDrop(List<Item> items)
    {
        foreach (var go in items)
        {
            //var drop = go.GetComponent<Item>();

            vec3 pos = new vec3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            
            int id = go.Id;

            DropData.Add(new DropSaveData(pos, id));


        }


    }



    [Serializable]
    //инвентарь
    public struct InventorySaveData
    {
        public int Id;
        public int Count;
        

        public InventorySaveData(int id, int count)
        {
            Count = count;
            Id = id;

        }
    }

    public List<InventorySaveData> InventoryData = new List<InventorySaveData>();


    public void SaveInventory(List<ItemInventaryView> invItem)
    {
        foreach (var go in invItem)
        {
            //ar item = go.GetComponent<ItemInventaryView>();

            int id = go.Id;
            int c = go.CountItem;


            InventoryData.Add(new InventorySaveData(id,c));


        }


    }

}


































