using AutoMapper;
using Csi.Ems.Api.Core.Domain;

namespace Csi.Ems.Api.Models
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                .ReverseMap()
                .ForMember(em => em.Id, o => o.Ignore());

        }
    }
}
