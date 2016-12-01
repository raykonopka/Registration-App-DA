using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationApp.DataClient
{
    public partial class EfData
    {
        private RegistrationDBEntities db = new RegistrationDBEntities();

        //General Data Access.

        #region Student User Data Access
        public List<StudentUser> GetStudentsUsers()
        {
            return db.StudentUsers.ToList();
        }
        #endregion


        #region Registrar User Data Access
        public List<RegistrarUser> GetRegistrarUsers()
        {
            return db.RegistrarUsers.ToList();
        }
        #endregion


        #region Students Data Access
        public List<Student> GetStudents()
        {
            return db.Students.ToList();
        }


        public bool InsertStudent(Student student)
        {
            var matchingMajors = db.Majors.Where(s => s.Id.Equals(student.MajorId));

            if (matchingMajors.Count() == 1)
            {
                db.Students.Add(student);
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Could not insert student because major id is invalid.");
                return false;
            }
        }


        public bool RemoveStudent(Student studentToRemove)
        {
            List<Student> allStudents = db.Students.ToList();

            var matchingStudents = allStudents.Where(st => st.Id.Equals(studentToRemove.Id));
            if (matchingStudents.Count() > 0)
            {
                db.Students.Remove(matchingStudents.First());
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Student was not found.");
                return false;
            }
        }
        #endregion


        #region Majors Data Access
        public List<Major> GetMajors()
        {
            return db.Majors.ToList();
        }


        public bool InsertMajor(Major major)
        {
            var matchingMajors = db.Majors.Where(s => s.Id.Equals(major.Id));
            
            if (matchingMajors.Count() == 0)
            {
                db.Majors.Add(major);
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Could not insert major because major id is invalid.");
                return false;
            }
        }
        #endregion


        #region Schedules Data Access
        public List<Schedule> GetSchedules()
        {
            return db.Schedules.ToList();
        }


        public bool InsertSchedule(Schedule schedule)
        {
            var matchingStudents = db.Students.Where(s => s.Id.Equals(schedule.StudentId));
            var matchingSessions = db.CourseSessions.Where(s => s.Id.Equals(schedule.CourseSessionId));

            if ( (matchingStudents.Count() == 1) && (matchingSessions.Count() == 1) )
            {
                db.Schedules.Add(schedule);
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Invalid student id or invalid session id.");
                return false;
            }
        }

        public bool removeSchedule(Schedule scheduleToRemove)
        {
            List<Schedule> allSchedules = db.Schedules.ToList();

            var currentStudentSchedule = allSchedules.Where(s => s.StudentId.Equals(scheduleToRemove.StudentId));
            if (currentStudentSchedule.Count() == 0)
            {
                Debug.WriteLine("Student is not registered for any courses.");
                return false;
            }

            var schedulesToDrop = currentStudentSchedule.Where(i => i.CourseSessionId.Equals(scheduleToRemove.CourseSessionId));
            if (schedulesToDrop.Count() == 0)
            {
                Debug.WriteLine("Student is not registered the specified course.");
                return false;
            }

            db.Schedules.Remove(schedulesToDrop.First());
            return db.SaveChanges() > 0;
        }
        #endregion


        #region Sessions Data Access
        public List<CourseSession> GetSessions()
        {
            return db.CourseSessions.ToList();
        }


        public bool InsertSession(CourseSession session)
        {
            var matchingCourses = db.Courses.Where(s => s.Id.Equals(session.CourseId));

            if (matchingCourses.Count() == 1)
            {
                db.CourseSessions.Add(session);
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Invalid course id.");
                return false;
            }
        }

        public bool RemoveSession(CourseSession session)
        {
            var matchingSessions = db.CourseSessions.Where(cs => cs.Id.Equals(session.Id));

            if (matchingSessions.Count() > 0)
            {
                db.CourseSessions.Remove(matchingSessions.First());
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Course does not exist.");
                return false;
            }
        }
        #endregion


        #region Courses Data Access
        public List<Course> GetCourses()
        {
            return db.Courses.ToList();
        }


        public bool InsertCourse(Course course)
        {
            var matchingDepartments = db.Departments.Where(s => s.Id.Equals(course.DepartmentId));

            if (matchingDepartments.Count() == 1)
            {
                db.Courses.Add(course);
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Invalid department id.");
                return false;
            }
        }


        public bool CancelCourse(Course course)
        {
            var matchingCourses = db.Courses.Where(c => c.Id.Equals(course.Id));

            if (matchingCourses.Count() > 0)
            {
                db.Courses.Remove(matchingCourses.First());
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Course does not exist.");
                return false;
            }
        }
        #endregion


        #region Departments Data Access
        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }


        public bool InsertDepartment(Department department)
        {
            var matchingDepartments = db.Departments.Where(s => s.Id.Equals(department.Id));

            if (matchingDepartments.Count() == 0)
            {
                db.Departments.Add(department);
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Could not insert department because department id is invalid.");
                return false;
            }
        }
        #endregion



        // Additional Data Access Functionality

        #region Register Student for Session
        public bool RegisterStudent(int studentId, int sessionId)
        {
            List<Schedule> allSchedules = GetSchedules();
            var currentStudentSchedule = allSchedules.Where(s => s.StudentId.Equals(studentId));

            List<CourseSession> allSessions = GetSessions();
            var matchingSessions = allSessions.Where(i => i.Id.Equals(sessionId));
            CourseSession session = matchingSessions.First();

            //Check for time conflicts
            bool timeConflict;
            timeConflict = false;
            foreach (Schedule scheduleEntry in currentStudentSchedule)
            { 
                if (scheduleEntry.CourseSession.DaysInSession == session.DaysInSession)
                {
                    if (
                        (scheduleEntry.CourseSession.StartTime == session.StartTime) ||
                        (scheduleEntry.CourseSession.StartTime > session.StartTime && scheduleEntry.CourseSession.StartTime < session.EndTime)
                       )
                    {
                        timeConflict = true;
                    }
                }
            }

            if (timeConflict)
            {
                Debug.WriteLine("Student is already registered for a class during this time.");
                return false;
            }

            else
            {
                Schedule registrationEntry = new Schedule() { StudentId = studentId, CourseSessionId = sessionId };
                return InsertSchedule(registrationEntry);
            }
        }
        #endregion


        #region Drop Student From Session
        public bool DropFromSession(int studentId, int sessionId)
        {
            Schedule scheduleToDrop = new Schedule { StudentId = studentId, CourseSessionId = sessionId };

            return removeSchedule(scheduleToDrop);
        }
        #endregion


        #region Update Session Time
        public bool UpdateSessionTime(TimeSpan startTime, TimeSpan endTime, int sessionId)
        {
            var matchingSessions = db.CourseSessions.Where(cs => cs.Id.Equals(sessionId));

            if (matchingSessions.Count() > 0)
            {
                CourseSession sessionToUpdate = matchingSessions.First();

                sessionToUpdate.StartTime = startTime;
                sessionToUpdate.EndTime = endTime;
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Invalid session id.");
                return false;
            }

        }
        #endregion


        #region Update Session Capacity
        public bool UpdateSessionCapacity(int newCapacity, int sessionId)
        {
            var matchingSessions = db.CourseSessions.Where(cs => cs.Id.Equals(sessionId));

            if (matchingSessions.Count() > 0)
            {
                CourseSession sessionToUpdate = matchingSessions.First();

                sessionToUpdate.Capacity = newCapacity;
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Invalid session id.");
                return false;
            }

        }
        #endregion


        #region List Students Enrolled In Session
        public List<Student> ListEnrolledStudents(int sessionId)
        {   
            List<Student> studentsEnrolled = new List<Student>();
            int studentIdTemp;

            var matchingSchedules = db.Schedules.Where(s => s.CourseSessionId.Equals(sessionId));

            if (matchingSchedules.Count() > 0)
            {
                foreach (Schedule sch in matchingSchedules)
                {
                    studentIdTemp = sch.StudentId;
                    var matchingStudents = db.Students.Where(st => st.Id.Equals(studentIdTemp));
                    studentsEnrolled.Add(matchingStudents.First());
                }

                return studentsEnrolled;
            }

            else
            {
                Debug.WriteLine("There are no students enrolled in that session.");
                return studentsEnrolled;
            }
        }
        #endregion


        #region List Student Schedule
        public List<CourseSession> ListStudentSchedule(int studentId)
        {
            List<CourseSession> studentSchedule = new List<CourseSession>();
            int courseSessionId;
            var matchingSchedules = db.Schedules.Where(sch => sch.Student.Id.Equals(studentId));

            if (matchingSchedules.Count() > 0)
            {
                foreach (Schedule sch in matchingSchedules)
                {
                    courseSessionId = sch.CourseSessionId;
                    var matchingCourseSessions = db.CourseSessions.Where(cs => cs.Id.Equals(courseSessionId));
                    studentSchedule.Add(matchingCourseSessions.First());
                }

                return studentSchedule;
            }

            else
            {
                Debug.WriteLine("The students is not registered for any courses.");
                return studentSchedule;
            }
        }
        #endregion


        #region Get Bookmarked Sessions
        public List<BookmarkedSession> GetBookmarkedSessions()
        {
            return db.BookmarkedSessions.ToList();
        }
        #endregion


        #region Add A Course Bookmark
        public bool AddCourseBookmark(BookmarkedSession bookmarkToAdd)
        {
            List<BookmarkedSession> allBookmarks = db.BookmarkedSessions.ToList();

            var matchingBookmarks1 = allBookmarks.Where(bm => bm.StudentUserId.Equals(bookmarkToAdd.StudentUserId));
            var matchingBookmarks2 = matchingBookmarks1.Where(bm => bm.SessionId.Equals(bookmarkToAdd.SessionId));

            if (matchingBookmarks2.Count() == 0)
            {
                db.BookmarkedSessions.Add(bookmarkToAdd);
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Bookmark already exists.");
                return false;
            }
        }
        #endregion


        #region Remove A Course Bookmark
        public bool RemoveCourseBookmark(BookmarkedSession bookmarkToRemove)
        {
            var matchingBookmarks = db.BookmarkedSessions.Where(bm => bm.Id.Equals(bookmarkToRemove.Id));

            if (matchingBookmarks.Count() > 0)
            {
                db.BookmarkedSessions.Remove(matchingBookmarks.First());
                return db.SaveChanges() > 0;
            }

            else
            {
                Debug.WriteLine("Bookmark does not contain valid information.");
                return false;
            } 
        }
        #endregion

    }
}
