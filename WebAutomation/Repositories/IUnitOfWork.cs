﻿namespace WebAutomation.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        IBookRepository Books { get; }
        Task<int> CompleteAsync();
    }
}