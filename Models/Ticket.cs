namespace Datalagring.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Datalagring.Models.Entities;

public class Ticket : INotifyPropertyChanged
{
    private Guid _code;
    private string _description;
    private DateTime _timeStamp;
    private Customer _customer;
    private Status _status;
    private IList<Comment> _comments;

    public Ticket()
    {
    }

    public Ticket(TicketEntity ticketEntity)
    {
        Code = ticketEntity.Code;
        Description = ticketEntity.Description;
        TimeStamp = ticketEntity.TimeStamp;
        Status = (Status)ticketEntity.Status.StatusCode;
        Customer = new Customer
        {
            FirstName = ticketEntity.Customer.FirstName,
            LastName = ticketEntity.Customer.LastName,
            PhoneNumber = ticketEntity.Customer.PhoneNumber,
            Email = ticketEntity.Customer.Email
        };

        var comments = new List<Comment>();

        foreach (var commentsEntity in ticketEntity.Comments)
        {
            comments.Add(new Comment
            {
                Message = commentsEntity.Comment.Message,
                TimeStamp = commentsEntity.Comment.TimeStamp
            });
        }

        Comments = comments;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Guid Code
    {
        get => _code;

        set
        {
            _code = value;
            RaisePropertyChanged();
        }
    }

    public string Description
    {
        get => _description;

        set
        {
            _description = value;
            RaisePropertyChanged();
        }
    }

    public DateTime TimeStamp
    {
        get => _timeStamp;

        set
        {
            _timeStamp = value;
            RaisePropertyChanged();
        }
    }

    public Customer Customer
    {
        get => _customer;

        set
        {
            _customer = value;
            RaisePropertyChanged();
        }
    }

    public Status Status
    {
        get => _status;

        set
        {
            _status = value;
            RaisePropertyChanged();
        }
    }

    public IList<Comment> Comments
    {
        get => _comments;

        set
        {
            _comments = value;
            RaisePropertyChanged();
        }
    }

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}