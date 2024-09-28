using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointments.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
    }
}
