using RegistrationApp.DataClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RegistrationApp.DataClient
{
    public class RegistrationDataService : IRegistrationDataService
    {
        private EfData db = new EfData();

        public List<StudentDAO> GetStudents()
        {
            var students = new List<StudentDAO>();

            foreach (var st in db.GetStudents())
            {
                students.Add(DataMapper.MapToStudentDAO(st));
            }

            return students;
        }


        public List<StudentUserDAO> GetStudentsUsers()
        {
            var studentUsers = new List<StudentUserDAO>();

            foreach (var su in db.GetStudentsUsers())
            {
                studentUsers.Add(DataMapper.MapToStudenUserDAO(su));
            }

            return studentUsers;
        }


        public List<RegistrarUserDAO> GetRegistrarUsers()
        {
            var registrarUsers = new List<RegistrarUserDAO>();

            foreach (var ru in db.GetRegistrarUsers())
            {
                registrarUsers.Add(DataMapper.MapToRegistrarUserDAO(ru));
            }

            return registrarUsers;
        }


        public List<CourseDAO> GetCourses()
        {
            var courses = new List<CourseDAO>();

            foreach (var c in db.GetCourses())
            {
                courses.Add(DataMapper.MapToCourseDAO(c));
            }

            return courses;
        }

        public List<CourseSessionDAO> GetSessions()
        {
            var sessions = new List<CourseSessionDAO>();

            foreach (var cs in db.GetSessions())
            {
                sessions.Add(DataMapper.MapToCourseSessionDAO(cs));
            }

            return sessions;
        }


        public bool RegisterStudent(int studentId, int sessionId)
        {
            bool registered = db.RegisterStudent(studentId, sessionId);

            return registered;
        }


        public bool DropFromSession(int studentId, int sessionId)
        {
            bool dropped = db.DropFromSession(studentId, sessionId);

            return dropped;
        }


        public bool AddCourseBookmark(BookmarkedSessionDAO bookmarkToAdd)
        {
            bool added = db.AddCourseBookmark(DataMapper.MapToBookmarkedSession(bookmarkToAdd));

            return added;
        }


        public bool RemoveCourseBookmark(BookmarkedSessionDAO bookmarkToRemove)
        {
            bool removed = db.RemoveCourseBookmark(DataMapper.MapToBookmarkedSession(bookmarkToRemove));

            return removed;
        }


        public List<BookmarkedSessionDAO> GetBookmarks()
        {
            var bookmarks = new List<BookmarkedSessionDAO>();

            foreach (var bk in db.GetBookmarkedSessions())
            {
                bookmarks.Add(DataMapper.MapToBookmarkedSessionDAO(bk));
            }

            return bookmarks;
        }


        public List<ScheduleDAO> GetSchedules()
        {
            var schedules = new List<ScheduleDAO>();

            foreach (var sch in db.GetSchedules())
            {
                schedules.Add(DataMapper.MapToScheduleDAO(sch));
            }

            return schedules;
        }


        public bool InsertCourse(CourseDAO course)
        {

            bool inserted = db.InsertCourse(DataMapper.MapToCourse(course));
            return inserted;
        }


        public bool CancelCourse(CourseDAO course)
        {
            bool removed = db.CancelCourse(DataMapper.MapToCourse(course));
            return removed;
        }


        public bool InsertStudent(StudentDAO student)
        {
            bool inserted = db.InsertStudent(DataMapper.MapToStudent(student));
            return inserted;
        }

        
        public bool RemoveStudent(StudentDAO student)
        {
            bool removed = db.RemoveStudent(DataMapper.MapToStudent(student));
            return removed;
        }

        public List<CourseSessionDAO> GetStudentSchedule(int studentId)
        {
            var allSessions = GetSessions();

            var matchingSchedules = db.GetSchedules().Where(sch => sch.Student.Id.Equals(studentId));
            
            List<CourseSessionDAO> scheduleCourses = new List<CourseSessionDAO>();
            foreach (var sch in matchingSchedules)
            {
                var matchingSessions = allSessions.Where(s => s.Id.Equals(sch.CourseSessionId));
                scheduleCourses.Add(matchingSessions.First());
            }
            return scheduleCourses;
        }


        public List<StudentDAO> ListEnrolledStudents(int sessionId)
        {
            List <StudentDAO> studentsDAO = new List<StudentDAO>();

            var students = db.ListEnrolledStudents(sessionId);

            foreach (var st in students)
            {
                studentsDAO.Add(DataMapper.MapToStudentDAO(st));
            }

            return studentsDAO;
        }
    }
}
