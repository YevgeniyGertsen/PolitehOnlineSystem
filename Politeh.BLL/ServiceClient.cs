using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Politeh.BLL.Model;
using Politeh.DAL;
using Politeh.DAL.Model;

namespace Politeh.BLL
{
    public class ServiceClient: Service<Client>
    {
        public ServiceClient(string path): base(path)
        {            
        }

        public (bool isError, string message) RegisterClient(ClientDTO client)
        {
            result = repo.Create(_iMapper.Map<Client>(client));
            if (result.IsError == true && del != null)
            {
                del.Invoke(result.IsError, result?.Exception.Message);
            }
            return (result.IsError,
                result.Exception != null
                 ? result.Exception.Message
                 : "");
        }

        public ClientDTO AuthorizationClient(ClientDTO client)
        {
            result = repo.GetAll();
            if (result.IsError == true && del != null)
            {
                del.Invoke(result.IsError, result?.Exception.Message);
            }
            result.Data = result.ListData
                .FirstOrDefault(f=>f.Email == client.Email
                && f.Password == client.Password);
                     
            return _iMapper.Map<ClientDTO>(result.Data);
        }
    }
}