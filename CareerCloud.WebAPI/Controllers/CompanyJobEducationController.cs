using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.WebAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/jobeducation/v1/[controller]")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _logic;
        public CompanyJobEducationController()
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repo);
        }
        [HttpGet]
        [Route("jobeducation")]
        public ActionResult GetAllCompanyJobEducation()
        {
            try
            {
                var company = _logic.GetAll();

                return Ok(company);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        [Route("jobeducation/{companyjobeducationId}")]
        // [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        // [ProducesResponseType(404)]
        public ActionResult GetCompanyJobEducation(Guid companyjobeducationId)
        {
            try
            {
                CompanyJobEducationPoco poco = _logic.Get(companyjobeducationId);

                return Ok(poco);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("jobeducation")]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
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
        [Route("jobeducation")]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
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
        [Route("jobeducation")]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
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