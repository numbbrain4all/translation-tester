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

namespace TranslationTester.Tests
{
  //  Narrative:
  //As a Developer
  //I want to be informed if the provided from instance will not be sufficient
  //to verify the mappings
  //So that I can fix the test to ensure all mappings are fully verified
//
  //Acceptance Criteria:
//
  //Scenario 1: Value type property not set
  //Given that the from type has a value type propery (e.g. int)
  //  And a mapping is specified for this property
  //  And the from instance does not assign a value to this property
  //When  VerifyFromInstance is called
  //Then  An exception should be thrown
  //  And the exception should contain the property name that was not assigned
//
  //Scenario 2: Reference type property not set
  //Given that the from type has a reference type propery (e.g. an instance of
  //another class)
  //  And a mapping is specified for this property
  //  And the from instance does not assign a value to this property
  //When  VerifyFromInstance is called
  //Then  An exception should be thrown
  //  And the exception should contain the property name that was not assigned
//
  //Scenario 2: Reference type property not set
  //Given that the from type has a reference type propery (e.g. an instance of
  //another class)
  //  And a mapping is specified for this property
  //  And the from instance does not assign a value to this property
  //When  VerifyFromInstance is called
  //Then  An exception should be thrown
  //  And the exception should contain the property name that was not assigned
//
  //Scenario 3: string type property not set
  //Given that the from type has a string propery
  //  And a mapping is specified for this property
  //  And the from instance does not assign a value to this property
  //When  VerifyFromInstance is called
  //Then  An exception should be thrown
  //  And the exception should contain the property name that was not assigned
//
  //Scenario 4: Multiple properties not set
  //Given that the from type has multiple properties
  //  And a mapping is specified for multiple properties
  //  And the from instance does not assign a value to these properties
  //When  VerifyFromInstance is called
  //Then  An exception should be thrown
  //  And the exception should contain the names of all the properties that
  //were not assigned
//
  //Scenario 5: VerifyAllMappings also verifies from instance
  //Given that the from instance has mapped, unassigned properties
  //When  VerifyAllMappings is called
  //Then  An exception should be thrown
//
  //Scenario 6: struct type property not set
  //Given that the from type has a struct propery
  //  And a mapping is specified for this property
  //  And the from instance does not assign a value to this property
  //When  VerifyFromInstance is called
  //Then  An exception should be thrown
  //  And the exception should contain the property name that was not assigned

  
  
  [TestFixture]
  public class VerifyFromInstanceTest
  {
    TypeTranslationTester<MultipleFrom,SimpleTo> target;
    MultipleFrom from;
    
    [SetUp]
    public void Setup(){
      target=new TypeTranslationTester<MultipleFrom,SimpleTo>();
      from=new MultipleFrom{
      };
    }
    
    [Test]
    [Description(@"Scenario 1: An unassigned value type should cause an argument exception
      as it will be impossible to verify the mapping of this property")]
    public void ValueTypeNotSetThrows()
    {
      target.AddMapping("Property1","Property1");
      
      var actual= Assert.Throws<ArgumentException>(()=>target.VerifyFromInstance(from));
    }
    
    [Test]
    [Description(@"Scenario 3: strings can be tricky, worth their own test")]
    public void StringTypeNotSetThrows()
    {
      target.AddMapping("StringProp","StringProperty");
      
      var actual= Assert.Throws<ArgumentException>(()=>target.VerifyFromInstance(from));
    }
    
    [Test]
    [Description(@"Scenario 2: reference types default to null")]
    public void ReferenceTypeNotSetThrows()
    {
      target.AddMapping("RefProp","RefProperty");
      
      var actual= Assert.Throws<ArgumentException>(()=>target.VerifyFromInstance(from));
    }
    
    [Test]
    [Description(@"Scenario 4: multiple unset properties should be reported together")]
    public void MultipleUnsetPropertiesReported()
    {
      var fromProp1="StringProp";
      var fromProp2="Property1";
      target.AddMapping(fromProp1,"StringProperty");
      target.AddMapping(fromProp2,"Property1");
      
      var actual= Assert.Throws<ArgumentException>(()=>target.VerifyFromInstance(from));
      Assert.That(actual.Message,Text.Contains(fromProp1));
      Assert.That(actual.Message,Text.Contains(fromProp2));
    }
  }
}
