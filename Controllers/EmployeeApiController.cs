using System.Web.Http;
using System.Net.Http;
using System.Net;
using System;
using System.Linq;
using System.Web.Http.Cors;

using KoncarWebService.App_Persistence;
using KoncarWebService.Data;

namespace KoncarWebService.Controllers
{

    [RoutePrefix("api/employee")]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class EmployeeApiController : ApiController
    {

		readonly EmployeePersistence persistence;


        public EmployeeApiController()
        {
            persistence = EmployeePersistence.getEmployeePersistence();
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]EmployeeDto employee)
        {
            try
            {
				long id = persistence.SaveEmployee(employee);

				// create http response
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, employee);
				response.Headers.Location = new Uri(Request.RequestUri, String.Format("/api/employee/{0}", id));

				return response;
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }

		[HttpGet]
        [Route("{id:long}")]
        public HttpResponseMessage GetEmployee(long id) 
        {
            try
            {
				EmployeeDto employee = persistence.GetEmployee(id);

				if (employee == null)
				{
					// failure
					return Request.CreateErrorResponse(
						HttpStatusCode.NotFound,
						new NoSuchEntityException(String.Format("Employee with id = {0} not found in the databse.", id)));
				}

				// success
				return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        [Route("{id:long}")]
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                if (persistence.DeleteEmployee(id) == 1)
                {
                    // success
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                }

                // not found
                return Request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    new NoSuchEntityException(String.Format("Employee with id = {0} not found in the database.", id)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPut]
        [Route("{id:long}")]
        public HttpResponseMessage Put(long id, [FromUri] EmployeeDto parameters)
        {
            try 
            {
                persistence.UpdateEmployee(id, parameters);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage ListEmployees([FromUri] EmployeeDto parameters)
        {
            try
            {
				// no parameters provided
				if (parameters == null)
				{
                    return Request.CreateResponse(HttpStatusCode.OK, persistence.GetEmployeeList());
				}

				// parameters provided
				IQueryable<EmployeeDto> employees = persistence.GetEmployeeList().AsQueryable();

				if (parameters.Id != null)
				{
					employees = employees.Where(e => e.Id == parameters.Id);
				}

				if (parameters.FirstName != null)
				{
					employees = employees.Where(e => e.FirstName.Equals(parameters.FirstName));
				}

				if (parameters.LastName != null)
				{
					employees = employees.Where(e => e.LastName.Equals(parameters.LastName));
				}

				if (parameters.CurrentPlace != null)
				{
					employees = employees.Where(e => e.CurrentPlace.Equals(parameters.CurrentPlace));
				}

				if (parameters.BirthPlace != null)
				{
					employees = employees.Where(e => e.BirthPlace.Equals(parameters.BirthPlace));
				}

				if (parameters.Gender != null)
				{
					employees = employees.Where(e => e.Gender.Equals(parameters.Gender));
				}

				if (parameters.Department != null)
				{
					employees = employees.Where(e => e.Department.Equals(parameters.Department));
				}

				if (parameters.OIB != null)
				{
					employees = employees.Where(e => e.OIB.Equals(parameters.OIB));
				}

                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}