using System;

namespace Lab5Charp
{
    // =================================================================
    // ЗАВДАННЯ 1: Ієрархія товарів (Іграшка, Продукт, Товар, Молочний продукт)
    // =================================================================

    // Інтерфейс для всіх предметів у магазині
    public interface IStoreItem
    {
        void Show();
        string Name { get; set; }
    }

    // Базовий абстрактний клас: Товар (Goods)
    public abstract class Goods : IStoreItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Goods(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public abstract void Show(); // Абстрактний метод для реалізації в дочірніх класах

        // Перевизначення стандартних методів Object
        public override string ToString() => $"Товар: {Name}, Ціна: {Price} грн";

        public override bool Equals(object obj)
        {
            if (obj is Goods other)
                return this.Name == other.Name && this.Price == other.Price;
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Price);
    }

    // Дочірній клас 1: Іграшка (Toy)
    public class Toy : Goods
    {
        public int AgeLimit { get; set; }

        public Toy(string name, decimal price, int ageLimit) : base(name, price)
        {
            AgeLimit = ageLimit;
        }

        public override void Show()
        {
            Console.WriteLine($"[Іграшка] Назва: {Name,-15} | Ціна: {Price,6} грн | Вікове обмеження: {AgeLimit}+");
        }
    }

    // Дочірній клас 2: Продукт харчування (Product)
    public class Product : Goods
    {
        public DateTime ExpirationDate { get; set; }

        public Product(string name, decimal price, DateTime expDate) : base(name, price)
        {
            ExpirationDate = expDate;
        }

        public override void Show()
        {
            Console.WriteLine($"[Продукт] Назва: {Name,-15} | Ціна: {Price,6} грн | Придатний до: {ExpirationDate.ToShortDateString()}");
        }
    }

    // Дочірній клас 3: Молочний продукт (MilkProduct - успадковується від Product)
    public class MilkProduct : Product
    {
        public double FatPercentage { get; set; }

        public MilkProduct(string name, decimal price, DateTime expDate, double fatPercentage)
            : base(name, price, expDate)
        {
            FatPercentage = fatPercentage;
        }

        public override void Show()
        {
            Console.WriteLine($"[Молочка] Назва: {Name,-15} | Ціна: {Price,6} грн | Жирність: {FatPercentage}% | Придатний до: {ExpirationDate.ToShortDateString()}");
        }
    }

    // =================================================================
    // ЗАВДАННЯ 2: Двовимірні фігури (Квадрат)
    // =================================================================
    public abstract class Figure
    {
        public abstract double Area();
        public abstract double Perimeter();

        public virtual void Print()
        {
            Console.WriteLine($"Площа: {Area():F2}, Периметр: {Perimeter():F2}");
        }
    }

    public class Square : Figure
    {
        public double Side { get; set; }

        public Square(double side) { Side = side; }

        public override double Area() => Side * Side;
        public override double Perimeter() => 4 * Side;

        public override void Print()
        {
            Console.WriteLine($"[Квадрат] Сторона = {Side}. Площа = {Area():F2}, Периметр = {Perimeter():F2}");
        }
    }

    // =================================================================
    // ЗАВДАННЯ 3: Тривимірні фігури (Куб)
    // =================================================================
    public abstract class Figure3D
    {
        public abstract double SurfaceArea();
        public abstract double Volume();

        public virtual void Print()
        {
            Console.WriteLine($"Площа поверхні: {SurfaceArea():F2}, Об'єм: {Volume():F2}");
        }
    }

    public class Cube : Figure3D
    {
        public double Side { get; set; }

        public Cube(double side) { Side = side; }

        public override double SurfaceArea() => 6 * Side * Side;
        public override double Volume() => Math.Pow(Side, 3);

        public override void Print()
        {
            Console.WriteLine($"[Куб] Сторона = {Side}. Площа повної поверхні = {SurfaceArea():F2}, Об'єм = {Volume():F2}");
        }
    }

    // =================================================================
    // ГОЛОВНА ПРОГРАМА З МЕНЮ
    // =================================================================
    class Program
    {
        static void Task1()
        {
            Console.WriteLine("\n--- Завдання 1.6: Ієрархія товарів ---");

            // Створюємо масив інтерфейсного типу, який зберігає об'єкти різних класів (ПОЛІМОРФІЗМ)
            IStoreItem[] storeInventory = new IStoreItem[]
            {
                new Toy("Конструктор LEGO", 1500.50m, 6),
                new Product("Хліб білий", 25.00m, DateTime.Now.AddDays(3)),
                new MilkProduct("Йогурт полуничний", 45.00m, DateTime.Now.AddDays(10), 2.5),
                new Toy("Плюшевий ведмідь", 450.00m, 0),
                new MilkProduct("Молоко селянське", 38.50m, DateTime.Now.AddDays(5), 3.2)
            };

            Console.WriteLine("Каталог магазину:");
            foreach (var item in storeInventory)
            {
                // Метод Show() автоматично розуміє, об'єкт якого саме класу зараз обробляється
                item.Show();
            }

            Console.WriteLine("\nПеревірка методу Equals():");
            Goods item1 = new MilkProduct("Кефір", 30m, DateTime.Now, 1.0);
            Goods item2 = new MilkProduct("Кефір", 30m, DateTime.Now.AddDays(1), 2.5);
            Console.WriteLine($"Чи однакові 'Кефір' за 30 грн (різна жирність)? -> {item1.Equals(item2)}");
        }

        static void Task2()
        {
            Console.WriteLine("\n--- Завдання 2.6: Фігура (Квадрат) ---");

            Console.Write("Введіть довжину сторони квадрата: ");
            if (double.TryParse(Console.ReadLine(), out double side) && side > 0)
            {
                Figure mySquare = new Square(side);
                mySquare.Print();
            }
            else
            {
                Console.WriteLine("Помилка: введіть коректне додатне число.");
            }
        }

        static void Task3()
        {
            Console.WriteLine("\n--- Завдання 3.6: Просторова фігура (Куб) ---");

            Console.Write("Введіть довжину ребра куба: ");
            if (double.TryParse(Console.ReadLine(), out double side) && side > 0)
            {
                Figure3D myCube = new Cube(side);
                myCube.Print();
            }
            else
            {
                Console.WriteLine("Помилка: введіть коректне додатне число.");
            }
        }

        static void Main()
        {
      
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("Оберіть завдання для запуску:");
                Console.WriteLine("1 - Завдання 1.6 (Товари, Продукти, Іграшки)");
                Console.WriteLine("2 - Завдання 2.6 (Площа і периметр квадрата)");
                Console.WriteLine("3 - Завдання 3.6 (Площа поверхні і об'єм куба)");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1": Task1(); break;
                    case "2": Task2(); break;
                    case "3": Task3(); break;
                    default: Console.WriteLine("Неправильне введення. Спробуйте ще раз."); break;
                }
            }
        }
    }
}