using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.WebAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/skill/v1/[controller]")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic _logic;
        public ApplicantSkillController()
        {
            var repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repo);
        }
        [HttpGet]
        [Route("skill")]
        public ActionResult GetAllApplicantSkill()
        {
            try
            {
                var applicant = _logic.GetAll();

                return Ok(applicant);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet]
        [Route("skill/{applicantskillId}")]
        // [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        // [ProducesResponseType(404)]
        public ActionResult GetApplicantSkill(Guid applicantskillId)
        {
            try
            {
                ApplicantSkillPoco poco = _logic.Get(applicantskillId);

                return Ok(poco);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("skill")]
        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
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
        [Route("skill")]
        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
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
        [Route("skill")]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
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
