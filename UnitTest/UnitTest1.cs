using SF2022User01Lib;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Тест рассчета времени
        /// </summary>
        [TestMethod]
        public void TestStandard()
        {
            TimeSpan[] times = new[] {
                new TimeSpan(10, 00, 0),
                new TimeSpan(11, 00, 0),
                new TimeSpan(15, 00, 0),
                new TimeSpan(15, 30, 0),
                new TimeSpan(16, 50, 0)
            };

            int[] durations = new[]
            {
                60,
                30,
                10,
                10,
                40
            };

            TimeSpan startWorkingTime = new TimeSpan(08, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(18, 00, 00);
            int consultationTime = 30;

            var result = Calculations.AvailablePeriods(times, durations, startWorkingTime, endWorkingTime, consultationTime);

            string[] expected = new[] {
                "08:00-08:30",
                "08:30-09:00",
                "09:00-09:30",
                "09:30-10:00",
                "11:30-12:00",
                "12:00-12:30",
                "12:30-13:00",
                "13:00-13:30",
                "13:30-14:00",
                "14:00-14:30",
                "14:30-15:00",
                "15:40-16:10",
                "16:10-16:40",
                "17:30-18:00"
            };

  
            Console.WriteLine(string.Join("\n", result));
            CollectionAssert.AreEqual(expected, result);
        }
        /// <summary>
        /// Тест ввода аргументов с разной длиной
        /// </summary>
        [TestMethod]
        public void TestArgumentLength()
        {
            TimeSpan[] times = new[] {
                new TimeSpan(10, 00, 0),
                new TimeSpan(11, 00, 0),
                new TimeSpan(15, 00, 0)
            };

            int[] durations = new[]
            {
                60,
                30
            };

            TimeSpan startWorkingTime = new TimeSpan(08, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(18, 00, 00);
            int consultationTime = 30;

            Assert.ThrowsException<ArgumentException>(() => {
                Calculations.AvailablePeriods(times, durations, startWorkingTime, endWorkingTime, consultationTime);
            });
        }
    }
}