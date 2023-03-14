using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocAPI.DTOs
{
    public class FilterDocumentDTO
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public PaginationDTO PaginationDTO
        {
            get { return new PaginationDTO() { Page = Page, RecordsPerPage = RecordsPerPage }; }
        }
        public string Title { get; set; }
        public string CreatedDate { get; set; }
        public string email { get; set; }
    }
}
