namespace App.FakeEntity.Orders
{
    public class ShoppingCartItemViewModel
    {
        public int Id
        {
            get; set;
        }
        public int StoreId
        {
            get; set;
        }
        public int ParentItemId
        {
            get; set;
        }
        public int BundleItemId
        {
            get; set;
        }
        public int ShoppingCartTypeId
        {
            get; set;
        }
        public int CustomerId
        {
            get; set;
        }
        public int PostId
        {
            get; set;
        }
        public string AttributesXml
        {
            get; set;
        }
        public decimal CustomerEnteredPrice
        {
            get; set;
        }
        public int Quantity
        {
            get; set;
        }
    }
}
