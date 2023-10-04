using System.Collections;
using System.Collections.Generic;
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

    public event UnityAction<int, int> HealthChanged;

    public Player Target => _target;

    public event UnityAction<Enemy> Dying;



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


            Destroy(gameObject);
            DropItem();
            
        }
    }

    private void DropItem()
    {
        Instantiate(_item, _lastEnemyTransform.position, transform.rotation);
    }

    
}
