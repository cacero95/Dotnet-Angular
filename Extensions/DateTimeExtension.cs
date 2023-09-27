namespace API.Extensions
{
    public static class DateTimeExtension
    {
        public static int CalculateAge( this DateOnly date ) {
            var today = DateOnly.FromDateTime( DateTime.UtcNow );
            var age = today.Year - date.Year;
            return date > today.AddYears( -age )
                ? age - 1 : age;
        }
    }
}