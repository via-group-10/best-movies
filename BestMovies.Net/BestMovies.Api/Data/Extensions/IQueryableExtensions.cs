using BestMovies.Api.Models.Filters;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BestMovies.Api.Data.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> query, FilterBase filter)
    {
        return query.Skip(filter.Offset).Take(filter.Limit);
    }
}
public static class MyQueryableExtensions
{
    public static IQueryable<TEntity> RemoveOrdering<TEntity>(
        [NotNull] this IQueryable<TEntity> source)
        where TEntity : class
    {
        return source.Provider is EntityQueryProvider
            ? source.Provider.CreateQuery<TEntity>(new RemoveOrderingExpression(source.Expression))
            : source;
    }
}

public class RemoveOrderingExpression : Expression, IPrintableExpression
{
    public Expression Source { get; }
    public sealed override ExpressionType NodeType => ExpressionType.Extension;
    public override Type Type => typeof(RemoveOrderingExpression);

    public RemoveOrderingExpression(Expression source)
    {
        Source = source;
    }

    protected override Expression VisitChildren(ExpressionVisitor visitor)
    {
        var source = visitor.Visit(Source);

        if (source is ShapedQueryExpression sqe &&
            sqe.QueryExpression is SelectExpression se &&
            0 < se.Orderings.Count)
        {
            se.ClearOrdering();
            return source;
        }

        return new RemoveOrderingExpression(source);
    }

    /// <inheritdoc />
    void IPrintableExpression.Print(ExpressionPrinter expressionPrinter)
    {
        expressionPrinter.AppendLine($"{nameof(RemoveOrderingExpression)}(");
        using (expressionPrinter.Indent())
        {
            expressionPrinter.Visit(Source);
            expressionPrinter.AppendLine(")");
        }
    }
}
