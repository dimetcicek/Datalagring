namespace Datalagring.Models.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CommentEntity
{
    public CommentEntity()
    {
    }

    public CommentEntity(Comment comment)
    {
        Message = comment.Message;
        TimeStamp = comment.TimeStamp;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(250)]
    public string Message { get; set; } = null!;

    [Required]
    [Column(TypeName = "datetime2")]
    public DateTime TimeStamp { get; set; }

    public ICollection<CommentsEntity> Comments { get; set; } = new HashSet<CommentsEntity>();
}