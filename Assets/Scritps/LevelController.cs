using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController current;
    Vector3 startingPosition;
    private int fruits = 0;
    private int coins = 0;
    private List<Crystal.Color> crystals = new List<Crystal.Color>();

    void Start()
    {
        current = this;
    }
    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }
    public void onRabbitDeath(Rabbit rabbit)
    {
        rabbit.Reborn();
        rabbit.transform.position = this.startingPosition;
        LivesController.current.onRabitDeath();
    }
    public void addCoins(int n = 1)
    {
        coins += n;
        CoinsController.current.rerender(coins);
    }
    public void addFruits(int n = 1)
    {
        fruits += n;
        FruitsController.current.rerender(fruits);
    }
    public void addCrystal(Crystal.Color color)
    {
        crystals.Add(color);
        CrystalController.current.rerender(crystals);
    }
}
