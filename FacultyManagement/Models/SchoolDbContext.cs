using MySql.Data.MySqlClient;
using System.Reflection.Metadata;

namespace SchoolManagementSystem.Models
{
    public class SchoolDbContext
    {
        public string ConnectionString { get; set; }

        public SchoolDbContext()
        {
            ConnectionString = "server=localhost;database=schooldb;user=root;password=Test#1234";
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        // Methods for Teachers
        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> list = new();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM teachers", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Teacher()
                        {
                            TeacherId = Convert.ToInt32(reader["teacherid"]),
                            TeacherFName = reader["teacherfname"].ToString() ?? string.Empty,
                            TeacherLName = reader["teacherlname"].ToString() ?? string.Empty,
                            EmployeeNumber = reader["employeenumber"].ToString() ?? string.Empty,
                            HireDate = Convert.ToDateTime(reader["hiredate"]),
                            Salary = Convert.ToDecimal(reader["salary"])
                        });
                    }
                }
            }
            return list;
        }

        public Teacher GetTeacherById(int id)
        {
            Teacher teacher = new();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM teachers WHERE teacherid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        teacher = new Teacher()
                        {
                            TeacherId = Convert.ToInt32(reader["teacherid"]),
                            TeacherFName = reader["teacherfname"].ToString() ?? string.Empty,
                            TeacherLName = reader["teacherlname"].ToString() ?? string.Empty,
                            EmployeeNumber = reader["employeenumber"].ToString() ?? string.Empty,
                            HireDate = Convert.ToDateTime(reader["hiredate"]),
                            Salary = Convert.ToDecimal(reader["salary"])
                        };
                    }
                }
            }
            return teacher;
        }

        public void AddTeacher(Teacher teacher)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES (@teacherfname, @teacherlname, @employeenumber, @hiredate, @salary)", conn);
                cmd.Parameters.AddWithValue("@teacherfname", teacher.TeacherFName);
                cmd.Parameters.AddWithValue("@teacherlname", teacher.TeacherLName);
                cmd.Parameters.AddWithValue("@employeenumber", teacher.EmployeeNumber);
                cmd.Parameters.AddWithValue("@hiredate", teacher.HireDate);
                cmd.Parameters.AddWithValue("@salary", teacher.Salary);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE teachers SET teacherfname = @teacherfname, teacherlname = @teacherlname, employeenumber = @employeenumber, hiredate = @hiredate, salary = @salary WHERE teacherid = @id", conn);
                cmd.Parameters.AddWithValue("@id", teacher.TeacherId);
                cmd.Parameters.AddWithValue("@teacherfname", teacher.TeacherFName);
                cmd.Parameters.AddWithValue("@teacherlname", teacher.TeacherLName);
                cmd.Parameters.AddWithValue("@employeenumber", teacher.EmployeeNumber);
                cmd.Parameters.AddWithValue("@hiredate", teacher.HireDate);
                cmd.Parameters.AddWithValue("@salary", teacher.Salary);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTeacher(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                // Set teacherid to NULL in classes table
                MySqlCommand updateClassesCmd = new MySqlCommand("UPDATE classes SET teacherid = NULL WHERE teacherid = @id", conn);
                updateClassesCmd.Parameters.AddWithValue("@id", id);
                updateClassesCmd.ExecuteNonQuery();

                // Delete the teacher record
                MySqlCommand deleteTeacherCmd = new MySqlCommand("DELETE FROM teachers WHERE teacherid = @id", conn);
                deleteTeacherCmd.Parameters.AddWithValue("@id", id);
                deleteTeacherCmd.ExecuteNonQuery();
            }
        }

        // Methods for Students
        public List<Student> GetAllStudents()
        {
            List<Student> list = new();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM students", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Student()
                        {
                            StudentId = Convert.ToInt32(reader["studentid"]),
                            StudentFName = reader["studentfname"].ToString() ?? string.Empty,
                            StudentLName = reader["studentlname"].ToString() ?? string.Empty,
                            StudentNumber = reader["studentnumber"].ToString() ?? string.Empty,
                            EnrolDate = Convert.ToDateTime(reader["enroldate"])
                        });
                    }
                }
            }
            return list;
        }

        public Student GetStudentById(int id)
        {
            Student student = new();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM students WHERE studentid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student = new Student()
                        {
                            StudentId = Convert.ToInt32(reader["studentid"]),
                            StudentFName = reader["studentfname"].ToString() ?? string.Empty,
                            StudentLName = reader["studentlname"].ToString() ?? string.Empty,
                            StudentNumber = reader["studentnumber"].ToString() ?? string.Empty,
                            EnrolDate = Convert.ToDateTime(reader["enroldate"])
                        };
                    }
                }
            }
            return student;
        }

        public void AddStudent(Student student)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO students (studentfname, studentlname, studentnumber, enroldate) VALUES (@studentfname, @studentlname, @studentnumber, @enroldate)", conn);
                cmd.Parameters.AddWithValue("@studentfname", student.StudentFName);
                cmd.Parameters.AddWithValue("@studentlname", student.StudentLName);
                cmd.Parameters.AddWithValue("@studentnumber", student.StudentNumber);
                cmd.Parameters.AddWithValue("@enroldate", student.EnrolDate);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE students SET studentfname = @studentfname, studentlname = @studentlname, studentnumber = @studentnumber, enroldate = @enroldate WHERE studentid = @id", conn);
                cmd.Parameters.AddWithValue("@id", student.StudentId);
                cmd.Parameters.AddWithValue("@studentfname", student.StudentFName);
                cmd.Parameters.AddWithValue("@studentlname", student.StudentLName);
                cmd.Parameters.AddWithValue("@studentnumber", student.StudentNumber);
                cmd.Parameters.AddWithValue("@enroldate", student.EnrolDate);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM students WHERE studentid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // Methods for Classes
        public List<Class> GetAllClasses()
        {
            List<Class> list = new List<Class>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM classes", conn);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int classId = Convert.ToInt32(reader["classid"]);
                    string classCode = reader["classcode"].ToString() ?? string.Empty;
                    int? teacherId = null;
                    DateTime? startDate = null;
                    DateTime? finishDate = null;
                    string className = reader["classname"].ToString() ?? string.Empty;

                    // Check for DBNull before conversion
                    if (!Convert.IsDBNull(reader["teacherid"]))
                    {
                        teacherId = Convert.ToInt32(reader["teacherid"]);
                    }

                    if (!Convert.IsDBNull(reader["startdate"]))
                    {
                        startDate = Convert.ToDateTime(reader["startdate"]);
                    }

                    if (!Convert.IsDBNull(reader["finishdate"]))
                    {
                        finishDate = Convert.ToDateTime(reader["finishdate"]);
                    }

                    list.Add(new Class()
                    {
                        ClassId = classId,
                        ClassCode = classCode,
                        TeacherId = teacherId,
                        StartDate = startDate,
                        FinishDate = finishDate,
                        ClassName = className,
                    });
                }
            }
            return list;
        }

        public Class GetClassById(int id)
        {
            Class classData = new();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM classes WHERE classid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    classData = new Class()
                    {
                        ClassId = Convert.ToInt32(reader["classid"]),
                        ClassCode = reader["classcode"].ToString() ?? string.Empty,
                        TeacherId = reader["teacherid"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["teacherid"]),
                        StartDate = Convert.ToDateTime(reader["startdate"]),
                        FinishDate = Convert.ToDateTime(reader["finishdate"]),
                        ClassName = reader["classname"].ToString() ?? string.Empty,
                        
                    };
                }
            }
            return classData;
        }

        public void AddClass(Class classData)
        {
            // Check if the provided teacherid exists in the teachers table
            if (!TeacherExists(classData.TeacherId))
            {
                throw new Exception("Teacher with the provided teacherid does not exist.");
            }

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO classes (classcode, classname, teacherid, startdate, finishdate) VALUES (@classcode, @classname, @teacherid, @startdate, @finishdate)", conn);
                cmd.Parameters.AddWithValue("@classcode", classData.ClassCode);
                cmd.Parameters.AddWithValue("@teacherid", classData.TeacherId);
                cmd.Parameters.AddWithValue("@startdate", classData.StartDate);
                cmd.Parameters.AddWithValue("@finishdate", classData.FinishDate);
                cmd.Parameters.AddWithValue("@classname", classData.ClassName);
                
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateClass(Class classData)
        {
            // Check if the provided teacherid exists in the teachers table
            if (!TeacherExists(classData.TeacherId))
            {
                throw new Exception("Teacher with the provided teacherid does not exist.");
            }

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE classes SET classcode = @classcode, teacherid = @teacherid, startdate = @startdate, finishdate = @finishdate, classname = @classname WHERE classid = @id", conn);
                cmd.Parameters.AddWithValue("@id", classData.ClassId);
                cmd.Parameters.AddWithValue("@classcode", classData.ClassCode);
                cmd.Parameters.AddWithValue("@teacherid", classData.TeacherId);
                cmd.Parameters.AddWithValue("@startdate", classData.StartDate);
                cmd.Parameters.AddWithValue("@finishdate", classData.FinishDate);
                cmd.Parameters.AddWithValue("@classname", classData.ClassName);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteClass(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM classes WHERE classid = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private bool TeacherExists(int? teacherId)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM teachers WHERE teacherid = @teacherid", conn);
                cmd.Parameters.AddWithValue("@teacherid", teacherId);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
}
