﻿using System;

namespace Sabertooth.Fx.Extensions {

    public static class DateTimeExt {

        /// <summary>
        /// Checks to see if a date is between two dates.
        /// </summary>
        /// <param name="dt">Date to check.</param>
        /// <param name="rangeBeg">Starting date</param>
        /// <param name="rangeEnd">End date</param>
        /// <returns>True if between the two dates, otherwise, returns false.</returns>
        public static bool Between(this DateTime dt, DateTime rangeBeg, DateTime rangeEnd) {
            return dt.Ticks >= rangeBeg.Ticks && dt.Ticks <= rangeEnd.Ticks;
        }

        /// <summary>
        /// Calculate an age.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> used to calculate age.</param>
        /// <returns>How old something is.</returns>
        public static int CalculateAge(this DateTime dateTime) {
            var age = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(age))
                age--;
            return age;
        }

        /// <summary>
        /// Based on the time, it will display a readable sentence as to when that time
        /// happened (i.e. 'One second ago' or '2 months ago')
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> to convert.</param>
        /// <returns>A friendly sentence as to when that time happened.</returns>
        public static string ToReadableTime(this DateTime value) {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - value.Ticks);
            double delta = ts.TotalSeconds;
            if (delta < 60) {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 120) {
                return "a minute ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days ago";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> falls on a work day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>True if the date is on a workday, otherwise, returns false.</returns>
        /// <remarks>Does not account for holiday's.</remarks>
        public static bool WorkingDay(this DateTime date) {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines if the current <see cref="DateTime"/> falls on a weekend day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>True if the date is on a weekend day, otherwise, returns false.</returns>
        public static bool IsWeekend(this DateTime date) {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines next upcoming work day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The <see cref="DateTime"/> of the next working day.</returns>
        public static DateTime NextWorkday(this DateTime date) {
            var nextDay = date;
            while (!nextDay.WorkingDay()) {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        /// <summary>
        /// Determines the next date by passing in a day of the week.
        /// </summary>
        /// <param name="current">The current <see cref="DateTime"/>.</param>
        /// <param name="dayOfWeek">The <see cref="DayOfWeek"/>.</param>
        /// <returns>Returns the next date for the current <see cref="DayOfWeek"/>.</returns>
        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek) {
            int offsetDays = dayOfWeek - current.DayOfWeek;
            if (offsetDays <= 0) {
                offsetDays += 7;
            }
            DateTime result = current.AddDays(offsetDays);
            return result;
        }
    }
}