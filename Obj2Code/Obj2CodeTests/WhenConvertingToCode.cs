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
            result.ShouldEqual("new EasyClass() { Id = 5, Name = \"Phil\", StartDate = DateTime.Parse(\"2009-06-15 13:45:30Z\"), };");
        }
    }
}
