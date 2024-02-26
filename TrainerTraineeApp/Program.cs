using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainerTraineeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Organization org = new Organization();
            Console.WriteLine("Enter organiztion name: ");
            org.Name = Console.ReadLine();
            Trainer trainer = new Trainer();   
            trainer.Org = org;

            Trainee t1 = new Trainee();
            Trainee t2 = new Trainee();
            Trainee t3 = new Trainee();

            trainer.Trainees.Add(t1);
            trainer.Trainees.Add(t2);
            trainer.Trainees.Add(t3);

            Training training = new Training();
            training.Trainees = trainer.Trainees;
            training.TheTrainer = trainer;

            Course course = new Course();
            Module m1 = new Module();
            Module m2 = new Module();

            Unit u1 = new Unit { Duration = 5 };
            Unit u2 = new Unit { Duration = 25 };

            m1.Units.Add(u1);
            m2.Units.Add(u2);

            course.Modules.Add(m1);
            course.Modules.Add(m2);

            training.TheCourse = course;

            Console.WriteLine($"Org Name : {training.GetTrainingOrganizationName()}");
            Console.WriteLine($"Trainees Count : {training.GetNumOfTrainees()}");
            Console.WriteLine($"Training Duration  : {training.GetTrainingDurationInHrs()}");
        }
    }
    class Organization
    {
        public string Name { get; set; }

    }
    class Trainer
    {
        public Organization Org {  get; set; }  
        public List<Trainee> Trainees { get; set; } = new List<Trainee>();
        public List<Training> Trainings { get; set; } = new List<Training>();
        public string GetOrganization()
        {
            return Org.Name;
        }
    }
    class Trainee
    {
        public Trainer TheTrainer { get; set; }
        public List<Training> Trainings { get; set; } = new List<Training>();
    }
    class Training
    {
        public Trainer TheTrainer { get; set; }
        public List<Trainee> Trainees { get; set; } = new List<Trainee>();
        public Course TheCourse { get; set; }
        public int GetNumOfTrainees()
        {
            return Trainees.Count;
        }
        public string GetTrainingOrganizationName()
        {
            return TheTrainer.Org.Name;
        }
        public int GetTrainingDurationInHrs()
        {
            int duration = 0;
            foreach(Module m in TheCourse.Modules)
            {
                foreach (Unit u in m.Units)
                    duration += u.Duration;
            }
            return duration;
        }
    }
    class Course
    {
        public List<Training> Trainings { get; set;} = new List<Training>();
        public List<Module> Modules { get; set; } = new List<Module>();

    }
    class Module
    {
        public List<Unit> Units { get; set; } = new List<Unit>();
    }
    class Unit
    {
        public int Duration { get; set; }
        public List<Topic> Topics { get; set; } = new List<Topic>();
    }
    class Topic
    {
        public string Name { get; set; }
    }
}
