namespace Datalagring.Services;

using Datalagring.Contexts;
using Datalagring.Models;
using Datalagring.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TicketService
{
    private static readonly DataContext _dataContext = new DataContext();

    public static async Task CreateTicket(Ticket ticket)
    {
        var statusEntity = await _dataContext.Status.FirstOrDefaultAsync(s => s.StatusCode == (int)ticket.Status);

        if (statusEntity == null)
        {
            throw new Exception(ticket.Status + " is not a valid status!");
        }

        var customerEntity = await _dataContext.Customer.FirstOrDefaultAsync(c => c.Email == ticket.Customer.Email);

        if (customerEntity == null)
        {
            customerEntity = new CustomerEntity(ticket.Customer);

            await _dataContext.Customer.AddAsync(customerEntity);
            await _dataContext.SaveChangesAsync();
        }

        var ticketEntity = new TicketEntity
        {
            Code = ticket.Code,
            Description = ticket.Description,
            TimeStamp = ticket.TimeStamp,
            CustomerId = customerEntity.Id,
            StatusId = statusEntity.Id
        };

        await _dataContext.Ticket.AddAsync(ticketEntity);
        await _dataContext.SaveChangesAsync();
    }

    public static async Task<IEnumerable<Ticket>> GetAllTickets()
    {
        var ticketEntities = await _dataContext.Ticket
            .Include(t => t.Status)
            .Include(t => t.Customer)
            .Include(t => t.Comments)
            .ThenInclude(c => c.Comment)
            .ToListAsync();

        var tickets = new List<Ticket>();

        foreach (var ticketEntity in ticketEntities)
        {
            tickets.Add(new Ticket(ticketEntity));
        }

        return tickets;
    }

    public static async Task AddComment(Ticket ticket, Comment comment)
    {
        var ticketEntity = await _dataContext.Ticket.FirstOrDefaultAsync(t => t.Code == ticket.Code);

        if (ticketEntity == null)
        {
            throw new Exception("Could not find the ticket!");
        }

        var commentEntity = new CommentEntity(comment);

        await _dataContext.Comment.AddAsync(commentEntity);
        await _dataContext.SaveChangesAsync();

        var commentsEntity = new CommentsEntity
        {
            CommentId = commentEntity.Id,
            TicketId = ticketEntity.Id
        };

        await _dataContext.Comments.AddAsync(commentsEntity);
        await _dataContext.SaveChangesAsync();

        ticket.Comments.Add(comment);
    }

    public static async Task UpdateStatus(Ticket ticket, Status status)
    {
        var ticketEntity = await _dataContext.Ticket.FirstOrDefaultAsync(t => t.Code == ticket.Code);

        if (ticketEntity == null)
        {
            throw new Exception("Could not find the ticket!");
        }

        var statusEntity = await _dataContext.Status.FirstOrDefaultAsync(s => s.StatusCode == (int)status);

        if (statusEntity == null)
        {
            throw new Exception(ticket.Status + " is not a valid status!");
        }

        ticketEntity.StatusId = statusEntity.Id;
        await _dataContext.SaveChangesAsync();
        ticket.Status = status;
    }
}