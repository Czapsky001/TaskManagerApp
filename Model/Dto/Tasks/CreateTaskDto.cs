﻿using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Model.Dto;

public class CreateSubTaskDto
{
    [Required]
    public string CreatedByUserId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

}
