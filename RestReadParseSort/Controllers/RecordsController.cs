using ReadParseSort;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Reflection;

namespace RestReadParseSort.Controllers
{
    public class RecordsController : ApiController
    {
        //public string FilesDirectory { get; set; }
        //
        
        public RecordsController()
        {
        }

        #region GET

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            try
            {
                var readParseSort = new ReadParseSortRecords();
                var supportingFilesDir = GetSupportingFilesLocation();

                if (!String.IsNullOrEmpty(supportingFilesDir))
                {
                    readParseSort.FilesDirectory = supportingFilesDir;

                    IEnumerable<Person> persons = GetPersons(readParseSort);

                    if (persons != null)
                    {
                        // Write the list to the response body.
                        response = Request.CreateResponse(HttpStatusCode.OK, persons);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                // ?
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpGet]
        public HttpResponseMessage gender()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var readParseSort = new ReadParseSortRecords();
            var supportingFilesDir = GetSupportingFilesLocation();

            IEnumerable<Person> persons = null;

            if (!String.IsNullOrEmpty(supportingFilesDir))
            {
                readParseSort.FilesDirectory = supportingFilesDir;

                persons = GetPersonsSortedByGender(readParseSort);
            }

            if (persons != null)
            {
                // Write the list to the response body.
                response = Request.CreateResponse(HttpStatusCode.OK, persons);
            }

            return response;
        }

        [HttpGet]
        public HttpResponseMessage birthdate()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var readParseSort = new ReadParseSortRecords();
            var supportingFilesDir = GetSupportingFilesLocation();
            IEnumerable<Person> persons = null;

            if (!String.IsNullOrEmpty(supportingFilesDir))
            {
                readParseSort.FilesDirectory = supportingFilesDir;

                persons = GetPersonsSortedByBirthdate(readParseSort);
            }

            if (persons != null)
            {
                // Write the list to the response body.
                response = Request.CreateResponse(HttpStatusCode.OK, persons);
            }

            return response;
        }

        [HttpGet]
        public HttpResponseMessage name()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);

            var readParseSort = new ReadParseSortRecords();
            var supportingFilesDir = GetSupportingFilesLocation();
            IEnumerable<Person> persons = null;

            if (!String.IsNullOrEmpty(supportingFilesDir))
            {
                readParseSort.FilesDirectory = supportingFilesDir;

                persons = GetPersonsSortedByLastName(readParseSort);
            }

            if (persons != null)
            {
                // Write the list to the response body.
                response = Request.CreateResponse(HttpStatusCode.OK, persons);
            }

            return response;
        }
        #endregion GET


        #region Private Methods
        private string GetSupportingFilesLocation()
        {
            var supportingFilesDir = string.Empty;

            if (HttpContext.Current != null && !string.IsNullOrEmpty(HttpRuntime.AppDomainAppPath))
            {
                supportingFilesDir = Path.Combine(HttpRuntime.AppDomainAppPath, "SupportingFiles");
            }
            else
            {
                var fqp = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                supportingFilesDir = string.Format("{0}\\{1}", fqp, "SupportingFiles");
            }

            return supportingFilesDir;
        }



        private void GetAndReadFiles(ReadParseSortRecords readParseSort)
        {

            if (!String.IsNullOrEmpty(readParseSort.FilesDirectory))
            { 
                string[] files = Directory.GetFiles(readParseSort.FilesDirectory);

                foreach (var filePath in files)
                {
                    var filename = Path.GetFileName(filePath);
                    if (filename.StartsWith("Records"))
                    {
                        readParseSort.AddFilePathToFilePathList(filePath);
                    }
                }

                // If files were found, read the data
                if (readParseSort.FileList.Count > 0)
                { 
                    readParseSort.ReadData();
                }
            }
        }

        // GET: api/Records
        private IEnumerable<Person> GetPersons(ReadParseSortRecords readParseSort)
        {
            GetAndReadFiles(readParseSort);

            //Get list no specific sort 
            var personList = readParseSort.PersonList;

            return personList;
        }

        //// GET: api/Records/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        private IEnumerable<Person>GetPersonsSortedByGender(ReadParseSortRecords readParseSort)
        {
            GetAndReadFiles(readParseSort);

            //Get list sorted by Gender (F/M) ascending, Last Name ascending.
            var sortedListByGenderLastName = readParseSort.SortPersonsByGenderAscLastNameAsc();

            return sortedListByGenderLastName;
        }

        //[HttpGet]
        private IEnumerable<Person> GetPersonsSortedByBirthdate(ReadParseSortRecords readParseSort)
        {
            GetAndReadFiles(readParseSort);

            //Get list sorted by by Date of Birth ascending.
            var sortedListByDateOfBirthAsc = readParseSort.SortPersonsByDateOfBirthAsc();

            return sortedListByDateOfBirthAsc;
        }

        //[HttpGet]
        private IEnumerable<Person> GetPersonsSortedByLastName(ReadParseSortRecords readParseSort)
        {
            GetAndReadFiles(readParseSort);

            //Get list sorted by Last Name descending.
            var sortedListByLastNameDesc = readParseSort.SortPersonsByLastName();

            return sortedListByLastNameDesc;
        }
        #endregion Private Methods

        #region POST
        // POST: api/Records
        //public void Post([FromBody]string value)
        //{
        //}

        // POST: api/Records
        //public void Post([FromBody]string value)
        public HttpResponseMessage Post([FromBody]string value)
        {
            try
            {
                HttpResponseMessage message = null;

                if (string.IsNullOrEmpty(value))
                {
                    message = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    //Do something here to persist the data, not persisting at the moment
                    var readParseSort = new ReadParseSortRecords();
                    var person = readParseSort.GetPersonFromTextInput(value);

                    if (person != null)
                    {
                        var newID = 12;
                        message = Request.CreateResponse(HttpStatusCode.Created, person);
                        message.Headers.Location = new Uri(Request.RequestUri + newID.ToString());
                    }
                    else
                    {
                        message = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                }

                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddPerson([FromBody]Person person)
        {
            try
            {
                //Do something here to persist the data, not persisting at the moment

                var newID = 12;
                var message = Request.CreateResponse(HttpStatusCode.Created, person);
                message.Headers.Location = new Uri(Request.RequestUri + newID.ToString());

                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPost]
        public HttpResponseMessage AddPersonFromStringInput([FromBody]string personRecord)
        {
            try
            {
                HttpResponseMessage message = null;

                if (string.IsNullOrEmpty(personRecord))
                {
                    message = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                { 
                    //Do something here to persist the data, not persisting at the moment
                    var readParseSort = new ReadParseSortRecords();
                    var person = readParseSort.GetPersonFromTextInput(personRecord);

                    if (person != null)
                    {
                        var newID = 12;
                        message = Request.CreateResponse(HttpStatusCode.Created, person);
                        message.Headers.Location = new Uri(Request.RequestUri + newID.ToString());
                    }
                    else
                    {
                        message =  Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                }

                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        #endregion POST


        //// PUT: api/Records/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Records/5
        //public void Delete(int id)
        //{
        //}
    }
}
