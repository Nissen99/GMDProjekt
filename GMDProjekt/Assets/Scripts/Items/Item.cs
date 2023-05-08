namespace Items
{
    
    public enum Rarity
    {
        Rare,
        Epic,
        Legendary
    }
    public class Item
    {
        public string NameOfItem { get; set; }
        public int MainStat { get; set; }
        public int Vitality { get; set; }
        public string LegendaryEffect { get; set; }
        public Rarity Rarity;
    }
}