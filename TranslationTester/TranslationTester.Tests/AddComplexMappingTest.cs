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
//I want to be able to specify complex translations where necessary
//So that as much as possible of the translation can be tested
//
//Acceptance Criteria:
//
//Scenario 1: Property exists
//Given that the property exists
//  And the property has not been excluded
//When  the Developer adds a complex mapping
//Then  the test for 'AllPropertiesMapped' will not fail on this property
//  And the mapping should be available for testing.
//
//Scenario 2: Property does not exist
//Given that the specified property does not exist on the 'From' class
//When the Developer tries to add the complex mapping
//Then the addition should fail
//  And the property name should be shown
//
//Scenario 3: Property already mapped
//Given that the specified property exists
// And the property already has a mapping
//When the Developer tries to add the complex mapping
//Then the addition should succeed
//  And the mapping should be available for testing (One property could be
//mapped to multiple outcomes)


namespace TranslationTester.Tests
{
  [TestFixture]
  public class AddComplexMappingTest
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
    [Description(@"Scenario 1: From property exists, doesn't throw")]
    public void FromPropertyExists()
    {
      Func<MultipleFrom, SimpleTo, bool> match=(f,t)=>{
        return true;
      };
      
      target.AddMapping("Property1",match);
    }
    
    [Test]
    [Description(@"Scenario 1: From property exists, a mapping should be returned")]
    public void FromPropertyExistsMappingReturned()
    {
      Func<MultipleFrom, SimpleTo, bool> match=(f,t)=>{
        return true;
      };
      
      var actual=  target.AddMapping("Property1",match);
      Assert.That(actual,Is.Not.Null);
    }
    
    [Test]
    [Description(@"Scenario 1: The mapping that is returned should contain the match function")]
    public void MappingReturnedContainsMatchFunction()
    {
      Func<MultipleFrom, SimpleTo, bool> match=(f,t)=>{
        return true;
      };
      
      var actual=  target.AddMapping("Property1",match);
      Assert.That(actual.MatchFunction,Is.EqualTo(match));
    }
  }
}