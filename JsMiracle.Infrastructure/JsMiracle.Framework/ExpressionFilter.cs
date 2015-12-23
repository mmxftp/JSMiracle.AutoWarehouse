using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JsMiracle.Framework
{
    /// <summary>
    /// 扩展所有IEnumerable集合的处理方法
    /// </summary>
    public static class ExpressionFilterExtension
    {
        public static IEnumerable<T> WhereFilter<T>(
           this IEnumerable<T> data,
           IEnumerable<ExpressGroup> expGroup
           )
        {
            var param = Expression.Parameter(typeof(T));

            Expression expressionBody = null;

            if (expGroup == null)
                return data;

            foreach (var grp in expGroup)
            {
                if (grp.ExpParameters == null)
                    continue;

                Expression expGroupBody = null;

                foreach (var par in grp.ExpParameters)
                {
                    var expAnd = Expression.Equal(Expression.Property(param, par.FieldName),
                        Expression.Constant(par.FieldValue, par.FieldType));

                    UpdateExpress(par.ExpType, expAnd, ref expGroupBody);
                }

                UpdateExpress(grp.ExpType, expGroupBody, ref expressionBody);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(expressionBody, param);
            return data.Where(lambda.Compile());
        }


        static void UpdateExpress(ExpressionType eType, Expression sourceExp, ref Expression targetExp)
        {
            if (targetExp == null)
            {
                targetExp = sourceExp;
                return;
            }

            switch (eType)
            {
                case ExpressionType.And:
                    targetExp = Expression.Add(sourceExp, targetExp);
                    break;

                case ExpressionType.Or:
                    targetExp = Expression.Or(sourceExp, targetExp);
                    break;
            }

        }
    }


    public class ExpressGroup
    {
        public IList<ExpressParameters> ExpParameters { get; private set; }

        public void AddExp(string name, object value, ExpressionType expType = ExpressionType.Equal)
        {
            if (ExpParameters == null)
                ExpParameters = new List<ExpressParameters>();

            ExpParameters.Add(new ExpressParameters(name, value, expType));
        }

        public ExpressionType ExpType { get; set; }

        public ExpressGroup(ExpressionType expType)
        {
            this.ExpType = expType;
        }


    }


    public class ExpressParameters
    {
        public string FieldName { get; set; }

        public Type FieldType { get; set; }

        public object FieldValue { get; set; }

        public ExpressionType ExpType { get; set; }

        //public ExpressParameters() { }

        public ExpressParameters(string name, object value, ExpressionType expType = ExpressionType.Equal)
        {
            this.FieldName = name;
            this.FieldType = value.GetType();
            this.FieldValue = value;
            this.ExpType = expType;
        }

    }
}
