using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _heath;
    [SerializeField] private int _currentHeath;
    [SerializeField] private List<Item> _dropItemList = new List<Item>();
    [SerializeField] private Player _target;
    
    private Transform _lastEnemyTransform;
    private Item _item;
    private bool _isDie = false;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<Enemy> Dying;


    public Player Target => _target;
    public int CurrentHeath { get => _currentHeath; set => _currentHeath = value; }
    public bool IsDie { get => _isDie; set => _isDie = value; }




    public void Init(Player target)
    {
        _target = target;
    }

    private void Start()
    {
        

        _currentHeath = _heath;
        HealthChanged?.Invoke(_currentHeath, _heath);

    }

    public void TakeDamage(int damage)
    {
        _currentHeath -= damage;
        HealthChanged?.Invoke(_currentHeath, _heath);

        if (_currentHeath <= 0)
        {
            Dying?.Invoke(this);
            _lastEnemyTransform = transform; //������� ��� ���� ���
            _item = _dropItemList[UnityEngine.Random.Range(0, _dropItemList.Count)]; //�������� �� ������ ��� �� ���� ������� 
            _isDie = true;
            gameObject.SetActive(false);
            //Destroy(gameObject);
            DropItem();
            
        }
    }

    private void DropItem()
    {
        Item i = Instantiate(_item, _lastEnemyTransform.position, transform.rotation);
        Spawner.DropOnGroundList.Add(i);
    }

    public void LoadData(Save.EnemySaveData data)
    {
        if(data.IsDie == true)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
            return;
        }
        transform.position = new Vector3(data.Position.x, data.Position.y, data.Position.z);

        _currentHeath = data.CurrentHeath;
        HealthChanged?.Invoke(_currentHeath, _heath);



    }
    
    public void LoadDataDrop(Save.DropSaveData data)
    {
        List<Item> enemy = FindAnyObjectByType<Enemy>()._dropItemList;


        Item i = Instantiate(enemy[data.Id - 1], new Vector3(data.Position.x, data.Position.y, data.Position.z), Quaternion.identity);
        
        Spawner.DropOnGroundList.Add(i);



    }





}
