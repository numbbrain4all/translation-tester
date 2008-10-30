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
  //Narrative:
  //As a Developer
  //I want to be able to verify that a translator fulfills a specific simple
  //mapping
  //So that I have confidence that the translator fulfills its specification
//
  //Acceptance Criteria:
//
  //Scenario 1: Simple mapping fulfilled
  //Given that a single simple mapping has been added
  //And the translator fulfills the mapping
  //When  VerifyAllMappings is called
  //Then  the method call should return sucessfully
//
  //Scenario 2: Simple mapping not fulfilled
  //Given that a single simple mapping has been added
  //And the translator does not fulfill the mapping
  //When  VerifyAllMappings is called
  //Then  the method call should throw an exception
  //And the exception should detail the mapping that failed
//
  //Scenario 3: Multiple simple mappings all fulfilled
  //Given that two simple mapping has been added
  //And the translator does fulfills both mappings
  //When  VerifyAllMappings is called
  //Then  the method call should return sucessfully
//
  //Scenario 4: Multiple simple mappings 1 not fulfilled
  //Given that two simple mapping has been added
  //And the translator fulfills only 1 mapping
  //When  VerifyAllMappings is called
  //Then  the method call should throw an exception
  //And the exception should detail the mapping that failed
//
//
  //Scenario 5: Multiple simple mappings multiple not fulfilled
  //Given that two simple mapping has been added
  //And the translator fulfills neither mapping
  //When  VerifyAllMappings is called
  //Then  the method call should throw an exception
  //And the exception should detail the mappings that failed

  
  [TestFixture]
  public class VerifySimpleMappingsTest
  {
    TypeTranslationTester<MultipleFrom,SimpleTo> target;
    MultipleFrom from;
    
    [SetUp]
    public void Setup(){
      target=new TypeTranslationTester<MultipleFrom,SimpleTo>();
      from=new MultipleFrom{
        Property1=1,
      };
    }
    
    [Test]
    [Description(@"If no translation method is specified for the first overload
      then an exception should be thrown")]
    public void NoTranslationMethodProvidedThrows()
    {
      target.AddMapping("Property1","Property1");
      var actual= Assert.Throws<InvalidOperationException>(()=>target.VerifyAllMappings(from));
    }
    
    [Test]
    [Description(@"If no translation method is specified but the 2nd overload is used
      then no exception should be thrown")]
    public void NoTranslationMethodUseOverloadSucceeds()
    {
      target.AddMapping("Property1","Property1");
      var to =new SimpleTo{
        Property1=from.Property1
      };
      target.VerifyAllMappings(from,to);
    }
    
    [Test]
    [Description(@"Scenario 1: A single mapping is fullfilled")]
    public void SimpleMappingFulfilled()
    {
      target.AddMapping("Property1","Property1");
      
      target.TranslationMethod=(val)=>
      {
        return new SimpleTo{
          Property1=val.Property1
        };
      };
      target.VerifyAllMappings(from);
    }
    
    [Test]
    [Description(@"Scenario 2: When VerifyAllMappings is called for
      a single mapping that is unfullfilled an exception should be thrown")]
    public void SingleUnfulfilledMappingThrows()
    {
      target.AddMapping("Property1","Property1");
      
      target.TranslationMethod=(val)=>
      {
        return new SimpleTo();
      };
      Assert.Throws<MappingFailedException>(()=>target.VerifyAllMappings(from));
    }
    
    [Test]
    [Description(@"Scenario 2: The unfullfilled exception should contains the
      mapping that failed")]
    public void SingleUnfulfilledMappingExceptionContainsMappingString()
    {
      var mapping=  target.AddMapping("Property1","Property1");

      target.TranslationMethod=(val)=>
      {
        return new SimpleTo();
      };
      var actual= Assert.Throws<MappingFailedException>(()=>target.VerifyAllMappings(from));
      Assert.That(actual.Message,Text.Contains(mapping.ToString()));
    }  
    
    [Test]
    [Description(@"Scenario 3: Two simple mappings are specified and fullfilled")]
    public void MultipleMappingFulfilled()
    {
      target.AddMapping("Property1","Property1");
      target.AddMapping("StringProp","StringProperty");
      from=new MultipleFrom{
        Property1=1,
        StringProp="string"
      };
      target.TranslationMethod=(val)=>
      {
        return new SimpleTo{
          Property1=val.Property1,
          StringProperty=val.StringProp
        };
      };
      target.VerifyAllMappings(from);
    }
    
    [Test]
    [Description(@"Scenario 4: If multiple mappings and one fails then the failing
      mapping should be shown in the exception message")]
    public void TwoMappingsOneFulfilled()
    {
      target.AddMapping("Property1","Property1");
      var unfulfilled=  target.AddMapping("StringProp","StringProperty");
      from=new MultipleFrom{
        Property1=1,
        StringProp="string"
      };
      target.TranslationMethod=(val)=>
      {
        return new SimpleTo{
          Property1=val.Property1,
        };
      };
      var actual= Assert.Throws<MappingFailedException>(()=>target.VerifyAllMappings(from));
      Assert.That(actual.Message,Text.Contains(unfulfilled.ToString()));
    }
    
    [Test]
    [Description(@"Scenario 4: If multiple mappings and both fail then the failing
      mappings should be shown in the exception message")]
    public void TwoMappingsBothUnfulfilled()
    {
      var unfulfilled1= target.AddMapping("Property1","Property1");
      var unfulfilled2= target.AddMapping("StringProp","StringProperty");
      from=new MultipleFrom{
        Property1=1,
        StringProp="string"
      };
      target.TranslationMethod=(val)=>
      {
        return new SimpleTo{
        };
      };
      var actual= Assert.Throws<MappingFailedException>(()=>target.VerifyAllMappings(from));
      Assert.That(actual.Message,Text.Contains(unfulfilled1.ToString()));
      Assert.That(actual.Message,Text.Contains(unfulfilled2.ToString()));
    }
  }
}
