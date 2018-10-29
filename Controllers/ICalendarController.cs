using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using static ConvertJsonEventsOfCalendarToIcsFile.Model;

namespace WbsICalendar.Controllers
{
    [RoutePrefix("GetICallEvents")]
    public class ICalendarController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetICallEvents()
        {
            string json = new WebClient().DownloadString("http://srv-ipso.cloudapp.net/getevents");

            var listEvents = JsonConvert.DeserializeObject<List<Event>>(json);

            StringBuilder sb = WriteICallFormat(listEvents);

            string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + "events.ics";
            System.IO.File.WriteAllText(path, sb.ToString());
          
            HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.OK);
            FileStream fs = File.OpenRead(path);
            resp.Content = new StreamContent(fs);
            resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/calendar");
            return resp;
        }

        static StringBuilder WriteICallFormat(List<Event> events)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Fgodin//test");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:PUBLISH");

            foreach (Event e in events)
            {
                e.Add(sb);
            }
            sb.AppendLine("END:VCALENDAR");
            return sb;
        }
    }
}
