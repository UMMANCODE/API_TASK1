using System;
using System.ComponentModel.DataAnnotations;
namespace CourseApi.Data.Entities {
  public class Group {
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public int Limit { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; }
  }
}

