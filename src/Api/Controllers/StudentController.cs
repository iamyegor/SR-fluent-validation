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

    public StudentController(StudentRepository studentRepository, CourseRepository courseRepository)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }

    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        Address[] addresses = request
            .Addresses.Select(x => new Address(x.Street, x.City, x.State, x.ZipCode))
            .ToArray();

        var emailOrError = Email.Create(request.Email);
        if (emailOrError.IsFailure)
        {
            return BadRequest(emailOrError.ErrorMessage);
        }

        var nameOrError = StudentName.Create(request.Name);
        if (nameOrError.IsFailure)
        {
            return BadRequest(emailOrError.ErrorMessage);
        }

        var student = new Student(emailOrError, nameOrError, addresses);
        _studentRepository.Save(student);

        var response = new RegisterResponse { Id = student.Id };
        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult EditPersonalInfo(long id, EditPersonalInfoRequest request)
    {
        Student student = _studentRepository.GetById(id);

        Address[] addresses = request
            .Addresses.Select(x => new Address(x.Street, x.City, x.State, x.ZipCode))
            .ToArray();

        // student.EditPersonalInfo(request.Name, addresses);
        _studentRepository.Save(student);

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
                    State = x.State,
                    ZipCode = x.ZipCode
                })
                .ToArray(),
            Email = student.Email.Value,
            Name = student.Name.Value,
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
