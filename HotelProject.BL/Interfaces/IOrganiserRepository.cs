using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IOrganiserRepository
    {
        void AddOrganiser(Organiser organiser);
        List<Organiser> GetOrganisers();
    }
}