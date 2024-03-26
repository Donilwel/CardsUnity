using UnityEngine;

public class CardView : MonoBehaviour
{
    public CardInstance CardInstance;

    private static short cardsCenter, cardsDiscard;

    private void PlayCard()
    {
        CardInstance.MoveToLayout(3, ++cardsCenter);
    }

    private void DiscardCard()
    {
        --cardsCenter;
        CardInstance.MoveToLayout(2, ++cardsDiscard);
    }

    public void Rotate(bool val)
    {
        transform.GetChild(0).gameObject.SetActive(val == true);
        transform.GetChild(1).gameObject.SetActive(val == false);
    }

    public void Init(CardInstance val)
    {
        CardInstance = val;
        UpdateCardSprite();
    }

    private void OnMouseDown()
    {
        if (CardInstance.LayoutId == 3)
        {
            DiscardCard();
        }
        else
        {
            PlayCard();
        }
    }

    private void UpdateCardSprite()
    {
        if (transform.GetChild(0).transform.GetChild(0))
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CardInstance.CardAsset.cardImage;
        }
    }
  
}