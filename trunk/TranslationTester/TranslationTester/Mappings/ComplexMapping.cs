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
  
  /// <summary>
  /// Represents an arbitrary mapping from one property.
  /// </summary>
  /// <typeparam name="TFrom">The type being translated from.</typeparam>
  /// <typeparam name="TTo">The type being translated to.</typeparam>
  public class ComplexMapping<TFrom, TTo> : AbstractMapping<TFrom, TTo>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ComplexMapping{TFrom, TTo}" /> class.
    /// </summary>
    /// <param name="fromProperty">The property on the 'From' type.</param>
    /// <param name="matchFunction">The function that should be evaluated to determine whether the
    /// mapping was fulfilled.</param>
    public ComplexMapping(
      string fromProperty,
      Func<TFrom, TTo, bool> matchFunction)
      : base(fromProperty)
    {
      this.MatchFunction = matchFunction;
    }
    
    /// <summary>
    /// Gets the function that should be evaluated to determine whether the
    /// mapping was fulfilled.
    /// </summary>
    /// <value>The function that should be evaluated to determine whether the
    /// mapping was fulfilled.</value>
    public Func<TFrom, TTo, bool> MatchFunction
    {
      get;      
      private set;    
    }
    
    /// <summary>
    /// Determines whether this mapping was fulfilled based on the from and to instances (post translation).
    /// </summary>
    /// <param name="fromInstance">The instance of the 'From' class that exercised the translation.</param>
    /// <param name="toInstance">The instance of the 'To' class that exercised the translation.</param>
    /// <returns>True if the mapping was fulfilled, false otherwise.</returns>
    public override bool Evaluate(TFrom fromInstance, TTo toInstance)
    {
      return MatchFunction(fromInstance, toInstance);
    }
  }
}
