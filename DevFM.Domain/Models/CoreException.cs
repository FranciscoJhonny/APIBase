using System.Runtime.Serialization;

namespace DevFM.Domain.Models
{
    public class CoreException : System.Exception
    {
        protected readonly ICollection<CoreError> _errors = new List<CoreError>();

        protected CoreException(string message)
            : base(message)
        {
        }

        protected CoreException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IEnumerable<CoreError> Errors => _errors;
        public virtual string Key { get; set; }
    }

    [Serializable]
    public class CoreException<T> : CoreException where T : CoreError
    {
        public CoreException()
            : base("Ops, something wrong happened.")
        {
        }

        protected CoreException(string message) :
            base(message)
        {
        }

        protected CoreException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CoreException AddError(params T[] errors)
        {
            foreach (var error in errors)
            {
                _errors.Add(error);
            }

            return this;
        }
    }
}
