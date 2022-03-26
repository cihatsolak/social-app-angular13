namespace ServerApp.Helpers
{
    public static class ExtensionMethods
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age -= 1;

            return age;
        }
    }
}
