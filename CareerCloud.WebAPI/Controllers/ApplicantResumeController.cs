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
    [Route("api/careercloud/resume/v1/[controller]")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic _logic;
        public ApplicantResumeController()
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);
        }
        [HttpGet]
        [Route("resume")]
        public ActionResult GetAllApplicantResume()
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
        [Route("resume/{applicantresumeId}")]
        // [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        // [ProducesResponseType(404)]
        public ActionResult GetApplicantResume(Guid applicantresumeId)
        {
            try
            {
                ApplicantResumePoco poco = _logic.Get(applicantresumeId);

                return Ok(poco);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("resume")]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] poco)
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
        [Route("resume")]
        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] poco)
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
        [Route("resume")]
        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] poco)
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