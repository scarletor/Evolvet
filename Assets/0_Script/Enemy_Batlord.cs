using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Batlord : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        if (isDemo)
            InvokeRepeating("Wandering2", 1, 5);
    }

  



    public bool isDemo;







}
