using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _heath;
    [SerializeField] private int _rewardGold;
    [SerializeField] private int _rewardExp;

    [SerializeField] private Player _target;

    public int RewardGold => _rewardGold;
    public int RewardExp => _rewardExp;
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
            Destroy(gameObject);
            //GetListEnemys().Remove(null);
        }
    }


    
}
