using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Questions
{
    public class UpdateQuestionDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Trường này không được bỏ trống")]
        [MaxLength(300, ErrorMessage = "Trường này tối đa 300 kí tự")]
        public string Content { get; set; }

        public string OptionA { get;  set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        
    }
}
