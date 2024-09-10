namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}
        IMessageRepository MessageRepository {get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}