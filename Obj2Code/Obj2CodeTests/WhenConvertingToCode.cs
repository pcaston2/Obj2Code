using Obj2CodeTests.TestClasses;
using System;
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
        public void Should_Convert_Boolean_To_Code()
        {
            var easyClass = new BooleanEasyClass() { IsActive = true, };
            var result = Obj2Coder.ToCode(easyClass);
            result.ShouldEqual("new Obj2CodeTests.TestClasses.BooleanEasyClass() { IsActive = True, }");
        }

        [Fact]
        public void Should_Convert_Nullable_Boolean_To_Code()
        {
            var easyClass = new NullableBooleanEasyClass() { IsActive = true, };
            var result = Obj2Coder.ToCode(easyClass);
            result.ShouldEqual("new Obj2CodeTests.TestClasses.NullableBooleanEasyClass() { IsActive = True, }");
        }

        [Fact]
        public void Should_Convert_Nullable_Int_To_Code()
        {
            var easyClass = new NullableIntEasyClass () { Count = 5, };
            var result = Obj2Coder.ToCode(easyClass);
            result.ShouldEqual("new Obj2CodeTests.TestClasses.NullableIntEasyClass() { Count = 5, }");
        }

        [Fact]
        public void Should_Convert_Nullable_DateTime_To_Code()
        {
            var easyClass = new NullableDateEasyClass() { StartDate = null, };
            var result = Obj2Coder.ToCode(easyClass);
            result.ShouldEqual("new Obj2CodeTests.TestClasses.NullableDateEasyClass() { StartDate = null, }");
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
