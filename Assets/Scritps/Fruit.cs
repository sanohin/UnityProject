using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{
    protected override void OnRabbitHit(Rabbit rabbit)
    {
        this.CollectedHide();
        LevelController.current.addFruits();
    }
}
