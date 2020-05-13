using Obj2CodeTests.TestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Obj2Code;
using Should;

namespace Obj2CodeTests
{
    public class WhenConvertingToCode
    {
        [Fact]
        public void Should_Convert_To_Code()
        {
            var easyClass = new EasyClass() { Id = 5, Name = "Phil", StartDate = DateTime.Parse("2009-06-15 13:45:30Z"), };
            var result = Obj2Coder.ToCode(easyClass);
            result.ShouldEqual("new Obj2CodeTests.TestClasses.EasyClass() { Id = 5, Name = \"Phil\", StartDate = DateTime.Parse(\"2009-06-15 13:45:30Z\"), }");
        }

        [Fact]
        public void Should_Convert_Short_To_Code()
        {
            var easyClass = new ShortEasyClass() { Id = 5, Name = "Phil", StartDate = DateTime.Parse("2009-06-15 13:45:30Z"), };
            var result = Obj2Coder.ToCode(easyClass);
            result.ShouldEqual("new Obj2CodeTests.TestClasses.ShortEasyClass() { Id = 5, Name = \"Phil\", StartDate = DateTime.Parse(\"2009-06-15 13:45:30Z\"), }");
        }

        [Fact]
        public void Should_Restore_Date()
        {
            var expectedDate = "2009-06-15 13:45:30Z";
            DateTime dt = DateTime.Parse(expectedDate);
            var resultDate = dt.ToUniversalTime().ToString("u");
            expectedDate.ShouldEqual(resultDate);
        }
    }
}
