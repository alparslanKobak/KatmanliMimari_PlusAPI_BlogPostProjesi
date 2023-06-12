using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Service.Abstract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new()
    {
    }
}
