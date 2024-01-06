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
    public class OrganiserManager
    {
        private IOrganiserRepository _organiserRepository;

        public OrganiserManager(IOrganiserRepository organiserRepository)
        {
            _organiserRepository = organiserRepository;
        }

        public List<Organiser> GetOrganisers()
        {
            try
            {
                return _organiserRepository.GetOrganisers();
            }
            catch (Exception ex)
            {
                throw new OrganiserManagerException("GetOrganiser", ex);
            }
        }

        public void AddOrganiser(Organiser organiser)
        {
            try
            {
                _organiserRepository.AddOrganiser(organiser);
            }
            catch (Exception ex)
            {
                throw new OrganiserManagerException("AddOrganiser", ex);
            }
        }
    }
}