using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsController : MonoBehaviour
{
    public static FruitsController current;
    public int totalFruits = 11;
    UILabel label;
    void Start()
    {
        current = this;
        label = this.GetComponentInChildren<UILabel>();
        label.text = "0/" + totalFruits;
    }
    public void rerender(int fruits = 1)
    {
        label.text = fruits + "/" + totalFruits;
    }
}
