using OISPublic.Helper;

namespace OISPublic.OISDataRoomDto
{
    public class DocumentDto : ErrorStatusCode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string DocumentSource { get; set; }
        public string ViewerType { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool IsAllowDownload { get; set; }
        public float DocumentSize { get; set; }
        public string? Status { get; set; }

        public string? DocType { get; set; }

        public int IsCheckedIn { get; set; }
        public int IsCheckedOut { get; set; }
        public string extension { get; set; }
        public string DocumentType { get; set; }
        public int ViewCount { get; set; }
        public List<DocumentMetaDataDto>? DocumentMetaDatas { get; set; } = new List<DocumentMetaDataDto>();

        public bool IsFavourite { get; set; }

        public Guid? CloudId { get; set; }

        public string? CreatedByName { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public string FilePath { get; set; }
        public bool Isshared { get; set; }
        public bool? Purge { get; set; }

        public string? CloudSlug { get; set; }
        public string? DocumentNumber { get; set; }

        public string? ClientId { get; set; }
        public int CompanyId { get; set; }
        public string? UserName { get; set; }
        public string? CompanyName { get; set; }
        public bool? IsPersonal { get; set; }
        public string? Breadcrumbs { get; set; }




    }
}
