using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Features.Auth.Validations;
public static class CustomAuthValidationRules
{
    public static bool PasswordShouldMatchConstraints(string password)
    {
        Regex regex = new Regex("^(?=.*[A-Z])(?=.*\\d)(?=.*[^\\w\\s]).+$");
        return regex.IsMatch(password);
    }
    public static bool PasswordShouldBeNullOrMatch(string password)
    {
        Regex regex = new Regex("^(?=.*[A-Z])(?=.*\\d)(?=.*[^\\w\\s]).+$");
        return string.IsNullOrEmpty(password) || regex.IsMatch(password);
    }
}
