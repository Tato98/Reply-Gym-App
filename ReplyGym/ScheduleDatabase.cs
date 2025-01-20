using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplyGym
{
    class ScheduleDatabase
    {
        private readonly SQLiteConnection _dbSchedule;

        public ScheduleDatabase()
        {
            var dbFieldPath = Path.Combine(FileSystem.AppDataDirectory, "app_field_data.db");
            _dbSchedule = new SQLiteConnection(dbFieldPath);
            _dbSchedule.CreateTable<Schedule>();
        }

        public void SaveScheduleData(Schedule scheduleData)
        {
            _dbSchedule.Insert(scheduleData);
        }

        public void UpdateCourseForDay(string dayOfWeek, string newCourse)
        {
            var courseToUpdate = _dbSchedule.Table<Schedule>().FirstOrDefault(c => c.Day == dayOfWeek);

            if (courseToUpdate != null)
            {
                // Modifica il corso
                courseToUpdate.Course = newCourse;

                // Esegui l'aggiornamento nel database
                _dbSchedule.Update(courseToUpdate);
            }
        }

        public List<Schedule> GetAllSchedules()
        {
            return [.. _dbSchedule.Table<Schedule>()];
        }

        public Schedule GetScheduleByDay(string dayOfWeek)
        {
            return _dbSchedule.Table<Schedule>().FirstOrDefault(s => s.Day == dayOfWeek);
        }
    }
}
