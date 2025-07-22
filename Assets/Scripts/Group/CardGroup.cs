using System.Collections.Generic;
using UnityEngine;

public class CardGroup : MonoBehaviour, IDropable
{
    [SerializeField] private List<Card> cardList = new();

    public void AddCard(Card card)
    {
        card.transform.SetParent(transform);
        card.SetGroup(this);
        cardList.Add(card);
    }

    public void RemoveCard(Card card)
    {
        cardList.Remove(card);
        card.SetGroup(null);
        CancelInvoke(nameof(CheckForEmpty));
        Invoke(nameof(CheckForEmpty), 0.01f);
    }

    public void CheckForEmpty()
    {
        if (cardList.Count == 0)
            Destroy(gameObject);
    }

    public void OnDrop(Card card)
    {
        if (!cardList.Contains(card))
            AddCard(card);
    }
}
