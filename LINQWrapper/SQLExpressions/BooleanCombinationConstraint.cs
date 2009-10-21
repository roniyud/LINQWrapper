﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LINQWrapper.SQLExpressions
{
    /// <summary>
    /// A row constraint made up of a combination of other constraints, combined with a single
    /// boolean operator (AND or OR)
    /// </summary>
    internal class BooleanCombinationConstraint : Constraint
    {
        public BooleanCombinationConstraint(ExpressionType type)
        {
            if ((type != ExpressionType.And)
                && (type != ExpressionType.Or))
            {
                throw new ArgumentOutOfRangeException("Constraint combination type must be AND or OR");
            }

            this.type = type;
            this.childConstraints = new List<Constraint>();
        }

        #region Constraint Members

        public void BuildExpression(StringBuilder builder)
        {
            if (childConstraints.Count == 0)
            {
                // If we combine a boolean operator zero times, we expect to get the identity 
                // element under that boolean operation.

                if (type == ExpressionType.Or)
                {
                    builder.Append(" 0 ");
                }
                else if (type == ExpressionType.And)
                {
                    builder.Append(" 1 ");
                }
            }
        }

        #endregion

        #region Private data members

        private List<Constraint> childConstraints;
        private ExpressionType type;

        #endregion
    }
}
