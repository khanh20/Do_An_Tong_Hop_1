using API.DbContexts;
using API.Dtos.Questions;
using API.Dtos.Shared;
using API.Entities;
using API.Exceptions;
using API.Services.Abstract;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implement
{
    public class QuestionService : IQuestionService
    {
        private ApplicationDbContext _applicationsDbContext;
        private readonly IMapper _mapper;

        public QuestionService(ApplicationDbContext applicationsDbContext, IMapper mapper)
        {
            _applicationsDbContext = applicationsDbContext;
            _mapper=mapper;
        }
        public PageResultDto<List<QuestionsDto>> GetAll(int PageIndex = 1, int PageSize = 30)
        {
            if (PageIndex < 1 || PageSize < 1)
            {
                return new PageResultDto<List<QuestionsDto>>
                {
                    Items = new List<QuestionsDto>(),
                    TotalItem = 0
                };
            }

            int skip = (PageIndex - 1) * PageSize;

            // Lấy tổng số lượng câu hỏi
            int totalItemCount = _applicationsDbContext.Questions.Count();

            // Lấy danh sách câu hỏi từ database có phân trang
            var questions = _applicationsDbContext.Questions
                .OrderByDescending(e => e.Content) // Sắp xếp theo nội dung hoặc trường khác
                .Skip(skip)
                .Take(PageSize)
                .ToList();

            // Sử dụng AutoMapper để map từ Question sang QuestionDTO
            var result = _mapper.Map<List<QuestionsDto>>(questions);

            // Trả về kết quả bao gồm danh sách câu hỏi và tổng số lượng câu hỏi
            return new PageResultDto<List<QuestionsDto>>
            {
                Items = result,
                TotalItem = totalItemCount
            };
        }

        public void CreateQuestion(CreateQuestionDto input)
        {
            var existQuestion = (from questions in _applicationsDbContext.Questions
                                   where questions.Content == input.Content
                                   select questions).FirstOrDefault();

            if (existQuestion != null)
            {
                throw new UserFriendlyException(" đã tồn tại");
            }

            var newQuestions = new Question
            {
                Content = input.Content,
                OptionA = input.OptionA,
                OptionB = input.OptionB,
                OptionC = input.OptionC,
                //OrderDate = input.OrderDate,
               
            };

            _applicationsDbContext.Questions.Add(newQuestions);
            _applicationsDbContext.SaveChanges();
        }
        public Question UpdateQuestion(int id, UpdateQuestionDto input)
        {
            // Tìm câu hỏi có id khớp
            var questionsExist = _applicationsDbContext.Questions
                .FirstOrDefault(q => q.Id == id);

            if (questionsExist == null)
            {
                throw new UserFriendlyException($"Không tìm thấy câu hỏi với id {id}");
            }

            // Kiểm tra xem Content mới có trùng với bất kỳ câu hỏi nào khác hay không, ngoại trừ câu hỏi hiện tại
            var isContentDuplicate = _applicationsDbContext.Questions
                .Any(q => q.Content == input.Content && q.Id != id);

            if (isContentDuplicate)
            {
                throw new UserFriendlyException("Nội dung câu hỏi đã tồn tại trong hệ thống");
            }

            // Cập nhật câu hỏi với nội dung mới nếu không có trùng lặp
            questionsExist.Content = input.Content;
            questionsExist.OptionA = input.OptionA;
            questionsExist.OptionB = input.OptionB;
            questionsExist.OptionC = input.OptionC;

            // Lưu thay đổi vào cơ sở dữ liệu
            _applicationsDbContext.SaveChanges();

            return questionsExist;
        }


        public void DeleteQuestion(int id)
        {
            var questionsExist = (from questions in _applicationsDbContext.Questions
                                   where questions.Id == id
                                   select questions).FirstOrDefault();

            if (questionsExist == null)
            {
                throw new UserFriendlyException($"Không tìm thấy câu có id {id}");
            }

            _applicationsDbContext.Questions.Remove(questionsExist);

            _applicationsDbContext.SaveChanges();

        }

        public Question GetQuestionById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
