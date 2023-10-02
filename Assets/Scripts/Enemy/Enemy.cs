using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _heath;
    [SerializeField] private List<Item> _dropItemList = new List<Item>();
    [SerializeField] private Player _target;

    private Transform _lastEnemyTransform;
    private Item _item;


    public Player Target => _target;

    public event UnityAction<Enemy> Dying;



    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _heath -= damage;

        if (_heath <= 0)
        {
            Dying?.Invoke(this);
            _lastEnemyTransform = transform; //позиция где умер моб
            _item = _dropItemList[UnityEngine.Random.Range(0, _dropItemList.Count)]; //выбираем из списка что из него выпадет 


            Destroy(gameObject);
            DropItem();
            
        }
    }

    private void DropItem()
    {
        Instantiate(_item, _lastEnemyTransform.position, transform.rotation);
    }

    
}
