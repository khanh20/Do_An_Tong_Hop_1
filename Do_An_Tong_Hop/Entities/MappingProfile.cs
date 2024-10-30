using API.Dtos.Answer;
using API.Dtos.Questions;
using AutoMapper;

namespace API.Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping từ Question -> QuestionDTO
            CreateMap<Question, QuestionsDto>();
            CreateMap<Answer, AnswerDto>();
        }
    }
}
