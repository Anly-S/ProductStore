
namespace ProductStore
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product(string code, string name, double price, int quantity)
        {
            Code = code;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Code},{Name},{Price},{Quantity}";
        }

        public static Product FromString(string productString)
        {
            string[] parts = productString.Split(',');
            return new Product(parts[0], parts[1], double.Parse(parts[2]), int.Parse(parts[3]));
        }
    }
}
