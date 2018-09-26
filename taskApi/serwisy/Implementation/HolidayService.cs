using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taskApi.Contract;
using taskApi.DAL;
using taskApi.Enums;
using taskApi.Help;
using taskApi.VM;

namespace taskApi.serwisy
{
    public class HolidayService : IHolidayService
    {
        private TaskApiContext _dbContext = new TaskApiContext();
        private readonly IFunctionService _function;
        private bool disposed = false;
        public HolidayService(IFunctionService function)
        {
            _function = function;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        //TODO do usuniecia
        public ServiceResponse<DayVM> AddUserHoliday_ToDelete(string date)
        {
            ServiceResponse<DayVM> result = new ServiceResponse<DayVM>();
            try
            {
                DayVM d = new DayVM();
                int UserID = _function.CurrentUser().Id;
                DateTime oDate = DateTime.ParseExact(date, "yyyyMMdd", null);
                d.Date = date;

                d.Id = oDate.Day;
                var cDataBase = _dbContext.Calendars.SingleOrDefault(k => k.UserID == UserID && k.Day == oDate);
                if (cDataBase != null)
                {
                    _dbContext.Calendars.Remove(cDataBase);
                    _dbContext.SaveChanges();
                    d.DayType = oDate.IsWork(); ;
                }
                else
                {
                    var c = new Calendar();
                    c.UserID = _function.CurrentUser().Id;
                    d.DayType = DayType.Select;
                    c.Day = oDate;
                    c.HolidayType = HolidayType.Rezerwacja;
                    _dbContext.Calendars.Add(c);
                    _dbContext.SaveChanges();



                }

                result.Result = d;

            }
            catch (Exception ex)
            {

                _function.MessageErrorSetResponse(result, ex);
            }
            return result;
        }

        public ServiceListResponse<MyHolidayVM> GetGetHoliday()
        {
            var result = new ServiceListResponse<MyHolidayVM>();
            List<MyHolidayVM> lista = new List<MyHolidayVM>();
            int userId = _function.CurrentUser().Id;

            var myCalendar = _dbContext.Calendars.Where(kk => kk.UserID == userId).ToList();
            int k = 1;
            foreach (var item in classConst.monthName)
            {
                MyHolidayVM myHolidayVM = new MyHolidayVM();
                myHolidayVM.MonthName = item;
                myHolidayVM.Id = k;

                DateTime dt = new DateTime(classConst.Year, k, 1);
                int bb = dt.DayNumberWeek();
                for (int i = 1; i < bb; i++)
                {
                    myHolidayVM.Days.Add(new DayVM() { Id = 0, DayName = "", DayType = DayType.Empty });
                }
                for (int i = 1; i <= classConst.monthDayYear[k - 1]; i++)
                {
                    DateTime dtDay = new DateTime(classConst.Year, k, i);
                    DayVM dd = new DayVM();
                    dd.Id = i;

                    dd.DayType = dtDay.IsWork();
                    if (myCalendar.Any(kd => kd.Day == dtDay))
                    {
                        dd.DayType = DayType.Holiday;
                    }
                    dd.Date = dtDay.ToString(classConst.formatdaty);
                    dd.DayName = classConst.dayNameShort[dtDay.DayNumberWeek() - 1];
                    myHolidayVM.Days.Add(dd);
                }
                lista.Add(myHolidayVM);
                k++;
            }

            result.Result = lista;
            return result;
        }

        public ServiceResponse<UserHoliday> GetGetHoliday2()
        {
            ServiceResponse<UserHoliday> result = new ServiceResponse<UserHoliday>();
            try
            {
                int userId = _function.CurrentUser().Id;
                var data = _dbContext.Users.Include("Calendars").Single(k => k.Id == userId);
                result.Result = Mapper.Map<User, UserHoliday>(data);

            }
            catch (Exception ex)
            {

                _function.MessageErrorSetResponse(result, ex);
            }
            return result;
        }

        public ServiceResponse<bool> AddUserHoliday(AddHolidayVM addHolidayVM)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            int UserId = _function.CurrentUser().Id;
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            DateTime DateFrom = DateTime.ParseExact(addHolidayVM.DateFrom, classConst.format_daty, culture);
            DateTime DateTo = DateTime.ParseExact(addHolidayVM.DateTo, classConst.format_daty, culture);

            for (DateTime date = DateFrom; date <= DateTo; date = date.AddDays(1))
            {
                if (date.IsWork() == DayType.Work)
                {


                    Calendar c = new Calendar()
                    {
                        UserID = UserId,
                        Day = date
                    };

                    _dbContext.Calendars.Add(c);
                }

            }
            _dbContext.SaveChanges();
            return result;
        }
    }
}