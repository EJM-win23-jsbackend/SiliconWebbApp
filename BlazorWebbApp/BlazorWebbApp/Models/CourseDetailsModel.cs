using System.ComponentModel.DataAnnotations;

namespace BlazorWebbApp.Models
{
    public class CourseDetailsModel
    {
    
        public string? ImageHeaderUri { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsDigital { get; set; }
        public string? Ingress { get; set; }
        public string? Reviews { get; set; }
        public string? Title { get; set; }
        public string? LikesInProcent { get; set; }
        public string? Likes { get; set; }
        public string? Hours { get; set; }
        public List<AuthorModel>? Authors { get; set; } 
        public string? Price { get; set; }
        public string? Discount { get; set; }

        public string? Description { get; set; }
        public List<ProgramDetail>? ProgramDetails { get; set; }
        public string[]? Includes { get; set; }
        public string[]? Learning { get; set; }

        [Range(0, 5)]
        public int? StarRating { get; set; }

    }

    public class ProgramDetail
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
    }

    public class AuthorModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AuthorImage { get; set; }
    }

}
