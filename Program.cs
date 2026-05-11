namespace shopNest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create Store
            Store store = new Store("ShopNest");

            // Create Admin
            Admin admin = new Admin(
                "Sara Ahmed",
                "admin@shopnest.com",
                "Super Admin"
            );

            // Demonstrate sealed override method
            admin.DisplayInfo();

            int choice;

            do
            {
                Console.WriteLine("========== ShopNest E-Commerce System ==========");
                Console.WriteLine("1. Add Physical Product");
                Console.WriteLine("2. Add Digital Product");
                Console.WriteLine("3. Display All Products");
                Console.WriteLine("4. Register Customer");
                Console.WriteLine("5. Place Order");
                Console.WriteLine("6. Cancel Order");
                Console.WriteLine("7. View Customer Order History");
                Console.WriteLine("8. Show Store Statistics");
                Console.WriteLine("0. Exit");

                Console.Write("\nEnter your choice: ");

                choice = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        {
                            Console.Write("Enter product name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter price: ");
                            double price =
                                Convert.ToDouble(Console.ReadLine());

                            Console.Write("Enter weight (kg): ");
                            double weight =
                                Convert.ToDouble(Console.ReadLine());

                            Console.Write("Enter shipping cost per kg: ");
                            double shipping =
                                Convert.ToDouble(Console.ReadLine());

                            store.AddPhysicalProduct(
                                name,
                                price,
                                weight,
                                shipping
                            );

                            break;
                        }

                    case 2:
                        {
                            Console.Write("Enter product name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter price: ");
                            double price =
                                Convert.ToDouble(Console.ReadLine());

                            Console.Write("Enter file size (MB): ");
                            double fileSize =
                                Convert.ToDouble(Console.ReadLine());

                            Console.Write("Enter download link: ");
                            string link = Console.ReadLine();

                            store.AddDigitalProduct(
                                name,
                                price,
                                fileSize,
                                link
                            );

                            break;
                        }

                    case 3:
                        {
                            store.DisplayAllProducts();
                            break;
                        }

                    case 4:
                        {
                            Console.Write("Enter full name: ");
                            string fullName = Console.ReadLine();

                            Console.Write("Enter email: ");
                            string email = Console.ReadLine();

                            store.RegisterCustomer(
                                fullName,
                                email
                            );

                            break;
                        }

                    case 5:
                        {
                            Console.Write("Enter customer email: ");
                            string email = Console.ReadLine();

                            Console.Write("Enter product ID: ");
                            int productID =
                                Convert.ToInt32(Console.ReadLine());

                            store.PlaceOrder(
                                email,
                                productID
                            );

                            break;
                        }

                    case 6:
                        {
                            Console.Write("Enter order ID: ");
                            int orderID =
                                Convert.ToInt32(Console.ReadLine());

                            store.CancelOrder(orderID);

                            break;
                        }

                    case 7:
                        {
                            Console.Write("Enter customer email: ");
                            string email = Console.ReadLine();

                            store.DisplayCustomerOrders(email);

                            break;
                        }

                    case 8:
                        {
                            store.DisplayStatistics();

                            break;
                        }

                    case 0:
                        {
                            Console.WriteLine(
                                "Thank you for using ShopNest!"
                            );

                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid choice. Please try again.");

                            break;
                        }
                }

                Console.WriteLine();

            } while (choice != 0);
        }
    
    }


    
    abstract class Product
    {
        // Static Fields
        private static int nextProductID = 100;
        private static int totalProductsCreated = 0;

        // Protected Fields
        protected string name;
        protected double price;

        // Properties
        public int ProductID { get; }

        public string Name
        {
            get { return name; }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value > 0)
                {
                    price = value;
                }
                else
                {
                    Console.WriteLine("Error: Price must be greater than zero.");
                }
            }
        }

        // Constructor
        public Product(string name, double price)
        {
            this.name = name;

            // Use property for validation
            Price = price;

            // Assign Product ID
            ProductID = nextProductID++;

            // Increment total products counter
            totalProductsCreated++;
        }

        // Static Method
        public static int GetTotalProductsCreated()
        {
            return totalProductsCreated;
        }

        // Abstract Method
        public abstract void DisplayInfo();

        // Virtual Method
        public virtual double CalculateTotalCost()
        {
            return price;
        }
    }

    class PhysicalProduct : Product
    {
        // Private Fields
        private double weightKg;
        private double shippingCostPerKg;

        // Property
        public double WeightKg
        {
            get { return weightKg; }
        }

        // Constructor
        public PhysicalProduct(string name, double price,double weightKg,double shippingCostPerKg): base(name, price)
        {
            this.weightKg = weightKg;
            this.shippingCostPerKg = shippingCostPerKg;
        }

        // Override DisplayInfo
        public override void DisplayInfo()
        {
            Console.WriteLine("===== [Physical Product] =====");
            Console.WriteLine($"Product ID: {ProductID}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"Weight (Kg): {weightKg}");
            Console.WriteLine($"Shipping Cost Per Kg: {shippingCostPerKg}");
            Console.WriteLine($"Total Cost: {CalculateTotalCost()}");
            Console.WriteLine();
        }

        // Override CalculateTotalCost
        public override double CalculateTotalCost()
        {
            return Price + (weightKg * shippingCostPerKg);
        }
    }

    class DigitalProduct : Product
    {
        // Private Fields
        private double fileSizeMB;
        private string downloadLink;

        // Constructor
        public DigitalProduct(string name,double price,double fileSizeMB,string downloadLink): base(name, price)
        {
            this.fileSizeMB = fileSizeMB;
            this.downloadLink = downloadLink;
        }

        // Override DisplayInfo
        public override void DisplayInfo()
        {
            Console.WriteLine("===== [Digital Product] =====");
            Console.WriteLine($"Product ID: {ProductID}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"File Size (MB): {fileSizeMB}");
            Console.WriteLine($"Download Link: {downloadLink}");
            Console.WriteLine($"Total Cost: {CalculateTotalCost()}");
            Console.WriteLine();
        }

      
    }

    abstract class User
    {
        // Static Field
        private static int totalUsersCreated = 0;

        // Protected Fields
        protected string fullName;
        protected string email;

        // Properties
        public string FullName
        {
            get { return fullName; }
        }

        public string Email
        {
            get { return email; }
        }

        // Constructor
        public User(string fullName, string email)
        {
            this.fullName = fullName;
            this.email = email;

            // Increment total users counter
            totalUsersCreated++;
        }

        // Static Method
        public static int GetTotalUsersCreated()
        {
            return totalUsersCreated;
        }

        // Abstract Method
        public abstract void DisplayInfo();
    }

    class Customer : User
    {
        // Private Field
        private List<Order> orders;

        // Constructor
        public Customer(string fullName, string email): base(fullName, email)
        {
            orders = new List<Order>();
        }

        // Override DisplayInfo
        public override void DisplayInfo()
        {
            Console.WriteLine("===== [Customer] =====");
            Console.WriteLine($"Full Name: {FullName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Orders Count: {orders.Count}");
            Console.WriteLine();
        }

        // Add Order
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        // Remove Order by ID
        public void RemoveOrder(int orderID)
        {
            orders.RemoveAll(order => order.OrderID == orderID);
        }

        // Display Order History
        public void DisplayOrderHistory()
        {
            Console.WriteLine($"===== Order History for {FullName} =====");

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders yet.");
            }
            else
            {
                foreach (Order order in orders)
                {
                    order.DisplayInfo();
                }
            }

            Console.WriteLine();
        }
    }


    class Admin : User
    {
        // Private Field
        private string role;

        // Constructor
        public Admin(string fullName, string email, string role): base(fullName, email)
        {
            this.role = role;
        }

        // Sealed Override Method
        public sealed override void DisplayInfo()
        {
            Console.WriteLine(
                $"[Admin] {fullName} | Email: {email} | Role: {role}"
            );
        }
    }


    class Order
    {
        // Static Field
        private static int nextOrderID = 5000;

        // Private Fields
        private int orderID;
        private Customer customer;
        private Product product;
        private double totalCost;

        // Properties
        public int OrderID
        {
            get { return orderID; }
        }

        public Customer Customer
        {
            get { return customer; }
        }

        public double TotalCost
        {
            get { return totalCost; }
        }

        // Constructor
        public Order(Customer customer, Product product)
        {
            // Auto-generate Order ID
            orderID = nextOrderID++;

            // Store references
            this.customer = customer;
            this.product = product;

            // Calculate total cost from product
            totalCost = product.CalculateTotalCost();
        }

        // Method
        public void DisplayInfo()
        {
            Console.WriteLine("===== [Order] =====");
            Console.WriteLine($"Order ID: {orderID}");
            Console.WriteLine($"Customer: {customer.FullName}");
            Console.WriteLine($"Product: {product.Name}");
            Console.WriteLine($"Total Paid: {totalCost}");
            Console.WriteLine();
        }
    }


    class Store
    {
        // Auto-Implemented Property
        public string StoreName { get; private set; }

        // Private Collections
        private List<Product> products;
        private List<Customer> customers;
        private List<Order> orders;

        // Constructor
        public Store(string name)
        {
            StoreName = name;

            products = new List<Product>();
            customers = new List<Customer>();
            orders = new List<Order>();
        }

        // =========================
        // Product Methods
        // =========================

        public void AddPhysicalProduct(string name,
                                       double price,
                                       double weight,
                                       double shippingPerKg)
        {
            PhysicalProduct product =
                new PhysicalProduct(name, price, weight, shippingPerKg);

            products.Add(product);

            Console.WriteLine(
                $"Physical product added successfully. ID: {product.ProductID}"
            );
        }

        public void AddDigitalProduct(string name,
                                      double price,
                                      double fileSizeMB,
                                      string link)
        {
            DigitalProduct product =
                new DigitalProduct(name, price, fileSizeMB, link);

            products.Add(product);

            Console.WriteLine(
                $"Digital product added successfully. ID: {product.ProductID}"
            );
        }

        public void DisplayAllProducts()
        {
            Console.WriteLine("===== All Products =====");

            if (products.Count == 0)
            {
                Console.WriteLine("No products available.");
            }
            else
            {
                foreach (Product product in products)
                {
                    // Polymorphism
                    product.DisplayInfo();
                }
            }

            Console.WriteLine();
        }

        // =========================
        // Customer Methods
        // =========================

        public void RegisterCustomer(string fullName, string email)
        {
            Customer existingCustomer =
                customers.Find(c => c.Email == email);

            if (existingCustomer != null)
            {
                Console.WriteLine("Error: Email already registered.");
                return;
            }

            Customer customer = new Customer(fullName, email);

            customers.Add(customer);

            Console.WriteLine(
                $"Customer registered successfully: {customer.FullName}"
            );
        }

        public Customer FindCustomer(string email)
        {
            return customers.Find(c => c.Email == email);
        }

        // =========================
        // Order Methods
        // =========================

        public void PlaceOrder(string email, int productID)
        {
            Customer customer = FindCustomer(email);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Product product =
                products.Find(p => p.ProductID == productID);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Order order = new Order(customer, product);

            orders.Add(order);

            customer.AddOrder(order);

            Console.WriteLine(
                $"Order placed successfully. Order ID: {order.OrderID}"
            );

            Console.WriteLine(
                $"Total Cost: {order.TotalCost}"
            );
        }

        public void CancelOrder(int orderID)
        {
            Order order =
                orders.Find(o => o.OrderID == orderID);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            // Remove from customer's history
            order.Customer.RemoveOrder(orderID);

            // Remove from store orders
            orders.RemoveAll(o => o.OrderID == orderID);

            Console.WriteLine(
                $"Order {orderID} cancelled successfully."
            );
        }

        public void DisplayCustomerOrders(string email)
        {
            Customer customer = FindCustomer(email);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            customer.DisplayInfo();
            customer.DisplayOrderHistory();
        }

        // =========================
        // Statistics / Report
        // =========================

        public void DisplayStatistics()
        {
            int physicalCount = 0;
            int digitalCount = 0;

            double totalRevenue = 0;

            // Count product types
            foreach (Product p in products)
            {
                if (p is PhysicalProduct)
                {
                    physicalCount++;
                }
                else if (p is DigitalProduct)
                {
                    digitalCount++;
                }
            }

            // Calculate revenue
            foreach (Order order in orders)
            {
                totalRevenue += order.TotalCost;
            }

            Console.WriteLine("===== Store Statistics =====");
            Console.WriteLine($"Store Name: {StoreName}");
            Console.WriteLine($"Total Products: {products.Count}");
            Console.WriteLine($"Physical Products: {physicalCount}");
            Console.WriteLine($"Digital Products: {digitalCount}");
            Console.WriteLine($"Registered Customers: {customers.Count}");
            Console.WriteLine($"Total Orders: {orders.Count}");
            Console.WriteLine($"Total Revenue: {totalRevenue}");
            Console.WriteLine($"Total Users Created: {User.GetTotalUsersCreated()}");
            Console.WriteLine();
        }
    }


}
