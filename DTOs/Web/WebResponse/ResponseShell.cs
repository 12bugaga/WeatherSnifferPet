using Newtonsoft.Json;

namespace DTOs.Web.WebResponse
{
    [Serializable]
    public class ResponseShell<T> : ResponseShell
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public new T? Response { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseShell" /> class.
        /// </summary>
        public ResponseShell()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseShell{T}" /> class.
        /// </summary>
        /// <param name="value">the payload.</param>
        public ResponseShell(T value)
        {
            Response = value;
        }

    }

    [Serializable]
    public class ResponseShell
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string? Message { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public object? Response { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseShell" /> class.
        /// </summary>
        public ResponseShell()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseShell" /> class.
        /// </summary>
        /// <param name="internalError">the exception that occured when processing the request.</param>
        public ResponseShell(string internalError)
        {
            Message = internalError;
        }
    }
}
