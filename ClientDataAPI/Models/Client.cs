namespace Client.DataAPI
{
    public class Client : IModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public object Clone()
        {
            return new Client() { ID = ID, Name = Name, Adress = Adress };
        }
    }
}
