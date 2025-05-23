﻿using GameReviews.Domain.Results;
using MediatR;

namespace GameReviews.Application.Common.Interfaces.Query;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;