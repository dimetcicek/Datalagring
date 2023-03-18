namespace Datalagring.Models.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TicketEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "uniqueidentifier")]
    public Guid Code { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    [Column(TypeName = "datetime2")]
    public DateTime TimeStamp { get; set; }

    public int CustomerId { get; set; }

    public CustomerEntity Customer { get; set; } = null!;

    public int StatusId { get; set; }

    public StatusEntity Status { get; set; } = null!;

    public ICollection<CommentsEntity> Comments { get; set; } = new HashSet<CommentsEntity>();
}