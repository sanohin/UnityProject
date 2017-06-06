using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcGreen : Orc
{
    private void UpdateAttack()
    {
        Vector3 rabbitPosition = Rabbit.lastRabit.transform.position;
        if (rabbitPosition.x > Mathf.Min(pointA.x, pointB.x)
            && rabbitPosition.x < Mathf.Max(pointA.x, pointB.x))
        {
            mode = Mode.Attack;
        }
    }
    protected override void UpdateMove()
    {
        UpdateAttack();
        base.UpdateMove();
    }
}