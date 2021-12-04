namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int StatusCode, string Message = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message ?? GetDefualtMessageForStatusCode(StatusCode); 
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefualtMessageForStatusCode(int status_code)
        {
            return status_code switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side.",
                _ => null,
            };
        }
    }
}