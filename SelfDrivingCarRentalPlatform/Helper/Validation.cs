namespace SelfDrivingCarRentalPlatform.Helper;
using System.Text.RegularExpressions;
public class Validation
{
    public static string _phonePattern = @"^0\d{9}$";
    public static string _emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    
    public static bool CheckValidation(string input, string pattern)
    {
        return Regex.IsMatch(input, pattern);
    }
}