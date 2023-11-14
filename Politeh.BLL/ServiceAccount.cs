using Politeh.DAL.Model;
using Politeh.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Politeh.BLL.Model;

namespace Politeh.BLL
{
    public class ServiceAccount: Service<Account>
    {
        public ServiceAccount(string path) : base(path)
        {
        }

        public List<AccountDTO> GetAllAccountClient(int ClientId)
        {
            result = repo.GetAll();
            if (result.IsError == true && del != null)
            {
                del.Invoke(result.IsError, result?.Exception.Message);
            }
            result.ListData = result.ListData
                .Where(w => w.ClientId == ClientId)
                .ToList();

            return _iMapper.Map<List<AccountDTO>>(result.Data);
        }

        public bool CreateAccount(Account account)
        {
           result = repo.Create(account);
           if ( result.IsError == true && del != null)
            {
                del.Invoke(result.IsError, result?.Exception.Message);

            }
            return result.IsError;
           
        }

        public bool RefillMoney(Account account)
        {
            result = repo.Update(account);
            if (result.IsError == true && del != null)
            {
                del.Invoke(result.IsError, result?.Exception.Message);

            }
            else if (result.IsError == false && del != null)
            {
                del.Invoke(result.IsError, "Счет успешно пополнен!");
            }
            return result.IsError;
        }
    }
}
