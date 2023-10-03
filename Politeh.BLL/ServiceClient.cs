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
    public class ServiceClient
    {
        private Repository<Client> repo = null;
        private ReturnResult<Client> result = null;
        private readonly IMapper _iMapper;
        public ServiceClient(string path)
        {
            repo = new Repository<Client>(path);
            _iMapper = SettingAutomapper.Init()
                .CreateMapper();
        }


        public (bool isError, string message) RegisterClient(ClientDTO client)
        {
            result = repo.Create(_iMapper.Map<Client>(client));

            return (result.IsError,
                result.Exception != null
                 ? result.Exception.Message
                 : "");
        }

        public ClientDTO AuthorizationClient(ClientDTO client)
        {
            result = repo.GetAll();

            result.Data = result.ListData
                .FirstOrDefault(f=>f.Email == client.Email
                && f.Password == client.Password);
                     
            return _iMapper.Map<ClientDTO>(result.Data);
        }
    }
}
