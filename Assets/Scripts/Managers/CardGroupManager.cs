using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGroupManager : MonoBehaviour
{
    public static CardGroupManager Instance;
   
    [SerializeField] private Button groupButton;
    [SerializeField] private Transform cardGroupParent;
    [SerializeField] private CardGroup cardGroupPrefab;

    [SerializeField] private List<CardGroup> currentGroups = new List<CardGroup>();

    private CardGroup newGroup;

    private void Awake() => Instance = this;
    private void Start()
    {
        groupButton.onClick.AddListener(OnGroupButtonClicked);
    }
    public void NotifyCardUpdate(bool isEnable)
    {
        groupButton.gameObject.SetActive(isEnable);
    }
    private void OnGroupButtonClicked()
    {
        Debug.Log("group");
        _ = CreateGroup();
        for (int i = 0; i < CardManager.Instance.SelectedCards.Count; i++)
        {
            var currentGroup = CardManager.Instance.SelectedCards[i].GetGroup();
            currentGroup.RemoveCard(CardManager.Instance.SelectedCards[i]);
            newGroup.AddCard(CardManager.Instance.SelectedCards[i]);
        }
        CardManager.Instance.UpdatedSelectedCard();

    }

    public CardGroup CreateGroup()
    {
        newGroup = Instantiate(cardGroupPrefab, cardGroupParent);
        currentGroups.Add(newGroup);
        return newGroup;
    }
}
