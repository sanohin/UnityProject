using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

    protected override void OnRabbitHit(Rabbit rabbit)
    {

        this.CollectedHide();
    }
}
