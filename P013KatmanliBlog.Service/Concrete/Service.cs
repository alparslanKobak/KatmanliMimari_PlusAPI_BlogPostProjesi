using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Data;
using P013KatmanliBlog.Data.Concrete;
using P013KatmanliBlog.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Service.Concrete
{
    public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
    {
        public Service(DatabaseContext context) : base(context)
        {

        }
    }
}
