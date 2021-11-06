namespace HG.Tuplify.Customer.WebApi.Triggers
{
    public record SignUpPostConfirmationItem
    {
        public SignUpPostConfirmationRequest Request { get; set; }

        public SignUpPostConfirmationResponse Response { get; set; }
    }
}
