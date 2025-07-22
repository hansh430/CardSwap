using System.Collections.Generic;

public class CardData
{
    [System.Serializable]
    public class DeckData
    {
        public List<string> deck;
    }
    [System.Serializable]
    public class CardDataModal
    {
        public DeckData data;
    }
}
