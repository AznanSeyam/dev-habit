using System;
using System.ComponentModel.DataAnnotations;

namespace dev_habit.Models;

public class CreateLoginDTO
{

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
