using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator.Business
{
    public class Calculator
    {
        //dependency injection 
        IRepository repository = null;
        public Calculator()
        {
            repository = new FileRepository();
        }
        public Calculator(IRepository repo)
        {
            this.repository = repo;
        }
        public int Sum(int a, int b)
        {
            if(a < 0 || b < 0) return 0;
            if (a % 2 != 0 || b % 2 != 0)
                throw new Exception("Input numbers are not even");
            return a + b;
        }
        public List<int> FilterEvens(List<int> numbers)
        {
            //List<int> ans = numbers.Where(x => x % 2 == 0).ToList();
            if (numbers == null)
                throw new InputEmptyException("Input array is null");
            if(numbers.Count == 0)
                throw new InputEmptyException("Input array is empty");


            List<int> ans = new List<int>();    
            foreach(int n in numbers)
            {
                if(n%2 == 0)
                    ans.Add(n);
            }

            //original
            repository.Save(ans);
            //mock 
            //MockRepo.Save(ans);

            return ans;
        }
    }
    public interface IRepository
    {
        void Save(List<int> data);
    }
    public class FileRepository : IRepository
    {
        public void Save(List<int> data)
        {
            string numbers = data.ToString();
            File.WriteAllText("C:\\Users\\MOHAMMAD SALMAN\\Documents\\listdata.txt", numbers);
        }
    }
    
}
