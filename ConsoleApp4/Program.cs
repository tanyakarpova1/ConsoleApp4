using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


class ApplicationContext : DbContext
{


    public DbSet<Student> Students { get; set; }
    public DbSet<Classrooms> classrooms { get; set; }
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DB1;Username=postgres;Password=1234");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public int classroomid { get; set; }
        public Classrooms classrooms { get; set; }
        public override string ToString()
        {
            return $"Student: name = {name}";
        }
    }


    public class Classrooms
    {
        public Classrooms()
        {
            Students = new List<Student>();
        }
        public int id { get; set; }
        public string teacher { get; set; }
        public virtual List<Student> Students { get; set; }
        public override string ToString()
        {
            return $"classroom: teacher = {teacher}()";
        }
    }

    static void Main(string[] args)
    {
        using (ApplicationContext applicationContext = new ApplicationContext())
        {
            Classrooms classrooms1 = new Classrooms { teacher = "Фролов Константин Иванович" };
            Classrooms classrooms2 = new Classrooms { teacher = "Кузнецов Илья Степанович" };
            Classrooms classrooms3 = new Classrooms { teacher = "Соколова Екатерина Михайловна" };
            Student student1 = new Student { name = "Анна Попова" };
            Student student2 = new Student { name = "Анастасия Афанасьева" };
            Student student3 = new Student { name = "Евгений Соловьев" };
            Student student4 = new Student { name = "Сергей Морозов" };
            Student student5 = new Student { name = "Игорь Воробьев" };
            Student student6 = new Student { name = "Анжелика Голубева" };
            Student student7 = new Student { name = "Наталья Орлова" };

            classrooms1.Students.Add(student1);
            classrooms2.Students.Add(student3);
            classrooms3.Students.Add(student2);
            classrooms1.Students.Add(student5);
            classrooms2.Students.Add(student7);
            classrooms3.Students.Add(student6);

            applicationContext.classrooms.AddRange(classrooms1, classrooms2, classrooms3);
            applicationContext.SaveChanges();

        }

        static void Main(string[] args)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                List<Classrooms> classrooms = applicationContext.classrooms.ToList();
                foreach (Classrooms t in classrooms)
                {
                    Console.WriteLine(t);

                }
            }
        }
    }
}
