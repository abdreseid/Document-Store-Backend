using DocAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocAPI.DTOs
{
    public class UpdateHistoriesCreationDTO
    {
        
        public IFormFile  Logo { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string SerialNumber { get; set; }
        
        public List<int> DocumentId { get; set; }
    }
}
