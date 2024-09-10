using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        private readonly DataContext _context;

        public PresenceHub(PresenceTracker tracker, DataContext context)
        {
            _tracker = tracker;
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var isOnline = await _tracker.UserConnected(Context.User.GetEmail(), Context.ConnectionId);
            var currentUserAssignedTeam = await _context.Teams
                .Include(t => t.AssignedDevelopers)
                .Include(t => t.Manager)
                .FirstOrDefaultAsync(t => t.AssignedDevelopers.Any(d => d.Developer.AppUser.Email == Context.User.GetEmail()) || t.Manager.AppUser.Email == Context.User.GetEmail());
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Context.User.GetEmail());

            if (isOnline && currentUserAssignedTeam != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, currentUserAssignedTeam.Id.ToString());
                await Clients.Group(currentUserAssignedTeam.Id.ToString()).SendAsync("UserIsOnline", Context.User.GetEmail());
            }

            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var isOffline = await _tracker.UserDisconnected(Context.User.GetEmail(), Context.ConnectionId);
            var currentUserAssignedTeam = await _context.Teams
                .Include(t => t.AssignedDevelopers)
                .Include(t => t.Manager)
                .FirstOrDefaultAsync(t => t.AssignedDevelopers.Any(d => d.Developer.AppUser.Email == Context.User.GetEmail()) || t.Manager.AppUser.Email == Context.User.GetEmail());
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Context.User.GetEmail());

            if (isOffline && currentUserAssignedTeam != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, currentUserAssignedTeam.Id.ToString());
                await Clients.Group(currentUserAssignedTeam.Id.ToString()).SendAsync("UserIsOffline", Context.User.GetEmail());
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}