namespace SF2022User01Lib
{
    /// <summary>
    /// 
    /// </summary>
    public static class Calculations
    {
        /// <summary>
        /// Рассчет свободного времени
        /// </summary>
        /// <param name="startTimes">Времена начала занятых промежутков</param>
        /// <param name="durations">Длительность занятых промежутков</param>
        /// <param name="beginWorkingTime">Начало рабочего дня</param>
        /// <param name="endWorkingTime">Конец рабочего дня</param>
        /// <param name="consultationTime">Время консультации</param>
        /// <returns>Список свободных промежутков для консультация</returns>
        /// <exception cref="ArgumentException">Выбрасывается при несовпадающих длинах начала и длительности занятых промежуткеов времени</exception>
        public static string[] AvailablePeriods(
            TimeSpan[] startTimes,
            int[] durations,
            TimeSpan beginWorkingTime,
            TimeSpan endWorkingTime,
            int consultationTime
            )
        {
            if(startTimes.Length != durations.Length)
            {
                throw new ArgumentException("StartTimes and Durations lengths must be the same");
            }

            List<string> availablePeriods = new();

            int i = 0;
            int availMin;
            TimeSpan currentPeriod = beginWorkingTime;
            TimeSpan availablePeriod = default;

            while (true)
            {
                TimeSpan next = i >= startTimes.Length ? endWorkingTime : startTimes[i];
                availMin = (int)(next - currentPeriod).TotalMinutes;

                if(availMin >= consultationTime)
                {
                    TimeSpan startTime = currentPeriod;
                    while(startTime <= next - TimeSpan.FromMinutes(consultationTime))
                    {
                        var endTime = startTime.Add(TimeSpan.FromMinutes(consultationTime));

                        string start = string.Format("{0:00}:{1:00}", startTime.Hours, startTime.Minutes);
                        string end = string.Format("{0:00}:{1:00}", endTime.Hours, endTime.Minutes);

                        availablePeriods.Add($"{start}-{end}");

                        startTime = endTime;
                    }
                }
                
                if (i >= startTimes.Length)
                {
                    break;
                }

                currentPeriod = startTimes[i].Add(TimeSpan.FromMinutes(durations[i]));

                i++;
            }

            return availablePeriods.ToArray() ;
        }
    }
}