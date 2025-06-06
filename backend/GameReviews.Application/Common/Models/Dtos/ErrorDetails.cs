﻿namespace GameReviews.Application.Common.Models.Dtos;
public class ErrorDetails
{
    public string Message { get; set; }

    public IDictionary<string, string[]>? Errors { get; set; } = null;

    public ErrorDetails(string message, IDictionary<string, string[]>? errors = null)
    {
        Message = message;
        Errors = errors;
    }
}

