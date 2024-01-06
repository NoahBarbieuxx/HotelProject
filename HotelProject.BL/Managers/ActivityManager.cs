using HotelProject.BL.Exceptions;
using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity = HotelProject.BL.Model.Activity;

namespace HotelProject.BL.Managers
{
    public class ActivityManager
    {
        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public List<Activity> GetAllActivities()
        {
            try
            {
                return _activityRepository.GetAllActivities();
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("GetAllActivities", ex);
            }
        }

        public void AddActivity(Activity activity)
        {
            try
            {
                _activityRepository.AddActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("AddActivity", ex);
            }
        }

        public void UpdateActivity(Activity activity)
        {
            try
            {
                _activityRepository.UpdateActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityManagerException("UpdateActivity", ex);
            }
        }
    }
}