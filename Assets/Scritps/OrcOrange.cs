using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcOrange : Orc, IFireable
{
    public float carrotLaunchRadius = 5.0f;
    public float carrotIntervalSeconds = 2.0f;
    public GameObject prefabCarrot;
    private float lastCarrot = 0;
    public void Fire(Vector2 direction)
    {
        if (Time.time - lastCarrot > carrotIntervalSeconds)
        {
            lastCarrot = Time.time;
            GameObject obj = GameObject.Instantiate(this.prefabCarrot);
            obj.transform.position = this.transform.position;
            Carrot carrot = obj.GetComponent<Carrot>();
            carrot.Launch(direction);
        }
    }
    protected override void UpdateMove()
    {
        Vector3 rabbitPos = Rabbit.lastRabit.transform.position;
        Vector3 pos = this.transform.position;
        if (Mathf.Abs(rabbitPos.x - pos.x) < this.carrotLaunchRadius)
        {
            this.Fire(rabbitPos - pos);
        }
        base.UpdateMove();
    }

}
