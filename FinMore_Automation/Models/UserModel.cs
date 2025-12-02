using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinMore_Automation.Models;

public class UserModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Currency { get; set; }

    public string Theme { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public bool IsAuthenticated { get; set; }

    public string Token {get; set; }
}
