using System;
using System.ComponentModel.DataAnnotations;

namespace dev_habit.Models;

public class CreateRegionDTO
{
    [Required]
    [MinLength(3, ErrorMessage = "minimum 3 character")]
    [MaxLength(3, ErrorMessage = "maximum 3 character")]
    public String Code { get; set; }

    [Required]
    [MinLength(10, ErrorMessage = "mimimum 10 chhaacter")]
    [MaxLength(100, ErrorMessage = "maximumm 100 character")]
    public String Name { get; set; }
    public String? RegionImgURL { get; set; }
}