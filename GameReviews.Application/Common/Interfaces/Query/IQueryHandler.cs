﻿using GameReviews.Domain.Common.Result;
using MediatR;

namespace GameReviews.Application.Common.Interfaces.Query;
internal interface IQueryHandler<in TRequest,TResponse> : IRequestHandler<TRequest,Result<TResponse>> 
    where TRequest : IQuery<TResponse>;