using System;

namespace To_Do_List.Domain.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException()
        {
        }
        public RecordNotFoundException(string message) : base(message)
        {
        }
    }
}
