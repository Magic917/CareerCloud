using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.WebAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/securityrole/v1/[controller]")]
    [ApiController]
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _logic;
        public SecurityRoleController()
        {
            var repo = new EFGenericRepository<SecurityRolePoco>();
            _logic = new SecurityRoleLogic(repo);
        }
        [HttpGet]
        [Route("securityrole")]
        public ActionResult GetAllSecurityRole()
        {
            try
            {
                var security = _logic.GetAll();

                return Ok(security);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        [Route("securityrole/{securityroleId}")]
        // [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        // [ProducesResponseType(404)]
        public ActionResult GetSecurityRole(Guid securityroleId)
        {
            try
            {
                SecurityRolePoco poco = _logic.Get(securityroleId);

                return Ok(poco);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("securityrole")]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] poco)
        {

            try
            {
                _logic.Add(poco);
                return Ok();
            }
            catch (DuplicateException ex)
            {
                return Conflict(ex.Message);

            }
            catch (InvalidObjectException ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpPut]
        [Route("securityrole")]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] poco)
        {
            try
            {
                _logic.Update(poco);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        [Route("securityrole")]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] poco)
        {
            try
            {
                _logic.Delete(poco);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}