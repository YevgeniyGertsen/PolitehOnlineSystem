﻿using Politeh.DAL.Model;
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
    public class ServiceAccount
    {
        private Repository<Account> repo = null;
        private ReturnResult<Account> result = null;
        private readonly IMapper _iMapper;
        public ServiceAccount(string path)
        {
            repo = new Repository<Account>(path);
            _iMapper = SettingAutomapper.Init()
               .CreateMapper();
        }

        public List<AccountDTO> GetAllAccountClient(int ClientId)
        {
            result = repo.GetAll();

            result.ListData = result.ListData
                .Where(w => w.ClientId == ClientId)
                .ToList();

            return _iMapper.Map<List<AccountDTO>>(result.Data);
        }
    }
}
