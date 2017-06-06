using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public static CrystalController current;
    public Sprite crystalBlueSprite;
    public Sprite crystalGreenSprite;
    public Sprite crystalRedSprite;
    void Start()
    {
        current = this;
    }

    public void rerender(List<Crystal.Color> crystalColors)
    {
        for (int i = 0; i < crystalColors.Count; i++)
        {
            Debug.Log(i);
            Transform child = transform.GetChild(i);
            UI2DSprite sprite = child.gameObject.GetComponent<UI2DSprite>();
            switch (crystalColors[i])
            {
                case Crystal.Color.Blue:
                    sprite.sprite2D = crystalBlueSprite;
                    break;
                case Crystal.Color.Green:
                    sprite.sprite2D = crystalGreenSprite;
                    break;
                case Crystal.Color.Red:
                    sprite.sprite2D = crystalRedSprite;
                    break;
            }
        }
    }

}
