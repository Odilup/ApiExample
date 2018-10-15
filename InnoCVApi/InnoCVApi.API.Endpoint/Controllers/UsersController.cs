using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using InnoCVApi.API.Endpoint.ActionFilters;
using InnoCVApi.API.Endpoint.Contracts;
using InnoCVApi.Core.Common.ModelMapper;
using InnoCVApi.Core.Resources;
using InnoCVApi.Domain.Entities.Users;
using InnoCVApi.Domain.Repositories.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace InnoCVApi.API.Endpoint.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [RoutePrefix("api")]
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User, int> _usersRepository;

        /// <summary>
        /// Users controller
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="modelMapper"></param>
        public UsersController(IUnitOfWork unitOfWork, IModelMapper modelMapper) : base(modelMapper)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = unitOfWork.UsersRepository;
        }

        /// <summary>
        /// Returns a complete user list
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.NoContent, "No users in database")]
        [SwaggerResponse(HttpStatusCode.OK, "Ok", typeof(IEnumerable<User>))]
        [HttpGet]
        [Route("Users")]
        public async Task<IHttpActionResult> ApiUserGetAll()
        {
            var users = await _usersRepository.GetAllAsync().ConfigureAwait(false);
            return ResponseMessage(!users.Any()
                ? Request.CreateResponse(HttpStatusCode.NoContent)
                : Request.CreateResponse(HttpStatusCode.OK, users));
        }

        /// <summary>
        /// Get an user by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.NotFound, "User not found")]
        [SwaggerResponse(HttpStatusCode.OK, "User found", typeof(User))]
        [HttpGet]
        [Route("Users/{id}")]
        public async Task<IHttpActionResult> ApiUserGetById([FromUri] int id)
        {
            var user = await _usersRepository.GetByIdAsync(id).ConfigureAwait(false);

            return ResponseMessage(user != null
                ? Request.CreateResponse(HttpStatusCode.OK, user)
                : Request.CreateResponse(HttpStatusCode.NotFound));
        }

        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="body">Body</param>
        /// <returns>New user's entity</returns>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.Conflict, "User already exists")]
        [SwaggerResponse(HttpStatusCode.Created, "User successfully added", typeof(User))]
        [HttpPost]
        [Route("Users")]
        [ValidateBody("body")]
        public async Task<IHttpActionResult> ApiUserPost([FromBody] UserApiRequest body)
        {
            var newUser = ModelMapper.Map<UserApiRequest, User>(body);

            if (await _usersRepository.GetByIdAsync(newUser.Id).ConfigureAwait(false) != null)
            {
                var diagnosis = new DiagnosisContract(HttpStatusCode.Conflict,
                    CoreDomainStringResources.Error_UserAlreadyExists);
                return ResponseMessage(Request.CreateResponse(diagnosis.HttpStatusCode, diagnosis));
            }

            _usersRepository.Add(newUser);
            await _unitOfWork.CommitAsync();
            return Created(UriComposer.ComposeLocationUri(HostBaseAddress, "Users", newUser.Id),newUser);
        }


        /// <summary>
        /// Modifies an existing user
        /// </summary>
        /// <param name="body">Body</param>
        /// <returns>User's modified data</returns>
        [SwaggerResponse(HttpStatusCode.Created, "User created", typeof(User))]
        [SwaggerResponse(HttpStatusCode.OK, "User modified", typeof(User))]
        [HttpPut]
        [Route("Users")]
        [ValidateBody("body")]
        public async Task<IHttpActionResult> ApiUserPut([FromBody] UserApiRequest body)
        {
            var user = ModelMapper.Map<UserApiRequest, User>(body);
            IHttpActionResult response;

            if (await _usersRepository.GetByIdAsync(user.Id).ConfigureAwait(false) == null)
            {
                _usersRepository.Add(user);
                response = Created(UriComposer.ComposeLocationUri(HostBaseAddress, "Users", user.Id), user);
            }
            else
            {
                _usersRepository.Modify(user);
                response = Ok(user);
            }

            await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Deletes an user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "User not found")]
        [SwaggerResponse(HttpStatusCode.NoContent, "User deleted")]
        [HttpDelete]
        [Route("Users/{id}")]
        public async Task<IHttpActionResult> ApiUserDelete([FromUri] int id)
        {
            var user = await _usersRepository.GetByIdAsync(id).ConfigureAwait(false);

            if ( user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            _usersRepository.Delete(user);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }

    }
}
