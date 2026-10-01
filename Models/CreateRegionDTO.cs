using System;

namespace dev_habit.Models;

public class CreateRegionDTO
{
    public String Code { get; set; }
    public String Name { get; set; }
    public String? RegionImgURL { get; set; }
}
