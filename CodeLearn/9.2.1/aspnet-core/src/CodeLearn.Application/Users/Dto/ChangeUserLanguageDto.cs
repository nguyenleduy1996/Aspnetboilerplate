using System.ComponentModel.DataAnnotations;

namespace CodeLearn.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}