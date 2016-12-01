using RegistrationApp.DataClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace RegistrationApp.Tests
{

    public class EfDataTests
    {

        #region Course Tests
        [Fact]
        public void Test_CancelCourse()
        {
            var data = new EfData();
            Course courseToCancel = new Course { Title = "Analysis of Algorithms", Credits = 3, DepartmentId = 1 };

            //Add test course
            data.InsertCourse(courseToCancel);

            var actual = data.CancelCourse(courseToCancel);
            Assert.True(actual);
        }


        [Fact]
        public void Test_UpdateSessionTime()
        {
            var data = new EfData();
            TimeSpan startTime = new TimeSpan(0, 15, 25, 00);
            TimeSpan endTime = new TimeSpan(0, 17, 00, 00);
            CourseSession sessionToUpdate = new CourseSession { CourseId = 1, Professor = "Bob Smith", Capacity = 40, DaysInSession = "TH", StartTime = startTime, EndTime = endTime };

            //Add test session
            data.InsertSession(sessionToUpdate);

            TimeSpan newStartTime = new TimeSpan(0, 10, 45, 00);
            TimeSpan newEndTime = new TimeSpan(0, 12, 00, 00);
            var actual = data.UpdateSessionTime(newStartTime, newEndTime, sessionToUpdate.Id);

            Assert.True(actual);

            //Remove any test data
            data.RemoveSession(sessionToUpdate);
        }


        [Fact]
        public void Test_UpdateSessionCapacity()
        {
            var data = new EfData();
            TimeSpan startTime = new TimeSpan(0, 15, 25, 00);
            TimeSpan endTime = new TimeSpan(0, 17, 00, 00);
            CourseSession sessionToUpdate = new CourseSession { CourseId = 1, Professor = "Bob Smith", Capacity = 40, DaysInSession = "TH", StartTime = startTime, EndTime = endTime };

            //Add test session
            data.InsertSession(sessionToUpdate);

            int updatedCapacity = 50;
            var actual = data.UpdateSessionCapacity(updatedCapacity, sessionToUpdate.Id);

            Assert.True(actual);

            //Remove any test data
            data.RemoveSession(sessionToUpdate);
        }


        [Fact]
        public void Test_ListEnrolledStudents()
        {
            var data = new EfData();

            List<CourseSession> sessions = data.GetSessions();
            int sessionId = sessions.First().Id;

            var actual = data.ListEnrolledStudents(sessionId);

            Assert.NotNull(actual);

            //Print list of students to debug
            foreach (Student st in actual)
            {
                Debug.WriteLine(st.StudentName);
            }
        }
        #endregion

        #region Master Scheduler
        [Fact]
        public void Test_ListStudentSchedule()
        {
            var data = new EfData();

            List<Student> students = data.GetStudents();
            int studentId = students.First().Id;

            var actual = data.ListStudentSchedule(studentId);

            Assert.NotNull(actual);

            //Print list of course IDs to debug
            foreach (CourseSession st in actual)
            {
                Debug.WriteLine(st.CourseId);
            }
        }


        [Fact]
        public void Test_InsertStudent()
        {
            var data = new EfData();

            Student studentToAdd = new Student { MajorId = 1, StudentName = "Test Student" };
            var actual = data.InsertStudent(studentToAdd);
            Assert.True(actual);

            //Remove test data
            data.RemoveStudent(studentToAdd);
        }


        [Fact]
        public void Test_RemoveStudent()
        {
            var data = new EfData();

            Student studentToAdd = new Student { MajorId = 1, StudentName = "Test Student" };
            data.InsertStudent(studentToAdd);

            var actual = data.RemoveStudent(studentToAdd);
            Assert.True(actual);
        }
        #endregion

    }
}
