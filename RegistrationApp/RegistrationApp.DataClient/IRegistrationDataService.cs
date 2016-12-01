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

    [ServiceContract]
    public interface IRegistrationDataService
    {
        [OperationContract]
        List<StudentDAO> GetStudents();

        [OperationContract]
        List<StudentUserDAO> GetStudentsUsers();

        [OperationContract]
        List<RegistrarUserDAO> GetRegistrarUsers();

        [OperationContract]
        List<CourseDAO> GetCourses();

        [OperationContract]
        List<CourseSessionDAO> GetSessions();

        [OperationContract]
        bool RegisterStudent(int studentId, int sessionId);

        [OperationContract]
        bool DropFromSession(int studentId, int sessionId);

        [OperationContract]
        bool AddCourseBookmark(BookmarkedSessionDAO bookmarkToAdd);

        [OperationContract]
        bool RemoveCourseBookmark(BookmarkedSessionDAO bookmarkToRemove);

        [OperationContract]
        List<BookmarkedSessionDAO> GetBookmarks();

        [OperationContract]
        List<ScheduleDAO> GetSchedules();

        [OperationContract]
        bool InsertCourse(CourseDAO course);

        [OperationContract]
        bool CancelCourse(CourseDAO course);



        [OperationContract]
        bool InsertStudent(StudentDAO student);

        [OperationContract]
        bool RemoveStudent(StudentDAO student);

        [OperationContract]
        List<CourseSessionDAO> GetStudentSchedule(int studentId);

        [OperationContract]
        List<StudentDAO> ListEnrolledStudents(int sessionId);
    }

}
