using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public int CardsInHand = 0;
    public float Angle = 90;
    public List<RectTransform> cardsTransforms;

    void Awake()
    {
        CardsInHand = transform.childCount;
        RearrangeCards();
    }

    public void DrawCard()
    {
        CardsInHand++;
        RearrangeCards();
    }

    public void PlayCard()
    {
        CardsInHand--;
        RearrangeCards();
    }

    public void RearrangeCards()
    {
        //TEST LINE
        CardsInHand = transform.childCount;
        if ( CardsInHand > 2)
        {
            float tempHelp = 0;
            if (CardsInHand > 2 && CardsInHand < 6)
                tempHelp = transform.childCount - 1;
            float step = Angle / ((transform.childCount - 1) + tempHelp);
            for (int i = 0; i < transform.childCount; i++)
            {
                float tempAngleHelp = 0;
                if (tempHelp > 0)
                    tempAngleHelp = 2;
                transform.GetChild(i).localPosition = new Vector3(0f, -1350f, 0f);
                transform.GetChild(i).eulerAngles = new Vector3(0, 0, (Angle / (2 + tempAngleHelp)) - step * i);
            }
        }
        else if (CardsInHand == 2)
        {
            transform.GetChild(0).localPosition = new Vector3(-100f, -1350f, 0f);
            transform.GetChild(0).eulerAngles = new Vector3(0f, 0f, 0f);
            transform.GetChild(1).localPosition = new Vector3(100f, -1350f, 0f);
            transform.GetChild(1).eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (CardsInHand == 1)
        {
            transform.GetChild(0).localPosition = new Vector3(0f, -1350f, 0f);
            transform.GetChild(0).eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}
