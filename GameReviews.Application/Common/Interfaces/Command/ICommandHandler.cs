﻿using MediatR;

namespace GameReviews.Application.Common.Interfaces.Command;

public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest> 
    where TRequest : ICommand;
public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>;
