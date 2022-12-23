using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeWork_SuperMarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new List<Item>() 
            {
                new Item("Зелье здоровья",15),
                new Item("Меч",30),
                new Item("Злеье регинераци",20),
                new Item("Лук", 20),
                new Item("Старый шлем",10),
                new Item("Хлам",1)
            };

            Supermarket supermarket = new Supermarket(items);
            supermarket.Work();
        }
    }

    class Supermarket 
    {
        private Queue<User> _users = new Queue<User>();
        private List<Item> _items = new List<Item>();

        private int _money = 0;

        public Supermarket(List<Item> items)
        {
            _items = items;
        }

        public void Work() 
        {
            CreatQueue();

            foreach (var user in _users)
            {
                user.ShowInfo();

                while (user.AmountMoney < user.ClaculationPriseAllItems())
                {
                    Console.WriteLine("У мользователя не хвотает средст.");
                    Console.ReadKey();
                    user.ReamoveItem(0);
                    Console.ReadKey();
                    Console.Clear();
                    user.ShowInfo();
                }
              
                Console.WriteLine("Пользователь приобрел придметы");
                Console.WriteLine($"Вы получили {user.ClaculationPriseAllItems()} монет(-ы)");
                _money += user.ClaculationPriseAllItems();
                Console.WriteLine($"На вашем счету {_money}\nОжидаем нового клиента");
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Клиентов больше не отстаось.");
            Console.ReadKey();
        }

        private void CreatQueue() 
        {
            Random random = new Random();
            int maxCountUsers = 10;
            int minCountUsers = 5;
            int countUsers = random.Next(minCountUsers, maxCountUsers + 1);
            for (int i = 0; i < countUsers; i++)
            {
                _users.Enqueue(CreatUser(random))
                    ;
            }
        }

        private User CreatUser(Random random)
        {
            List<Item> usersItems = new List<Item>();
            int maxRandomMoney = 100;
            int minRandomMoney = 20;
            int amountMoney = random.Next(minRandomMoney, maxRandomMoney + 1);
            int minAmountItem = 3;
            int amountItem = random.Next(minAmountItem, _items.Count);

            for (int i = 0; i < amountItem; i++) 
            {
                int randomIndex = random.Next(0, _items.Count);
                usersItems.Add(_items[randomIndex]);
            }

            return new User(amountMoney, usersItems);
        }

    }

       class Item
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Item(string name, int prace)
        {
            Name = name;
            Price = prace;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} за {Price}");
        }
    }

    class User
    {
        public int AmountMoney { get; private set; }
        private List<Item> _items = new List<Item>();

        public User(int amountMoney, List<Item> items)
        {
            AmountMoney = amountMoney;
            _items = items;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"У пользователя {AmountMoney} монет");
            Console.WriteLine("И он хочиет приобрести: ");

            foreach (Item item in _items)
            {
                item.ShowInfo();
            }
        }

        public void ReamoveItem(int indexItem)
        {
            Console.WriteLine($"Был удален предмет \"{_items[indexItem].Name}\" за {_items[indexItem].Price}");
            _items.RemoveAt(indexItem);            
        }      

        public int ClaculationPriseAllItems() 
        {
            int priceAllItems = 0;

            foreach (Item item in _items) 
            {
                priceAllItems += item.Price;
            }

            return priceAllItems;
        }
    }
}
