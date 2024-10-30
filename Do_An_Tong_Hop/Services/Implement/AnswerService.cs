using API.DbContexts;
using API.Dtos.Answer;
using API.Dtos.Questions;
using API.Entities;
using API.Exceptions;
using API.Services.Abstract;
using AutoMapper;

namespace API.Services.Implement
{
    public class AnswerService : IAnswerService
    {
        private ApplicationDbContext _applicationsDbContext;
        private readonly IMapper _mapper;

        public AnswerService(ApplicationDbContext applicationsDbContext, IMapper mapper)
        {
            _applicationsDbContext = applicationsDbContext;
            _mapper=mapper;
        }
        public List<AnswerDto> GetAll(int PageIndex = 1, int PageSize = 30)
        {
            // Kiểm tra PageIndex và PageSize hợp lệ
            if (PageIndex < 1 || PageSize < 1)
            {
                return new List<AnswerDto>(); // Trả về danh sách rỗng nếu không hợp lệ
            }

            // Tính toán số lượng items cần bỏ qua
            int skip = (PageIndex - 1) * PageSize;

            // Lấy danh sách Answer từ database
            var answers = _applicationsDbContext.Answers
                .OrderByDescending(e => e.Id) // Sắp xếp theo Id giảm dần
                .Skip(skip) // Bỏ qua số lượng items tương ứng
                .Take(PageSize) // Lấy số lượng items theo PageSize
                .ToList(); // Chuyển kết quả sang danh sách
            var result = _mapper.Map<List<AnswerDto>>(answers);

            return result;
        }

        public Answer GetQuestionById(int id)
        {
            throw new NotImplementedException();
        }
        public void CreateAnswer(CreateAnswerDto input)
        {
            var existingAnswer = _applicationsDbContext.Answers
                    .FirstOrDefault(a => a.QuestionId == input.QuestionId && a.StudentId == input.StudentId);


            if (existingAnswer != null)
            {
                // Nếu đã tồn tại, không cho phép thêm mới
                throw new UserFriendlyException("Sinh viên này đã trả lời câu hỏi.");
            }

            var answer = new Answer
            {
                QuestionId = input.QuestionId,
                StudentId = input.StudentId,
                AnswerContent = input.AnswerContent
            };
            _applicationsDbContext.Answers.Add(answer);
            _applicationsDbContext.SaveChanges();
        }

    }
}
