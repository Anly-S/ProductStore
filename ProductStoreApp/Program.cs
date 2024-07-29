
namespace ProductStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n----- PRODUCT STORE MENU -----");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Buy Product");
                Console.WriteLine("3. View Sales Report");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        store.ViewProducts();
                        break;
                    case "2":
                        store.BuyProduct();
                        break;
                    case "3":
                        store.ViewSalesReport();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
