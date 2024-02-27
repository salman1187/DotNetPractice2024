using System;

namespace IdeLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //base class variable can store child class object
            IDE ide = new IDE();

            ILang java = new LangJava();
            ILang cs = new LangCSharp();
            ILang c = new LangC();      
            //ILang py = new Python();

            ide.ilang = java;
            ide.Work();
            ide.ilang = cs;
            ide.Work();
            ide.ilang = c;
            ide.Work();
        }
    }

    class IDE
    {
        public ILang ilang;

        public void Work()
        {
            Console.WriteLine(ilang.GetName());
            Console.WriteLine(ilang.GetUnit());
            Console.WriteLine(ilang.GetParadigm());
        }
    }

    interface ILang 
    {
        string GetName();
        string GetUnit();
        string GetParadigm();
    }

    class LangJava : ILang
    {
        public string GetName() { return "Java Language"; }
        public string GetUnit() { return "Class"; }
        public string GetParadigm() { return "Object Oriented"; }
    }

    class LangCSharp : ILang
    {
        public string GetName() { return "CSharp Language"; }
        public string GetUnit() { return "Class"; }
        public string GetParadigm() { return "Object Oriented"; }
    }

    class LangC : ILang
    {
        public string GetName() { return "C Language"; }
        public string GetUnit() { return "Function"; }
        public string GetParadigm() { return "Procedural Oriented"; }
    }
}
