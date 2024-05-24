namespace BlazorWebbApp.Models;

public class CoursesModel
{
    public string? Id { get; set; } 
    public string? ImageUri { get; set; }
    public bool IsBestSeller { get; set; }
    public string? Title { get; set; }
    public string? LikesInProcent { get; set; }
    public string? Likes { get; set; }
    public string? Hours { get; set; }
    public string? Author { get; set; }
    public string? Price { get; set; }
    public string? Discount { get; set; }
}
