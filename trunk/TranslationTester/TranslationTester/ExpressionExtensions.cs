#region License
// Copyright (c) 2008, Alex McMahon
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
//  * Redistributions of source code must retain the above copyright notice,
// this list of conditions and the following disclaimer.
//  * Redistributions in binary form must reproduce the above copyright notice,
// this list of conditions and the following disclaimer in the documentation
// and/or other materials provided with the distribution.
//  * Neither the name of Alex McMahon nor the names of its
// contributors may be used to endorse or promote products derived from this
// software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

namespace TranslationTester
{
  using System;
  using System.Globalization;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Reflection;

  /// <summary>
  /// Extension methods for Expressions.
  /// </summary>
  public static class ExpressionExtensions
  {
    /// <summary>
    /// Converts this expression to the property name the expression represents.
    /// </summary>
    /// <param name="propertyExpression">The expression.</param>
    /// <typeparam name="TFrom">The type containing the property.</typeparam>
    /// <typeparam name="TProp">The type of the property.</typeparam>
    /// <returns>The property name.</returns>
    /// <exception cref="ArgumentException">Thrown when the expression is not a property.</exception>
    public static string ToPropertyName<TFrom, TProp>(this Expression<Func<TFrom, TProp>> propertyExpression)
    {
      var lambda = ToLambda(propertyExpression);
      var prop = lambda.Body as MemberExpression;

      if (prop != null)
      {
        var info = prop.Member as PropertyInfo;
        if (info != null)
        {
          return info.Name;
        }
      }
      
      throw new ArgumentException(string.Format(
        CultureInfo.CurrentCulture,
        Properties.Resources.ErrorNotPropertyExpression,
        propertyExpression));
    }
    
    /// <summary>
    /// Converts this Expression into a LambdaExpression, removing any cast if present.
    /// </summary>
    /// <param name="expression">The <see cref="Expression"/> to convert.</param>
    /// <returns>The <see cref="LambdaExpression"/>.</returns>
    private static LambdaExpression ToLambda(Expression expression)
    {
      var lambda = expression as LambdaExpression;
      if (lambda == null)
      {
        throw new ArgumentException(string.Format(
          CultureInfo.CurrentCulture,
          Properties.Resources.ErrorExpressionNotSupported,
          expression));
      }
      
      var convertLambda = lambda.Body as UnaryExpression;
      if (convertLambda != null
          && convertLambda.NodeType == ExpressionType.Convert)
      {
        lambda = Expression.Lambda(convertLambda.Operand, lambda.Parameters.ToArray());
      }
      
      return lambda;
    }
  }
}
