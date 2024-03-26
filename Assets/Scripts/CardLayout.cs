using System.Collections.Generic;
using UnityEngine;

public class CardLayout : MonoBehaviour
{
    [SerializeField] public int layoutId;
    [SerializeField] public Vector2 offset;
    [SerializeField] public bool faceUp;
    [SerializeField] public bool playerLayout;

    void Update()
    {
        foreach (CardView value in CardGame.Instance.GetCardsInLayout(layoutId))
        {
            Vector3 newPosition = offset * value.CardInstance.CardPosition;

            value.transform.position = gameObject.GetComponent<RectTransform>().transform.position + newPosition;

            value.Rotate(faceUp);
            if (layoutId == 3 || layoutId == 2)
            {
                value.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = value.CardInstance.CardPosition;
            }
        }
    }
}