﻿// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System.Data.SqlClient;
using System.Linq;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using Test.Helpers;
using Test.UnitTests.TestSupport;
using TestSupport.EfHelpers;
using TestSupport.Helpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests.DataLayer
{
    public class TestApplyScriptExtension

    {
        private readonly ITestOutputHelper _output;

        public TestApplyScriptExtension(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestApplyScriptOneCommandToDatabaseOk()
        {
            //SETUP
            var options = this.CreateUniqueClassOptions<EfCoreContext>();
            var filepath = TestFileHelpers.GetTestDataFilePath("Script01 - Add row to Authors table.sql");
            using (var context = new EfCoreContext(options))
            {
                context.CreateEmptyViaWipe();

                //ATTEMPT
                context.ExecuteScriptFileInTransaction(filepath);

                //VERIFY
                context.Authors.Count().ShouldEqual(1);
            }
        }

        [Fact]
        public void TestApplyScriptTwoCommandsToDatabaseOk()
        {
            //SETUP
            var options = this.CreateUniqueClassOptions<EfCoreContext>();
            var filepath = TestFileHelpers.GetTestDataFilePath("Script02 - Add two rows to Authors table.sql");
            using (var context = new EfCoreContext(options))
            {
                context.CreateEmptyViaWipe();

                //ATTEMPT
                context.ExecuteScriptFileInTransaction(filepath);

                //VERIFY
                context.Authors.Count().ShouldEqual(2);
            }
        }
    }
}