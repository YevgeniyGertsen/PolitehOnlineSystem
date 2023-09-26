using Politeh.BLL;
using Politeh.DAL;
using Politeh.DAL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Politeh.Online.View
{
    internal class Program
    {
        static string path = ConfigurationManager
            .ConnectionStrings["DefaultConnection"]
            .ConnectionString;

        static void Main(string[] args)
        {
            FirstMenu();
        }

        public static void FirstMenu()
        {
            Console.Clear();
            Console.WriteLine("1) Авторизация");
            Console.WriteLine("2) Регистрация");
            Console.WriteLine("3) Выход");
            int ch = int.Parse(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    Authorization();
                    break;
                case 2:
                    Registration();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }
        public static void Authorization()
        {
            Client client = new Client();
            Console.Write("Введите логин: ");
            client.Email = Console.ReadLine();
            Console.Write("Введите пароль: ");
            client.Password = Console.ReadLine();

            ServiceClient serviceClient = new ServiceClient(path);
            client = serviceClient.AuthorizationClient(client);
            if(client != null)
            {
                Console.WriteLine("Приветствую тебя, " +  client.FullName);
            }
            else
            {
                Console.WriteLine("Такой пользователь не зарегистрирован");
                Thread.Sleep(2000);
                FirstMenu();
            }
        }
        public static void Registration()
        {
            Client client = new Client();
            Console.Write("Введите email: ");

            client.Email = Console.ReadLine();
            Console.Write("Введите пароль: ");
            client.Password = Console.ReadLine();

            Console.Write("Введите имя: ");
            client.FName = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            client.SName = Console.ReadLine();

            Console.Write("Введите отчество: ");
            client.LName = Console.ReadLine();

            ServiceClient serviceClient = new ServiceClient(path);
            var result = serviceClient.RegisterClient(client);
            if (result.isError)
            {
                Console.Write(result.message);
            }
            else
            {

                Console.WriteLine("Все прошло успешно");
                Thread.Sleep(2000);
                FirstMenu();
            }

        }
    }
}
