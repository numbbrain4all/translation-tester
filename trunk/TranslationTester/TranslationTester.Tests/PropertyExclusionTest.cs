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
//I want to specify that a property should not be translated
//So that I can have minimal classes
//
//Acceptance Criteria:
//
//Scenario 1: Property exists
//Given that the specified property exists on the 'From' class
//When the Developer tries to exclude the property
//Then the test for 'AllPropertiesMapped' will not fail on this property
//
//Scenario 2: Property does not exist
//Given that the specified property does not exist on the 'From' class
//When the Developer tries to exclude the property
//Then the exclusion should fail
// And the property name should be shown
//
//Scenario 3: Property already mapped
//Given that the specified property exists
// And the property already has a mapping
//When the Developer tries to exclude the property
//Then the exclusion should fail
// And the property name should be shown

namespace TranslationTester.Tests
{
  [TestFixture]
  public class PropertyExclusionTest
  {
    TypeTranslationTester<SimpleFrom,SimpleTo> target;
    SimpleFrom from;
    SimpleTo to;
    
    [SetUp]
    public void Setup(){
      target=new TypeTranslationTester<SimpleFrom,SimpleTo>();
      from=new SimpleFrom();
      var translator=new SinglePropertyMappedTranslator();
      to=translator.translate(from);
    }
    
    [Test]
    [Description(@"Scenario 1: Excluding a property should make
      VerifyAllPropertiesMapped not fail on the property")]
    public void PropertyExists()
    {
      target.ExcludeProperty("Property1");
      target.VerifyAllPropertiesMapped();
    }
    
    [Test]
    [Description(@"Scenario 2: If attempt to exclude a property that
      does not exist then a PropertyNotFoundException should be thrown")]
    public void PropertyDoesNotExist()
    {
      Assert.Throws<PropertyNotFoundException>(()=>target.ExcludeProperty("Invalid"));
    }
    
    [Test]
    [Description(@"Scenario 2: If attempt to exclude a property that
      does not exist then a PropertyNotFoundException should be thrown with the property name in it")]
    public void PropertyDoesNotExistExceptionContainsPropertyName()
    {
      var propertyName="Invalid";
      var actual = Assert.Throws<PropertyNotFoundException>(()=>target.ExcludeProperty(propertyName));
      Assert.That(actual.Message,Text.Contains(propertyName));
    }
    
    [Test]
    [Description(@"Scenario 3: If attempt to exclude a property that
      is already mapped then a PropertyAlreadyMappedException should be thrown with the property name in it")]
    public void PropertyAlreadyMappedExceptionContainsPropertyName()
    {
      var propertyName="Property1";//valid
      target.AddMapping(propertyName,"Property1");
      var actual = Assert.Throws<PropertyAlreadyMappedException>(()=>target.ExcludeProperty(propertyName));
      Assert.That(actual.Message,Text.Contains(propertyName));
    }
    
    [Test]
    [Description(@"If attempt to exclude a property that is already excluded
      then a PropertyAlreadyMappedException should be thrown with the property name in it")]
    public void PropertyAlreadyExcludedExceptionContainsPropertyName()
    {
      var propertyName="Property1";//valid
      target.ExcludeProperty(propertyName);
      var actual = Assert.Throws<PropertyAlreadyMappedException>(()=>target.ExcludeProperty(propertyName));
      Assert.That(actual.Message,Text.Contains(propertyName));
    }
    
    
  }
}
