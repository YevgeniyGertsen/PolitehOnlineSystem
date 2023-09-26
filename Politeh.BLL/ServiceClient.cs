using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Politeh.DAL;
using Politeh.DAL.Model;

namespace Politeh.BLL
{
    public class ServiceClient
    {
        private readonly string path = "";
        public ServiceClient(string path)
        {
            this.path = path;
        }

        public (bool isError, string message) RegisterClient(Client client)
        {
            RepositoryClient repo = new RepositoryClient(path);

            ReturnResultClient result = repo.CreateClient(client);
            
            //string message = "";
            //if(result.Exception != null)
            //{
            //    message = result.Exception.Message;
            //}
            //else
            //{
            //    message = "";
            //}

            return (result.IsError, 
                result.Exception!=null 
                 ? result.Exception.Message
                 : "");
        }

        public Client AuthorizationClient(Client client)
        {
            RepositoryClient repo = new RepositoryClient(path);
            ReturnResultClient result = repo.GetClient(client.Email, client.Password);

            return result.Client;
        }
    }
}
