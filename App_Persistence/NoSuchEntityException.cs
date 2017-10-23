using System;
namespace KoncarWebService.App_Persistence
{
    public class NoSuchEntityException : Exception
    {
        public NoSuchEntityException(string message) : base(message)
        {
        }
    }
}
