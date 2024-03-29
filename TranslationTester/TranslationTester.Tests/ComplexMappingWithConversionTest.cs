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
using System;
using NUnit.Framework;

//Narrative:
//As a Developer
//I want to be able to be able to test that a property was mapped from one
//type to another
//So that I can map properties where the types do not match
//
//Acceptance Criteria:
//It should be possible to specify a complex mapping that is fulfilled
//if the translator uses the output of a conversion meth

namespace TranslationTester.Tests
{
  [TestFixture]
  public class ComplexMappingWithConversionTest
  {
    TypeTranslationTester<MultipleFrom,SimpleTo> target;
    MultipleFrom from;
    SimpleTo to;
    
    [SetUp]
    public void Setup(){
      target=new TypeTranslationTester<MultipleFrom, SimpleTo>();
      from=new MultipleFrom();
      to=new SimpleTo();
    }
    
    [Test]
    [Description(@"An int property is mapped to a string property through ToString()")]
    public void IntToString()
    {
      Func<MultipleFrom, SimpleTo, bool> match=(f,t)=>{
        return f.Property1==t.Property1;
      };
      
      target.AddMapping(f=>f.Property1,match);      
    }
  }
}
