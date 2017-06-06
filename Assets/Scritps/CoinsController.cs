using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    public static CoinsController current;
    UILabel label;
    void Start()
    {
        current = this;
        label = this.GetComponentInChildren<UILabel>();
    }
    public void rerender(int coins)
    {
        var text = coins.ToString();
        var res = text.Length >= 4 ? text : new string('0', 4 - text.Length) + text;
        label.text = res;
    }
}
