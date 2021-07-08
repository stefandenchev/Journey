namespace Journey.Services.Data.Interfaces
{
    using System;

    public interface INewsUrlGenerator
    {
        string GenerateUrl(int id, string title, DateTime createdOn);
    }
}
