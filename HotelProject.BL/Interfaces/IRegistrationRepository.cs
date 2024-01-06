using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IRegistrationRepository
    {
        void AddRegistration(Registration registration);
        List<Registration> GetRegistrationByActivityId(int activityId);
        bool MemberRegistrated(string name, int registrationId);
    }
}