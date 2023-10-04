using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    

    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private float _timeAfterLastWaveDone;
    private float _timeNextWaveDelay;
    private int _spawned;
    private int _enemyCount;
    private int _numWaveOnThisLevel;
    private bool _waveComplete;
    private bool _waveAllEnemySpawned;
    public event UnityAction AllEnemySpawned;
    

    public static event UnityAction OnLevelWin;
    

    public event UnityAction<int, int> EnemyCountChanged;


    private void OnEnable()
    {
        AllEnemySpawned += OnAllEnemySpawned;
        
    }


    private void OnDisable()
    {
        AllEnemySpawned -= OnAllEnemySpawned;
        
    }


    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _numWaveOnThisLevel = _waves.Count;
        
        SetWave(_currentWaveNumber);
        _waveComplete = false;
        _waveAllEnemySpawned = false;
    }

    private void Update()
    {
        _timeAfterLastSpawn += Time.deltaTime;
        _timeAfterLastWaveDone += Time.deltaTime;

       

        // Условие если все цели заспавнены и все они убиты
        if (_waveAllEnemySpawned == true)
        {
           
            if (_waveComplete == true)
            {
               
                            
                if (_timeAfterLastWaveDone >= _timeNextWaveDelay)
                {
                    NextWave();
                   
                }
            }

        }
        //проверка есть ли еще волны
        if (_currentWave == null)
            return;

        //проверка для спавна монстра по времени задержки спавна
        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            
            InstantiateEnemy();
            _spawned++;
            _enemyCount++;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }
        


        //проверка все ли монстры заспавнены
        if(_currentWave.Count <= _spawned)
        {
            //и если число волн на уровень больше числа текущей волны 
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();
                

            _currentWave = null;
         
        }


        
    }
    // Спавн врагов
    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    //Установка начальной волны
    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        EnemyCountChanged?.Invoke(0, 1);
        _timeNextWaveDelay = _currentWave.DelayWave;    //присвоение время задержки след волны

    }

    //установка следующей волны
    public void NextWave()
    {
        _spawned = 0;
        _waveAllEnemySpawned = false;
        _waveComplete = false;  
        _currentWaveNumber++;
        SetWave(_currentWaveNumber);
    }

    //враг убит
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _enemyCount--;
       
        if (_enemyCount == 0)
        {
            _timeAfterLastWaveDone = 0;
            _waveComplete = true;
            _numWaveOnThisLevel--;
                if (_numWaveOnThisLevel == 0)
                {
                    Win();
                }

        }
    }

    //все враги заспавнены
    private void OnAllEnemySpawned()
    {
        _waveAllEnemySpawned = true;

    }

    //Когда победил
    public void Win()
    {
        Debug.Log($"WINNER1");
        OnLevelWin?.Invoke();
        
    }



}


[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
    public int DelayWave;
}