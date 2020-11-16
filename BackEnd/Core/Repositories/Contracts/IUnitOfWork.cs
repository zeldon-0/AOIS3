using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        IDrugRepository DrugRepository { get;  }
        ISuffixRepository SuffixRepository { get;  }
        Task SaveAsync();
    }
}
