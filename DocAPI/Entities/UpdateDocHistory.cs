using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocAPI.Entities
{
    public class UpdateDocHistory
    { 
        [Key]
        public int Id { get; set; }
        public int DID { get; set; }

        public string Logo { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string SerialNumber { get; set; }
       
    }
}
