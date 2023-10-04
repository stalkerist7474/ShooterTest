using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotateUI : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private Transform transformPlayer;
    void Awake()
    {
        transformPlayer = player.transform;
        
    }
    void LateUpdate()
    {
        transform.position = new Vector3(transformPlayer.position.x, transformPlayer.position.y, transform.position.z);
        
        
    }
}
