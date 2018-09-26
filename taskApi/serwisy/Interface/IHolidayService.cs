using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskApi.Contract;
using taskApi.VM;

namespace taskApi.serwisy
{
    public interface IHolidayService : IDependency
    {
        ServiceListResponse<MyHolidayVM> GetGetHoliday();

        ServiceResponse<UserHoliday> GetGetHoliday2();
        ServiceResponse<bool> AddUserHoliday(AddHolidayVM addHolidayVM);
    }
}
