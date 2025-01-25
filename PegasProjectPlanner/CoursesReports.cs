using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PegasProjectPlanner
{
    public class CoursesReports
    {
        string nameCourse = "";
        string fullNameStudent = "";
        int countTasks = 0;
        int countOfCompletedTasks = 0;

        public CoursesReports(string nameCourse, string fullNameStudent, int countTasks, int countOfCompletedTasks)
        {
            this.nameCourse = nameCourse;
            this.fullNameStudent = fullNameStudent;
            this.countTasks = countTasks;
            this.countOfCompletedTasks = countOfCompletedTasks;
        }

        public string NameCourse
        {
            get
            {
                return this.nameCourse;
            }
            set
            {
                this.nameCourse = value;
            }
        }
        public string FullNameStudent
        {
            get
            {
                return this.fullNameStudent;
            }
            set
            {
                this.fullNameStudent = value;
            }
        }
        public int CountTasks
        {
            get
            {
                return this.countTasks;
            }
            set
            {
                this.countTasks = value;
            }
        }
        public int CountOfCompletedTasks
        {
            get
            {
                return this.countOfCompletedTasks;
            }
            set
            {
                this.countOfCompletedTasks = value;
            }
        }
    }
}
