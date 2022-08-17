using AutoMapper;
using Csi.Ems.Api.Core.Domain;

namespace Csi.Ems.Api.Models
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                .ReverseMap();

            //CreateMap<Camp, CampModel>()  //map from Camp to CampModel
            //    .ForMember(cm => cm.Venue, o => o.MapFrom(c => c.Location.VenueName))  //map individual member
            //    .ReverseMap();  //map from CampModel to Camp
            // .ForMember(t => t.TalkId, o => o.Ignore())

        }
    }
}
