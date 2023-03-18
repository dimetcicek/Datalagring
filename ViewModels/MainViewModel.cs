namespace Datalagring.ViewModels;

using Datalagring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Datalagring.Services;

public class MainViewModel : ViewModelBase
{
    private Status selectedStatus;
    private IEnumerable<Status> _statuses;
    private IEnumerable<Ticket> _tickets;
    private ICommand _createCommand;
    private ICommand _addCommentCommand;
    private Ticket _selectedTicket;
    private bool _showTicketInfo;
    private IEnumerable<Comment> _comments;
    private string _comment;

    public MainViewModel()
    {
        CreateCommand = new Command(OnCreate);
        AddCommentCommand = new Command(OnAddComment);
        Statuses = Enum.GetValues<Status>();
    }

    public ICommand CreateCommand
    {
        get => _createCommand;
        
        set
        {
            _createCommand = value;
            RaisePropertyChanged();
        }
    }

    public ICommand AddCommentCommand
    {
        get => _addCommentCommand;

        set
        {
            _addCommentCommand = value;
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

            OnStatusChanged();
        }
    }

    public IEnumerable<Ticket> Tickets
    {
        get => _tickets;

        set
        {
            _tickets = value;
            RaisePropertyChanged();
        }
    }

    public Ticket SelectedTicket
    {
        get => _selectedTicket;

        set
        {
            _selectedTicket = value;
            RaisePropertyChanged();

            OnSelectedTicketChanged();
        }
    }

    public IEnumerable<Comment> Comments
    {
        get => _comments;

        set
        {
            _comments = value;
            RaisePropertyChanged();
        }
    }

    public string Comment
    {
        get => _comment;

        set
        {
            _comment = value;
            RaisePropertyChanged();
        }
    }

    public bool ShowTicketInfo
    {
        get => _showTicketInfo;

        set
        {
            _showTicketInfo = value;

            RaisePropertyChanged();
        }
    }

    public override async void OnNavigatedTo()
    {
        Tickets = await TicketService.GetAllTickets();
    }

    private void UpdateComments()
    {
        if (SelectedTicket == null)
        {
            Comments = null;

            return;
        }

        var comments = SelectedTicket.Comments.ToList();
        comments.Sort((a, b) => DateTime.Compare(a.TimeStamp, b.TimeStamp));

        Comments = comments;
    }

    private void OnCreate()
    {
        NavigateTo(typeof(CreateTicketViewModel));
    }

    private async void OnAddComment()
    {
        if (SelectedTicket != null && !string.IsNullOrWhiteSpace(Comment))
        {
            await TicketService.AddComment(SelectedTicket, new Comment
            {
                Message = Comment,
                TimeStamp = DateTime.UtcNow
            });
         
            Comment = null;

            UpdateComments();
        }
    }

    private void OnSelectedTicketChanged()
    {
        Comment = null;

        if (SelectedTicket != null)
        {
            SelectedStatus = SelectedTicket.Status;

            UpdateComments();

            ShowTicketInfo = true;
        }
        else
        {
            ShowTicketInfo = false;
        }
    }

    private async void OnStatusChanged()
    {
        if (SelectedTicket == null) 
        {
            return;
        }

        if (SelectedTicket.Status == SelectedStatus)
        {
            return;
        }

        await TicketService.UpdateStatus(SelectedTicket, SelectedStatus);
    }
}