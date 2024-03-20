namespace event_scheduler.api.Mapping
{
    public struct GlobalResponse<T>
    {
        public GlobalResponse
          (bool isSuccess, string? message = null, T? value = default, List<string>? errors = null)
        {
            if (isSuccess && errors != null || !isSuccess && errors == null)
            {
                throw new ArgumentException("invalid response");
            };

            IsSuccess = isSuccess;
            Message = message;
            Data = value;
            Errors = errors;
        }


        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}
