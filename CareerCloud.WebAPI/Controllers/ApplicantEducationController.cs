using Azure;
using CareerCloud.Pocos;
using CareerCloud.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System;
using CareerCloud.WebAPI.Exceptions;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/education/v1/[controller]")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;
        public ApplicantEducationController()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }
        [HttpGet]
        [Route("education")]
        public ActionResult GetAllApplicantEducation()
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
        [Route("education/{applicantEducationId}")]
       // [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
       // [ProducesResponseType(404)]
        public ActionResult GetApplicantEducation(Guid applicantEducationId)
        {
            try
            {
                ApplicantEducationPoco poco = _logic.Get(applicantEducationId);

                return Ok(poco);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }   
        }

        [HttpPost]
        [Route("education")]
        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
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
        [Route("education")]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
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
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
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






