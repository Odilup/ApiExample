using AutoMapper;
using InnoCVApi.API.Endpoint.Contracts;
using InnoCVApi.Domain.Entities.Users;

namespace InnoCVApi.API.Endpoint
{
    /// <summary>
    /// Profile for model mapping
    /// </summary>
    public class AutomapperProfile : Profile
    {
        /// <summary>
        /// Automapper profile
        /// </summary>
        public AutomapperProfile()
        {
            CreateMap<UserApiRequest, User>();
        }
    }
}