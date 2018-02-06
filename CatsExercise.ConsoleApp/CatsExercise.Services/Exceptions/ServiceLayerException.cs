using System;

namespace CatsExercise.Services.Exceptions
{
    public class ServiceLayerException : ApplicationException
    {
        public ServiceLayerException() : base()
        {
        }

        public ServiceLayerException(string message) : base(message)
        {
        }

        public ServiceLayerException(string message, Exception innerExeption) : base(message, innerExeption)
        {
        }
    }
    
}
