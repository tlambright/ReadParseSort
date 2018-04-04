using ReadParseSort;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace RestApplication.Controllers
{
    public class RecordsController : ApiController
    {
        // GET: api/Records
        public IEnumerable<Person> Get()
        {
            var readParseSort = new ReadParseSortRecords();

            //readParseSort.AddFileToFileList(@"C:\Users\Trent\Desktop\ParseSort\RecordsPipe.txt");
            //readParseSort.AddFileToFileList(@"C:\Users\Trent\Desktop\ParseSort\RecordsSpace.txt");
            //readParseSort.AddFileToFileList(@"C:\Users\Trent\Desktop\ParseSort\RecordsComma.txt");

            var supportingFilesDir = Path.Combine(HttpRuntime.AppDomainAppPath, "SupportingFiles");

            string[] files = Directory.GetFiles(supportingFilesDir);

            foreach (var filePath in files)
            {
                var filename = Path.GetFileName(filePath);
                if (filename.StartsWith("Records"))
                {
                    readParseSort.AddFileToFileList(filePath);
                }
            }

            readParseSort.ReadData();

            //Get list no specific sort 
            var personList = readParseSort.PersonList;

            return personList;
        }

        //// GET: api/Records/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpGet]
        public IEnumerable<Person> gender()
        {
            var readParseSort = new ReadParseSortRecords();

            var supportingFilesDir = Path.Combine(HttpRuntime.AppDomainAppPath, "SupportingFiles");

            string[] files = Directory.GetFiles(supportingFilesDir);

            foreach (var filePath in files)
            {
                var filename = Path.GetFileName(filePath);
                if (filename.StartsWith("Records"))
                {
                    readParseSort.AddFileToFileList(filePath);
                }
            }

            readParseSort.ReadData();

            //Get list sorted by Gender (F/M) ascending, Last Name ascending.
            var sortedListByGenderLastName = readParseSort.SortPersonsByGenderAscLastNameAsc();

            return sortedListByGenderLastName;
        }

        [HttpGet]
        public IEnumerable<Person> birthdate()
        {
            var readParseSort = new ReadParseSortRecords();

            var supportingFilesDir = Path.Combine(HttpRuntime.AppDomainAppPath, "SupportingFiles");

            string[] files = Directory.GetFiles(supportingFilesDir);

            foreach (var filePath in files)
            {
                var filename = Path.GetFileName(filePath);
                if (filename.StartsWith("Records"))
                {
                    readParseSort.AddFileToFileList(filePath);
                }
            }

            readParseSort.ReadData();

            //Get list sorted by by Date of Birth ascending.
            var sortedListByDateOfBirthAsc = readParseSort.SortPersonsByDateOfBirthAsc();

            return sortedListByDateOfBirthAsc;
        }

        [HttpGet]
        public IEnumerable<Person> name()
        {
            var readParseSort = new ReadParseSortRecords();

            var supportingFilesDir = Path.Combine(HttpRuntime.AppDomainAppPath, "SupportingFiles");

            string[] files = Directory.GetFiles(supportingFilesDir);

            foreach (var filePath in files)
            {
                var filename = Path.GetFileName(filePath);
                if (filename.StartsWith("Records"))
                {
                    readParseSort.AddFileToFileList(filePath);
                }
            }

            readParseSort.ReadData();

            //Get list sorted by Last Name descending.
            var sortedListByLastNameDesc = readParseSort.SortPersonsByLastName();

            return sortedListByLastNameDesc;
        }


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
