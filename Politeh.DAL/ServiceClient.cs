using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Politeh.DAL.Model;

namespace Politeh.DAL
{
    public class ServiceClient
    {
        /// <summary>
        /// Метод который возвращает список всех клиентов
        /// </summary>
        /// <returns></returns>
        public List<Client> GetAllClients()
        {
            List<Client> cleints = null;

            using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
            {
                cleints = db.GetCollection<Client>("Client")
                    .FindAll()
                    .ToList();
            }

            return cleints;
        }

        /// <summary>
        /// Метод котоырй возвращает клиента по его ID
        /// </summary>
        /// <param name="Id">Уникальный номер клиента</param>
        /// <returns></returns>
        public Client GetClientById(int Id)
        {
            Client client = null;
            using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
            {
                client = db.GetCollection<Client>("Client")
                    .FindById(Id);
            }
            return client;
        }


        /// <summary>
        /// Метод который  создает кдлиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool CreateClient(Client client)
        {
            using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
            {
                var clients = db.GetCollection<Client>("Client");

                clients.Insert(client);
            }

            return true;
        }
        //Мет од кооторый обнорвляет клиента
        //Метод который удаляет клиента
    }
}
