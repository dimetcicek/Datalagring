namespace Datalagring.Models.Entities;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Index(nameof(Email), IsUnique = true)]
public class CustomerEntity
{
    public CustomerEntity()
    {
    }

    public CustomerEntity(Customer customer)
    {
        FirstName = customer.FirstName;
        LastName = customer.LastName;
        PhoneNumber = customer.PhoneNumber;
        Email = customer.Email;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "varchar(15)")]
    public string PhoneNumber { get; set; }

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string Email { get; set; } = null!;

    public ICollection<TicketEntity> Tickets { get; set; } = new HashSet<TicketEntity>();
}