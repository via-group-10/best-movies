namespace BestMovies.Api.Exceptions
{
    public class RecordAlreadyExistsException : Exception
    {
        public Type Type { get; private set; }
        public new string Message { get; private set; }

        public RecordAlreadyExistsException(Type type)
        {
            this.Type = type;
            this.Message = $"Record of {type} already exists.";
        }

    }
}
