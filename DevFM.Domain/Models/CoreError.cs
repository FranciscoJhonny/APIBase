using System;
namespace DevFM.Domain.Models
{
    [Serializable]
    public class CoreError
    {
        public CoreError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; }

        public string Message { get; }
    }
}
