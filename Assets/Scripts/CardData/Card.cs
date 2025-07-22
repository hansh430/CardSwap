using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string CardId { get; private set; }
    public bool IsSelected { get; private set; }

    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject highlightImageGO;
    private CardGroup currentGroup;
    private Button button;
    private Action<bool, Card> onSelectCard;
    public void Initialize(string id, CardGroup group, Sprite sprite)
    {
        CardId = id;
        currentGroup = group;
        cardImage.sprite = sprite;
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleSelect);
        Deselect();
    }
    public void SetOnSelectCard(Action<bool, Card> onSelectCard)
    {
        this.onSelectCard = onSelectCard;
    }
    public void Deselect()
    {
        IsSelected = false;
        highlightImageGO.SetActive(false);
    }
    public void ToggleSelect()
    {
        IsSelected = !IsSelected;
        highlightImageGO.SetActive(IsSelected);
        onSelectCard?.Invoke(IsSelected, this);
    }
    public CardGroup GetGroup() => currentGroup;
    public void SetGroup(CardGroup group) => currentGroup = group;
}
