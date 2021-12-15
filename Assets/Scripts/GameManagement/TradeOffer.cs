using InventorySystem;

namespace GameManagement
{
    public class TradeOffer
    {
        public bool isPlayerOffer;
        public bool isNpcOffer => !isPlayerOffer;
        public ItemStack itemToSell;
        public int pricePerUnit;
        public int maxItemsSellCount;
        public int itemsSellCount;
        public int totalGoldValue => pricePerUnit * itemsSellCount;
    }
}