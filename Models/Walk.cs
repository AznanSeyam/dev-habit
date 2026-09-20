using System;

namespace dev_habit.Models;

public class Walk
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public Double LengthKm { get; set; }
    public String? WalkImgURL { get; set; }
    public Guid DifficultyID { get; set; }
    public Difficulty Difficulty { get; set; }
    public Guid RegionID { get; set; }
    public Region Region { get; set; }

}
