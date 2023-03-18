namespace Datalagring.Models.Entities;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

[Index(nameof(StatusCode), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StatusCode { get; set; }

    public ICollection<TicketEntity> Tickets { get; set; } = new HashSet<TicketEntity>();
}