using InventorySystem;

namespace GameManagement
{
    public class TradeOffer
    {
        public ItemStack itemToSell;
        public int pricePerUnit;
        public int maxItemsSellCount;
        public int itemsSellCount;
        public int totalGoldValue => pricePerUnit * itemsSellCount;
    }
}