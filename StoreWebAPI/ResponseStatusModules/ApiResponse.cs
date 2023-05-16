namespace StoreWebAPI.ResponseStatusModules
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            SatausCode = statusCode;
            Message = GetErrorMessageForSatausCode(statusCode);
        }
        public int SatausCode { get; set; }
        public string? Message { get; set; }

        public string GetErrorMessageForSatausCode(int satausCode)
        {
            return satausCode switch
            {
                301 => "Moved Permanently",
                400 => "Bad Request",
                403 => "Forbidden",
                404 => "Not Found",
                405 => "Method Not Allowed",
                502 => "Bad Gateway",
                500 => "Internal Server Error",
                _ => string.Empty

            };
        }

    }
}
