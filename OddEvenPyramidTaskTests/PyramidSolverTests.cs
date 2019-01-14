using OddEvenPyramidTask;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OddEvenPyramidTaskTests
{
    public class PyramidSolverTests
    {

        private readonly PyramidSolver _classUnderTest;

        public PyramidSolverTests()
        {
            _classUnderTest = new PyramidSolver();
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_Input_String_Null_Expected_Null()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath(null);
            Assert.Null(result);
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_Input_String_Empty_Expected_Null()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath("");
            Assert.Null(result);
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_Input_String_Space_Expected_Null()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath(" ");
            Assert.Null(result);
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_Input_String_Has_Non_Digits_Expected_Null()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath(@"25
                                                                        2a 29");
            Assert.Null(result);
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_All_Digits_Are_Odd_Expected_Null()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath(@"25
                                                                        27 29");
            Assert.Null(result);
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_All_Digits_Are_Even_Expected_Null()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath(@"26
                                                                        30 32");
            Assert.Null(result);
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_Missing_Link_Expected_Null()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath(@"26
                                                                        30 32
                                                                        31 45 37");
            Assert.Null(result);
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_Largest_Path_Has_Missing_Link_Expected_Null()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath(@"3000
                                                                        30 30
                                                                        31 45 37");
            Assert.Null(result);
        }

        [Fact]
        public void GetPyramidOddEvenLongestPath_Valid_Path_Expected_Valid_Result()
        {
            var result = _classUnderTest.GetPyramidOddEvenLongestPath(@"  1
                                                                           8 9
                                                                         1 5 9
                                                                          4 5 2 3");
            Assert.NotNull(result);
            Assert.Equal(16, result.MaxSum);
            Assert.Equal("1 8 5 2", result.Path);
        }
    }
}
