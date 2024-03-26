using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardGame : MonoBehaviour
{
    public static CardGame Instance;
    [SerializeField] public List<CardAsset> initialCards;
    [SerializeField] public GameObject cardPrefab;
    [SerializeField] public int handCapacity;
    [SerializeField] List<CardLayout> layouts = new();
    private readonly Dictionary<CardInstance, CardView> _cardInstances = new();

    private void Update()
    {
        foreach (var value in layouts)
        {
            RecalculateLayout(value.layoutId);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        foreach (CardAsset value in initialCards)
        {
            CreateCard(value, 1);
        }

        StartTurn();
    }

    private void CreateCard(CardAsset cardAsset, int layoutId)
    {
        CardInstance cardInstance = new CardInstance(cardAsset);
        cardInstance.MoveToLayout(layoutId, GetCardsInLayout(layoutId).Count + 1);
        CreateCardView(cardInstance);
    }

    private void CreateCardView(CardInstance cardInstance)
    {
        GameObject cardObject = Instantiate(cardPrefab);
        CardView cardView = cardObject.GetComponent<CardView>();
        cardView.Init(cardInstance);

        _cardInstances.Add(cardInstance, cardView);

        MoveToLayout(cardInstance, cardInstance.LayoutId);
    }

    private void MoveToLayout(CardInstance card, int lid)
    {
        card.MoveToLayout(lid, GetCardsInLayout(lid).Count + 1);
    }

    public List<CardView> GetCardsInLayout(int lid) => _cardInstances.Where(pair => pair.Key.LayoutId == lid).Select(pair => pair.Value).ToList();


    private void StartTurn()
    {
        ShuffleLayout(1);
        
        foreach (var value in layouts.Where(playerLayout => playerLayout.playerLayout))
        {
            for (int i = 0; i < handCapacity; ++i)
            {
                if (GetCardsInLayout(1).OrderBy(view => view.CardInstance.CardPosition).ToList().Count <= 0) break;
                MoveToLayout(GetCardsInLayout(1).OrderBy(view => view.CardInstance.CardPosition).ToList()[0].CardInstance, value.layoutId);
            }
        }
    }

    private void RecalculateLayout(int lid)
    {
        List<CardView> cardsLayout = GetCardsInLayout(lid).OrderBy(view => view.CardInstance.CardPosition).ToList();

        for (int i = 0; i < cardsLayout.Count; ++i)
        {
            cardsLayout[i].CardInstance.CardPosition = i + 1;
        }
    }

    private void ShuffleLayout(int lid)
    {
        List<CardView> cardsInLayout = GetCardsInLayout(lid);
        List<int> positions = cardsInLayout.Select(cardView => cardView.CardInstance.CardPosition).ToList();
        foreach (var value in cardsInLayout)
        {
            var positUnknow = Random.Range(0, positions.Count);
            value.CardInstance.CardPosition = positions[positUnknow];
            positions.RemoveAt(positUnknow);
        }
    }
}