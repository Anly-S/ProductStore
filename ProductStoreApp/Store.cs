using ProductStore;

public class Store
{
    private List<Product> products = new List<Product>();
    private const string ProductsFilePath = "C:\\Users\\anly.s\\source\\repos\\ProductStoreApp\\ProductStoreApp\\products.csv";
    private const string SalesFilePath = "C:\\Users\\anly.s\\source\\repos\\ProductStoreApp\\ProductStoreApp\\sales.csv";
    private int saleIdCounter;

    public Store()
    {
        LoadProducts();
        InitializeSaleIdCounter();
    }

    private void LoadProducts()
    {
        try
        {
            if (File.Exists(ProductsFilePath))
            {
                string[] lines = File.ReadAllLines(ProductsFilePath);
                foreach (string line in lines)
                {
                    products.Add(Product.FromString(line));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading products: {ex.Message}");
        }
    }

    private void SaveProducts()
    {
        try
        {
            List<string> lines = new List<string>();
            foreach (Product product in products)
            {
                lines.Add(product.ToString());
            }
            File.WriteAllLines(ProductsFilePath, lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving products: {ex.Message}");
        }
    }

    private void InitializeSaleIdCounter()
    {
        saleIdCounter = 1;

        if (File.Exists(SalesFilePath))
        {
            string[] sales = File.ReadAllLines(SalesFilePath);
            if (sales.Length > 1)
            {
                string lastSale = sales.Last();
                string[] fields = lastSale.Split(',');
                if (int.TryParse(fields[0], out int lastSaleId))
                {
                    saleIdCounter = lastSaleId + 1;
                }
            }
        }
    }

    public void ViewProducts()
    {
        Console.WriteLine("Code\tName\tPrice\tQuantity");
        foreach (Product product in products)
        {
            Console.WriteLine($"{product.Code}\t{product.Name}\t{product.Price}\t{product.Quantity}");
        }
    }

    public void BuyProduct()
    {
        Console.Write("Enter product code: ");
        string code = Console.ReadLine()?.Trim();
        Product product = products.Find(p => p.Code == code);

        if (product != null)
        {
            Console.Write("Enter quantity to buy: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
            {
                if (product.Quantity >= quantity)
                {
                    product.Quantity -= quantity;
                    double total = product.Price * quantity;
                    Console.WriteLine($"Purchased {quantity} {product.Name}(s) for rupees {total}");
                    SaveProducts();
                    LogSale(product, quantity, total);
                }
                else
                {
                    Console.WriteLine("Insufficient stock.");
                }
            }
            else
            {
                Console.WriteLine("Invalid quantity.");
            }
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    private void LogSale(Product product, int quantity, double total)
    {
        try
        {
            string saleInfo = $"{saleIdCounter},{DateTime.Now},{product.Code},{product.Name},{quantity},{total}";
            bool fileExists = File.Exists(SalesFilePath);

            if (!fileExists)
            {
                File.AppendAllText(SalesFilePath, "SaleID,Date,Code,Name,Quantity,Total\n");
            }

            File.AppendAllText(SalesFilePath, saleInfo + Environment.NewLine);
            Console.WriteLine("Sale logged successfully.");
            saleIdCounter++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error logging sale: {ex.Message}");
        }
    }

    public void ViewSalesReport()
    {
        try
        {
            if (File.Exists(SalesFilePath))
            {
                string[] sales = File.ReadAllLines(SalesFilePath);
                Console.WriteLine("SaleID\tDate\t\t\tCode\t\tName\t\tQuantity\tTotal");
                foreach (string sale in sales.Skip(1))
                {
                    string[] fields = sale.Split(',');
                    Console.WriteLine($"{fields[0]}\t{fields[1]}\t{fields[2]}\t\t{fields[3]}\t\t{fields[4]}\t\t{fields[5]}");
                }
            }
            else
            {
                Console.WriteLine("No sales report found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading sales report: {ex.Message}");
        }
    }
}

