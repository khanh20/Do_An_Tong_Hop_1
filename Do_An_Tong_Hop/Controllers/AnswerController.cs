using API.Dtos.Answer;
using API.Dtos.Questions;
using API.Services.Abstract;
using API.Services.Implement;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AnswerController : ControllerBase
    {
        private IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;

        }
        [HttpGet("get-all")]
        public IActionResult GetAll(int PageIndex = 1, int PageSize = 30)
        {
            return Ok(_answerService.GetAll(PageIndex, PageSize));
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateAnswerDto input)
        {
            try
            {
                _answerService.CreateAnswer(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
