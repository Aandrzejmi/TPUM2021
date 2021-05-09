namespace Server.DataAPI
{
    public class Product : IModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public object Clone()
        {
            return new Product() { ID = ID, Name = Name, Price = Price };
        }
    }
}
