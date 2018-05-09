using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oFormsAPI.Models
{
    public class FormsConfiguration
    {
        public string StorageConnectionString { get; set; }
        public string SendGridAPIKey { get; set; }
        public string MainSiteURLRedirect { get; set; }
        public string Location { get; set; }
    }
}
