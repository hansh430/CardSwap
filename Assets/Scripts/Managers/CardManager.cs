using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static CardData;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private CardsSpriteData cardsSpriteDataSO;
    [SerializeField] private List<Card> currentSelectedCards = new List<Card>();
    private void Awake()
    {
        Instance = this;
    }
    public List<Card> SelectedCards => currentSelectedCards;
    private void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "card_data.json");
        string json = File.ReadAllText(path);
        CardDataModal cardData = JsonUtility.FromJson<CardDataModal>(json);

        CardGroup defaultGroup = CardGroupManager.Instance.CreateGroup();
        Debug.Log(cardData.data.ToString());
        int index = 0;
        foreach (string id in cardData.data.deck)
        {
            if (index++ > 9)
                break;
            Card card = Instantiate(cardPrefab, defaultGroup.transform);
            defaultGroup.AddCard(card);
            card.Initialize(id, defaultGroup, cardsSpriteDataSO.GetSprite(id));
            card.SetOnSelectCard(OnCardUpdated);
        }
    }

    public void OnCardUpdated(bool status, Card card)
    {
        if (status)
        {
            currentSelectedCards.Add(card);
        }
        else
        {
            currentSelectedCards.Remove(card);
        }
        NotifySelectedCardUpdates();
    }

    private void NotifySelectedCardUpdates()
    {
        if (currentSelectedCards.Count >= 2)
        {
            CardGroupManager.Instance.NotifyCardUpdate(true);
        }
        else
        {
            CardGroupManager.Instance.NotifyCardUpdate(false);
        }
    }

    public void UpdatedSelectedCard()
    {
        for (int i = 0; i < currentSelectedCards.Count;)
        {
            currentSelectedCards[i].ToggleSelect();
        }
    }
}
