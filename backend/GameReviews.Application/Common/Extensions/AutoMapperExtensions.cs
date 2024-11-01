using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Application.Common.Extensions;

public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDestination> MemberMapFrom<TMember,TSourceMember, TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> mapping,
        Expression<Func<TDestination, TMember>> destExpression,
        Expression<Func<TSource, TSourceMember>> sourceExpression)
    {
        mapping.ForMember(destExpression, opt => opt.MapFrom(sourceExpression));

        return mapping;
    }
}
