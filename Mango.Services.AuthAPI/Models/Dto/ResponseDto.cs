﻿namespace Mango.Services.AuthAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Response { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = "";
    }
}
