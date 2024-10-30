﻿using API.Dtos.Exceptions;
using API.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected ILogger _logger;

        public ApiControllerBase(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult ReturnException(Exception ex)
        {
            if (ex is UserFriendlyException) 
            {
                var userEx = ex as UserFriendlyException; 
                return StatusCode(StatusCodes.Status400BadRequest, new ExceptionBody
                {
                    Message = userEx.Message
                });
            }
            _logger.LogError(ex, ex.Message); //log lại lỗi
            return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionBody
            {
                Message = ex.Message
            });
        }
    }
}
