using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Politeh.DAL.Model;

namespace Politeh.DAL
{
    public class RepositoryClient
    {
        private readonly string path;

        public RepositoryClient(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("Путь в БД не может быть пустым");

            this.path = path;
        }

        /// <summary>
        /// Метод который возвращает список всех клиентов
        /// </summary>
        /// <returns></returns>
        public ReturnResultClient GetAllClients()
        {
            ReturnResultClient result = new ReturnResultClient();

            try
            {
                using (var db = new LiteDatabase(path))
                {
                    result.Clients = db.GetCollection<Client>("Client").FindAll().ToList();
                }
            }
            catch(LiteException ex) 
                when(string.IsNullOrWhiteSpace(path))
            {
                result.Exception = ex;
                result.IsError = true;
            }
            catch (Exception ex) 
            {
                result.Exception = ex;
                result.IsError = true;
            }

            return result;
        }

        /// <summary>
        /// Метод котоырй возвращает клиента по его ID
        /// </summary>
        /// <param name="Id">Уникальный номер клиента</param>
        /// <returns></returns>
        public ReturnResultClient GetClientById(int Id)
        {
            ReturnResultClient result = new ReturnResultClient();
            try
            {
                using (var db = new LiteDatabase(path))
                {
                    result.Client = db.GetCollection<Client>("Client").FindById(Id);
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsError = true;
            }

            return result;
        }

        /// <summary>
        /// Метод который  создает кдлиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public ReturnResultClient CreateClient(Client client)
        {
            ReturnResultClient result = new ReturnResultClient();
            try
            {
                using (var db = new LiteDatabase(path))
                {
                    var clients = db.GetCollection<Client>("Client");
                    clients.Insert(client);
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.IsError = true;
            }
            return result;
        }
        //Мет од кооторый обнорвляет клиента
        //Метод который удаляет клиента

        public ReturnResultClient GetClient(string email, string password)
        {
            ReturnResultClient result = new ReturnResultClient();
            try
            {
                using (LiteDatabase db = new LiteDatabase(path))
                {
                    result.Client =
                     db.GetCollection<Client>("Client").FindAll()
                        .First(f => f.Email == email && f.Password == password);
                }
            }
            catch (Exception e) {
                result.Exception = e;
                result.IsError = true;
            }
            return result;
        }
    }
}
