namespace Datalagring.Models.Entities;

using System.ComponentModel.DataAnnotations;

public class CommentsEntity
{
    [Key]
    public int Id { get; set; }

    public int TicketId { get; set; }

    public TicketEntity Ticket { get; set; } = null!;

    public int CommentId { get; set; }

    public CommentEntity Comment { get; set; } = null!;
}