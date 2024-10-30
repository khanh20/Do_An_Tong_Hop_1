using API.Dtos.Questions;
using API.Dtos.Shared;
using API.Entities;

namespace API.Services.Abstract
{
    public interface IQuestionService
    {
        PageResultDto<List<QuestionsDto>> GetAll(int PageIndex = 1, int PageSize = 30);
        Question GetQuestionById(int id);

        void CreateQuestion(CreateQuestionDto input);

        Question UpdateQuestion(int id, UpdateQuestionDto input);

        void DeleteQuestion(int id);
    }
}
