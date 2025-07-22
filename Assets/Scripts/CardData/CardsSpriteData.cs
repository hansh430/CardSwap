using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSprites", menuName = "Card/SpriteData")]
public class CardsSpriteData : ScriptableObject
{
    [System.Serializable]
    public class CardSpriteData
    {
        public string cardId;
        public Sprite sprite;
    }

    public List<CardSpriteData> cardSprites;

    private Dictionary<string, Sprite> cardsDict;

    public Sprite GetSprite(string id)
    {
        if (cardsDict == null)
        {
            cardsDict = new Dictionary<string, Sprite>();
            foreach (var entry in cardSprites)
                cardsDict[entry.cardId] = entry.sprite;
        }

        return cardsDict.TryGetValue(id, out var sprite) ? sprite : null;
    }
}
