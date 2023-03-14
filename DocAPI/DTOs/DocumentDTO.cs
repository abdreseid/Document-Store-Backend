using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocAPI.DTOs
{
    public class DocumentDTO
    {

        public int Id { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int NumberOfCopy { get; set; }
        public bool IsUpdated { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsArchived { get; set; }
        public string SerialNumber { get; set; }
        public string UserId { get; set; }
    }
}
