namespace Features.Items.Scripts.SaveSystem
{
    public class InventoryItem
    {
        public string id;
        public int quantity;

        public InventoryItem(string id, int quantity)
        {
            this.id = id;
            this.quantity = quantity;
        }

        public override string ToString()
        {
            return $"<{id}: {quantity}>";
        }
    }
}