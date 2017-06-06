using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    public static LivesController current;
    public Sprite usedLive;
    private int timesDead = 0;
    void Start()
    {
        current = this;
    }
    public void onRabitDeath()
    {
        timesDead += 1;
        var totalLives = transform.childCount;
        for (int i = totalLives - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            var sprite = child.gameObject.GetComponent<UI2DSprite>();
            if (!sprite.sprite2D.Equals(usedLive))
            {
                sprite.sprite2D = usedLive;
                break;
            }
        }

    }
}
