using UnityEngine;

[CreateAssetMenu(fileName = "New Card Asset", menuName = "Card Game/Card Asset")]
public class CardAsset : ScriptableObject
{
    public CardRank cardRank;
    public CardSuit cardSuit;
    public Sprite cardImage;
}