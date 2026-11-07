using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public record TaminKonanadehLoginModel
    {
        public int TaminKonandehLoginSN { get; set; }

        public decimal TaminKonandehSN { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int LPasswordTag { get; set; }

        public bool FlagAdmin { get; set; }

        public byte[]? PasswordEn { get; set; }

        public string? UserType { get; set; }

        public string? OperatorName { get; set; }

        public string? Tel { get; set; }

        public DateTime? LastEdit { get; set; }

        public bool SpecialUser { get; set; }

        public bool? ShowVazn { get; set; }

        public Guid ApiHash { get; set; }

        public int UserStatus { get; set; }
    }
}
