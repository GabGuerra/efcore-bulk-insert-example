using Bogus;
using efcore_bulk_insert_example.Models;
using efcore_bulk_insert_example.Repository.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace efcore_bulk_insert_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private ICustomersRepository _customersRepository;
        public ExampleController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }


        [HttpGet("without-bulk")]
        public async Task<IActionResult> WithoutBulk()
        {
            var sw = Stopwatch.StartNew();

            var faker = new Faker<Customer>()
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Email, f => f.Internet.Email(f.Person.FirstName).ToLower())
            .RuleFor(x => x.BirthDate, f => f.Date.Recent(1000))
            .RuleFor(x => x.CreatedAt, f => f.Date.Recent(400))
            .RuleFor(x => x.SSN, f => f.Random.Number(9999).ToString())
            .RuleFor(x => x.PhoneNumber, f => f.Person.Phone);

            var values = faker.Generate(100000);

            await _customersRepository.AddRangeAsync(values);

            sw.Stop();
            return Ok($"Without bulk took {sw.ElapsedMilliseconds} ms");

        }

        [HttpGet("with-bulk")]
        public async Task<IActionResult> WithBulk()
        {
            var sw = Stopwatch.StartNew();
            var faker = new Faker<Customer>()
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Email, f => f.Internet.Email(f.Person.FirstName).ToLower())
                .RuleFor(x => x.BirthDate, f => f.Date.Recent(1000))
                .RuleFor(x => x.CreatedAt, f => f.Date.Recent(400))
                .RuleFor(x => x.SSN, f => f.Random.Number(9999).ToString())
                .RuleFor(x => x.PhoneNumber, f => f.Person.Phone);

            var values = faker.Generate(100000);

            await _customersRepository.BulkInsertAsync(values);

            sw.Stop();
            return Ok($"With bulk took {sw.ElapsedMilliseconds} ms");

        }
    }
}