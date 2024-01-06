using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IActivityRepository
    {
        void AddActivity(Activity activity);
        List<Activity> GetAllActivities();
        void UpdateActivity(Activity activity);
    }
}