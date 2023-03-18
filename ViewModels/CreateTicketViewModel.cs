namespace Datalagring.ViewModels;

using Datalagring.Models;
using Datalagring.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

public class CreateTicketViewModel : ViewModelBase
{
    private string _name;
    private string _email;
    private string _phoneNumber;
    private string _description;
    private IEnumerable<Status> _statuses;
    private Status selectedStatus;

    public CreateTicketViewModel()
    {
        CreateTicketCommand = new Command(OnCreateTicket);
        CancelCommand = new Command(OnCancel);

        Statuses = Enum.GetValues<Status>();
    }

    public ICommand CreateTicketCommand { get; }

    public ICommand CancelCommand { get; }

    public string Name
    {
        get => _name;

        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }

    public string Email
    {
        get => _email;

        set
        {
            _email = value;
            RaisePropertyChanged();
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;

        set
        {
            _phoneNumber = value;
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

    public IEnumerable<Status> Statuses
    {
        get => _statuses;

        set
        {
            _statuses = value;
            RaisePropertyChanged();
        }
    }

    public Status SelectedStatus
    {
        get => selectedStatus;

        set
        {
            selectedStatus = value;
            RaisePropertyChanged();
        }
    }

    public override void OnNavigatedTo()
    {
        Name = null;
        Description = null;
        Email = null;
        PhoneNumber = null;
        SelectedStatus = Status.NotStarted;
    }

    private async void OnCreateTicket()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            MessageBox.Show("Name can not be empty");
            return;
        }

        var nameSplit = Name?.Split(' ');

        var firstName = string.Empty;
        var lastName = string.Empty;

        if (nameSplit != null)
        {
            if (nameSplit.Length > 1)
            {
                lastName = nameSplit.Last();
                firstName = string.Join(' ', nameSplit.Take(nameSplit.Length - 1));
            }
            else
            {
                firstName = nameSplit.First();
            }
        }

        if (string.IsNullOrWhiteSpace(Email))
        {
            MessageBox.Show("E-mail can not be empty");
            return;
        }

        // Copied the regex from here: https://regexr.com/3e48o
        if (!Regex.IsMatch(Email, @"^[\w\-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
        {
            MessageBox.Show(Email + " is not a valid e-mail");
            return;
        }

        var phoneNumberString = PhoneNumber?.Replace("+46", "0");

        if (!string.IsNullOrWhiteSpace(phoneNumberString))
        {
            if (phoneNumberString.Length > 15)
            {
                MessageBox.Show("A phone number can not be longer than 15 digits");
                return;
            }

            foreach (var character in phoneNumberString)
            {
                if (!char.IsDigit(character))
                {
                    MessageBox.Show(phoneNumberString + " is not a valid phone number!");

                    return;
                }
            }
        }

        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = Email,
            PhoneNumber = phoneNumberString
        };

        var ticket = new Ticket
        {
            Code = Guid.NewGuid(),
            Description = Description,
            TimeStamp = DateTime.UtcNow,
            Customer = customer,
            Status = SelectedStatus
        };

        try
        {
            await TicketService.CreateTicket(ticket);

            NavigateTo(typeof(MainViewModel));
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to create ticket!");
        }
    }

    private void OnCancel()
    {
        NavigateTo(typeof(MainViewModel));
    }
}