using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocAPI.DTOs
{
    public class UpdateHistoriesDTO
    {
        public int DID { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string SerialNumber { get; set; }
    }
}
