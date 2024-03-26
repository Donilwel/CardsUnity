public class CardInstance
{
    public readonly CardAsset CardAsset;
    public int LayoutId;
    public int CardPosition;

    public CardInstance(CardAsset c)
    {
        CardAsset = c;
    }

    public void MoveToLayout(int lid, int cards)
    {
        CardPosition = cards;
        LayoutId = lid;
    }
}