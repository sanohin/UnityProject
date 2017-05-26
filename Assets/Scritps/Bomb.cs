using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bomb : Collectable
{
    protected override void OnRabbitHit(Rabbit rabbit)
    {
        if (!rabbit.IsProtected)
        {
            if (rabbit.Big)
            {
                rabbit.ShrinkSize();
                rabbit.SetProtection();
            }
            else
            {
                rabbit.Die();
            }
            this.CollectedHide();
        }
    }
}
