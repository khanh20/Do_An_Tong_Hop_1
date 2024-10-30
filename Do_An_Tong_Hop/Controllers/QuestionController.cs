using API.Dtos.Questions;
using API.Dtos.Shared;
using API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _questionsService;
        public QuestionController(IQuestionService questionsService)
        {
            _questionsService = questionsService;

        }
        [HttpGet("get-all")]
        public IActionResult GetAll(int PageIndex = 1, int PageSize = 30)
        {
            // Gọi service để lấy danh sách câu hỏi và tổng số câu hỏi
            var result = _questionsService.GetAll(PageIndex, PageSize);

            // Trả về kết quả với status 200 OK, kèm theo dữ liệu phân trang
            return Ok(new PageResultDto<List<QuestionsDto>>
            {
                Items = result.Items,
                TotalItem = result.TotalItem
            });
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateQuestionDto input)
        {
            try
            {
                _questionsService.CreateQuestion(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody] UpdateQuestionDto input)
        {
            try
            {
                return Ok(_questionsService.UpdateQuestion(id, input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            try
            {
                _questionsService.DeleteQuestion(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
