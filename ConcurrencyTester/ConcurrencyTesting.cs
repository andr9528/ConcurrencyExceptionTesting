using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Domain.Concrete;
using Domain.Core;
using Repository.Core;

namespace ConcurrencyTester
{
    public class ConcurrencyTesting : IConcurrencyTesting
    {
        private readonly IGenericRepository repo;

        public ConcurrencyTesting(IGenericRepository repo)
        {
            this.repo = repo;
        }

        public void RunTests()
        {
            Console.WriteLine("Running Tests...");

            RunImplicitTest();

            RunExplicitTest();

            RunTimestampTest();
        }

        private void RunTimestampTest()
        {
            Console.WriteLine();
            try
            {
                var element = AddElement(new DummyTimestamp());

                Console.WriteLine("Attempting to throw 'DBConcurrencyException' with use of 'Timestamp Concurrency Check'");

                var result1 = UserOneAsync(element);
                var result2 = UserTwoAsync(element);

                Console.WriteLine($"Attempt Failed -> User One: {0}, User Two: {1}", result1.Result, result2.Result);
            }
            catch (DBConcurrencyException e)
            {
                Console.WriteLine($"Attempt Successful -> {0}", e.Message);
            }
        }

        private void RunExplicitTest()
        {
            Console.WriteLine();
            try
            {
                var element = AddElement(new DummyExplicit());

                Console.WriteLine("Attempting to throw 'DBConcurrencyException' with use of 'Explicit Concurrency Check'");

                var result1 = UserOneAsync(element);
                var result2 = UserTwoAsync(element);

                Console.WriteLine($"Attempt Failed -> User One: {0}, User Two: {1}", result1.Result, result2.Result);
            }
            catch (DBConcurrencyException e)
            {
                Console.WriteLine($"Attempt Successful -> {0}", e.Message);
            }
        }

        private void RunImplicitTest()
        {
            Console.WriteLine();
            try
            {
                var element = AddElement(new DummyImplicit());

                Console.WriteLine("Attempting to throw 'DBConcurrencyException' with use of 'Implicit Concurrency Check'");

                var result1 = UserOneAsync(element);
                var result2 = UserTwoAsync(element);

                Console.WriteLine($"Attempt Failed -> User One: {0}, User Two: {1}", result1.Result, result2.Result);
            }
            catch (DBConcurrencyException e)
            {
                Console.WriteLine($"Attempt Successful -> {0}", e.Message);
            }
        }

        private T AddElement<T>(T element) where T : class, IEntity
        {
            int count = repo.FindMultiple(element).Count;

            IEntity toAdd = null;

            switch (element)
            {
                case IDummyTimestamp t:
                    Console.WriteLine("Makeing a DummyTimestamp...");
                    toAdd = new DummyTimestamp() {ArbitraryInt = count, ArbitraryString = "I use Row version / Timestamp for Concurrency Check"};
                    break;
                case IDummyExplicit e:
                    Console.WriteLine("Makeing a DummyExplicit...");
                    toAdd = new DummyExplicit() {ArbitraryInt = count, ArbitraryString = "I use my ArbitraryInt for Concurrency Check" };
                    break;
                case IDummyImplicit i:
                    Console.WriteLine("Makeing a DummyImplicit...");
                    toAdd = new DummyImplicit() {ArbitraryInt = count, ArbitraryString = "I'm not sure i do any Concurrency Check"};
                    break;
                default:
                    break;
            }


            repo.Add(toAdd);
            repo.Save();

            return repo.Find(toAdd) as T;
        }

        private async Task<bool> UserOneAsync<T>(T element) where T : class, IEntity
        {
            ((IDummy) element).ArbitraryInt = ((IDummy) element).ArbitraryInt * 3;

            var task = await Task.Run(() => repo.Update(element));

            repo.Save();

            return task;
        }

        private async Task<bool> UserTwoAsync<T>(T element) where T : class, IEntity
        {
            ((IDummy)element).ArbitraryInt = ((IDummy)element).ArbitraryInt * 9;

            var random = new Random();
            Thread.Sleep(random.Next(2000, 6000));

            var task = await Task.Run(() => repo.Update(element));

            repo.Save();

            return task;
        }
    }
}