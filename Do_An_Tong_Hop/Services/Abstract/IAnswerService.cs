using API.Dtos.Answer;
using API.Dtos.Questions;
using API.Entities;

namespace API.Services.Abstract
{
    public interface IAnswerService
    {
        List<AnswerDto> GetAll(int PageIndex = 1, int PageSize = 30);
        Answer GetQuestionById(int id);
        void CreateAnswer(CreateAnswerDto input);
    }
}
