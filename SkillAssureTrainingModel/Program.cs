using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssureTraining
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Client Name: ");
            SkillAssureTrainingModel model = new SkillAssureTrainingModel();
            model.ClientName = Console.ReadLine();

            Iteration[] itrs = new Iteration[3];
            for (int i = 0; i < itrs.Length; i++)
                itrs[i] = new Iteration();

            Assessment a1 = new Assessment();
            Assessment a2 = new Assessment();
            Assessment a3 = new Assessment();
            a1.NoQuestions = 1;
            a2.NoQuestions = 2;
            a3.NoQuestions = 1;
            MCQQuestion mcq1 = new MCQQuestion();
            MCQQuestion mcq2 = new MCQQuestion();
            HandsOnQuestion handson1 = new HandsOnQuestion();
            Question q1 = mcq1;
            Question q2 = mcq2;
            Question q3 = handson1;
            q1.Marks = 1;
            q2.Marks = 1;
            q3.Marks = 2;
            a1.Questions.Add(q1); //mcq
            a2.Questions.Add(q1); //mcq
            a2.Questions.Add(q2); //mcq
            a3.Questions.Add(q3); //handson
            itrs[0].Assessments.Add(a1); //1 marks
            itrs[1].Assessments.Add(a2); //2 marks
            itrs[2].Assessments.Add(a3); //2 marks

            model.iterations = itrs;

            Console.WriteLine($"total assessments: {model.GetTotalAssessmentsInTheTraining()}");
            Console.WriteLine($"total mcq assessments: {model.GetNumMCQBasedAssessments()}");
            Console.WriteLine($"total handson assessments: {model.GetNumHandsOnBasedAssessments()}");
            Console.WriteLine($"total score: {model.GetTotalScoreOfAllAssessments()}");
        }
    }
    class SkillAssureTrainingModel
    {
        public string ClientName { get; set; } 
        public Iteration[] iterations { get; set; } = new Iteration[3];
        public int GetTotalAssessmentsInTheTraining()
        {
            return iterations[0].Assessments.Count + iterations[1].Assessments.Count + iterations[2].Assessments.Count;
        }
        public int GetNumMCQBasedAssessments()
        {
            int total = 0;
            foreach(Iteration i in iterations)
            {
                foreach(Assessment a in  i.Assessments)
                {
                    foreach(Question q in a.Questions)
                    {
                        if (q is MCQQuestion)
                            total++;
                    }
                }
            }
            return total;
        }
        public int GetNumHandsOnBasedAssessments()
        {
            int total = 0;
            foreach (Iteration i in iterations)
            {
                foreach (Assessment a in i.Assessments)
                {
                    foreach (Question q in a.Questions)
                    {
                        if (q is HandsOnQuestion)
                            total++;
                    }
                }
            }
            return total;
        }
        public int GetTotalScoreOfAllAssessments()
        {
            int total = 0;
            foreach (Iteration i in iterations)
            {
                foreach (Assessment a in i.Assessments)
                {
                    total += a.GetTotalMarks();
                }
            }
            return total;
        }
    }
    class Iteration
    {
        public int IterationNo { get; set; }
        public string Goal { get; set; }
        public Course TheCourse { get; set; }
        public List<Assessment> Assessments { get; set; } = new List<Assessment>();
    }
    class Course
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
        public List<Assessment> Assessments { get; set; } = new List<Assessment>();
    }
    class Assessment
    {
        public string AssessmentId { get; set; }
        public string Description { get; set; }

        public int NoQuestions { get; set; }
        public DateTime DtAssessments { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();
        public int GetTotalMarks()
        {
            int total = 0;
            foreach (Question q in Questions)
                total += q.Marks;

            return total;
        }

    }
    class Question
    {
        public int Marks { get; set; }
    }
    class MCQQuestion : Question
    {
        public string QuestionName { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string RightOption { get; set; }
    }
    class HandsOnQuestion : Question
    {
        public string QuestionDescription { get; set; } 
        public string ReferenceDocument { get; set; }
    }
}
