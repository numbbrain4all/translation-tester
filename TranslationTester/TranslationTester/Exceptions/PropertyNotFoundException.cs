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

namespace TranslationTester.Exceptions
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  /// Description of PropertyNotFoundException.
  /// </summary>
  [Serializable]
  public class PropertyNotFoundException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyNotFoundException"/> class.
    /// </summary>
    public PropertyNotFoundException()
      : base()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The message for the exception.</param>
    public PropertyNotFoundException(string message)
      : base(message)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The message for the exception.</param>
    /// <param name="innerException">The inner exception to be wrapped.</param>
    public PropertyNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyNotFoundException"/> class.
    /// </summary>
    /// <param name="info"><see cref="SerializationInfo"/> for serializing the exception.</param>
    /// <param name="context">The context for the serialization.</param>
    protected PropertyNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
