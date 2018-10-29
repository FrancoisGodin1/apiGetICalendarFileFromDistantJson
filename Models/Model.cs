using System;
using System.Text;

namespace ConvertJsonEventsOfCalendarToIcsFile
{
    class Model
    {
        public class Event{
            public string ID { get; set; }
            public DateTime DateEvent { get; set; }
            public string Objet { get; set; }
            public string Lieu { get; set; }
            public string Str_Hdeb { get; set; }
            public string Str_Hfin { get; set; }

            public void Add(StringBuilder sb)
            {
                string DateFormat = "yyyyMMddTHHmmssZ";
                string now = DateTime.Now.ToUniversalTime().ToString(DateFormat);
                DateTime dtStart = this.DateEvent.Date + new TimeSpan(Convert.ToInt32(this.Str_Hdeb.Substring(0, 2)), 0, 0);
                DateTime dtEnd = this.DateEvent.Date + new TimeSpan(Convert.ToInt32(this.Str_Hfin.Substring(0, 2)), 0, 0);

                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine("DTSTART:" + dtStart.ToUniversalTime().ToString(DateFormat));
                sb.AppendLine("DTEND:" + dtEnd.ToUniversalTime().ToString(DateFormat));
                sb.AppendLine("DTSTAMP:" + now);
                sb.AppendLine("UID:" + this.ID);
                sb.AppendLine("CREATED:" + now);
                sb.AppendLine("DESCRIPTION:" + "");
                sb.AppendLine("LAST-MODIFIED:" + now);
                sb.AppendLine("LOCATION:" + this.Lieu);
                sb.AppendLine("SEQUENCE:0");
                sb.AppendLine("STATUS:CONFIRMED");
                sb.AppendLine("SUMMARY:" + this.Objet);
                sb.AppendLine("TRANSP:OPAQUE");
                sb.AppendLine("END:VEVENT");
            }
        }   
    }
}
