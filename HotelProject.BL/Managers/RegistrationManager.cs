using HotelProject.BL.Exceptions;
using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Managers
{
    public class RegistrationManager
    {
        private IRegistrationRepository _registrationRepository;

        public RegistrationManager(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public void AddRegistration(Registration registration)
        {
            try
            {
                _registrationRepository.AddRegistration(registration);
            }
            catch (Exception ex)
            {
                throw new RegistrationManagerException("AddRegistration", ex);
            }
        }

        public List<Registration> GetRegistrationByActivityId(int activityId)
        {
            try
            {
                return _registrationRepository.GetRegistrationByActivityId(activityId);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("GetRegistrationByActivityId", ex);

            }
        }

        public bool MemberRegistrated(string name, int registrationId)
        {
            try
            {
                return _registrationRepository.MemberRegistrated(name, registrationId);
            }
            catch (Exception ex)
            {
                throw new RegistrationManagerException("MemberRegistrated", ex);
            }
        }
    }
}
