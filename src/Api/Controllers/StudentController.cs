using System;
using System.Linq;
using Api.DTOs;
using Api.Repositories;
using DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/students")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly StudentRepository _studentRepository;
    private readonly CourseRepository _courseRepository;
    private readonly StatesRepository _statesRepository;

    public StudentController(
        StudentRepository studentRepository,
        CourseRepository courseRepository,
        StatesRepository statesRepository
    )
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _statesRepository = statesRepository;
    }

    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        string[] allStates = _statesRepository.GetAll();
        Address[] addresses = request
            .Addresses.Select(x =>
                Address.Create(x.Street, x.City, State.Create(x.State, allStates), x.ZipCode).Value
            )
            .ToArray();

        Email email = Email.Create(request.Email);
        string name = request.Name.Trim();
        Student student = new Student(email, name, addresses);

        _studentRepository.Save(student);

        var response = new RegisterResponse { Id = student.Id };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult EditPersonalInfo(long id, EditPersonalInfoRequest request)
    {
        // Student student = _studentRepository.GetById(id);
        //
        // Address[] addresses = request
        //     .Addresses.Select(x =>
        //         Address.Create(x.Street, x.City, State.Create(x.State,), x.ZipCode).Value
        //     )
        //     .ToArray();
        //
        // student.EditPersonalInfo(request.Name, addresses);
        // _studentRepository.Save(student);
        //
        return Ok();
    }

    [HttpPost("{id}/enrollments")]
    public IActionResult Enroll(long id, EnrollRequest request)
    {
        Student student = _studentRepository.GetById(id);

        foreach (CourseEnrollmentDto enrollmentDto in request.Enrollments)
        {
            Course course = _courseRepository.GetByName(enrollmentDto.Course);
            var grade = Enum.Parse<Grade>(enrollmentDto.Grade);

            student.Enroll(course, grade);
        }

        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        Student student = _studentRepository.GetById(id);

        var resonse = new GetResonse
        {
            Addresses = student
                .Addresses.Select(x => new AddressDto
                {
                    Street = x.Street,
                    City = x.City,
                    State = x.State.Value,
                    ZipCode = x.ZipCode
                })
                .ToArray(),
            Email = student.Email.Value,
            Name = student.Name,
            Enrollments = student
                .Enrollments.Select(x => new CourseEnrollmentDto
                {
                    Course = x.Course.Name,
                    Grade = x.Grade.ToString()
                })
                .ToArray()
        };
        return Ok(resonse);
    }
}
