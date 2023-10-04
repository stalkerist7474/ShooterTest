using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private void Update()
    {
        transform.position = transform.position;
    }
}

