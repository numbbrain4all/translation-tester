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
	[TestFixture]
	public class BasicTranslationTest
	{
		TypeTranslationTester<SimpleFrom,SimpleTo> target;
		SimpleFrom from;
		SimpleTo to;
		
		[SetUp]
		public void Setup()
		{
			target=new TypeTranslationTester<SimpleFrom,SimpleTo>();
			from=new SimpleFrom();
			var translator=new SinglePropertyMappedTranslator();
			to=translator.translate(from);
		}
	
		[Test]
		[Description(@"Having an unmapped property should cause the translation tester
			to throw an exception when running VerifyAllPropertiesMapped")]
		[ExpectedException(typeof(UnmappedPropertyException))]
		public void UnmappedPropertyCausesTestToFail()
		{
			target.VerifyAllPropertiesMapped();			
		}
		
		[Test]
		[Description(@"Having an unmapped property should mean that the AllPropertiesMapped
			property will be false")]
		public void AllPropertiesMappedFalseWhenUnmappedProperty()
		{
			Assert.That(target.AllPropertiesMapped==false);
		}	
		
		[Test]
		[Description(@"If a mapping is added for all properties AllPropertiesMapped should be true")]
		public void AllPropertiesMapped()
		{
		  target.AddMapping(f=>f.Property1,t=>t.Property1);
			Assert.That(target.AllPropertiesMapped);
		}		
	}
}
