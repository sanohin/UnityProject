using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{
    public enum Color
    {
        Blue,
        Green,
        Red
    }
    public Color color;
    protected override void OnRabbitHit(Rabbit rabbit)
    {
        this.CollectedHide();
        LevelController.current.addCrystal(this.color);
    }
}
