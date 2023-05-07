using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification()
        {
        }
        
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            if(Criteria is null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }
            
            Criteria = criteria;
        }
        
        public Expression<Func<T, bool>>? Criteria { get; }

        public IList<Expression<Func<T, object>>> Includes { get; } = 
            new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
