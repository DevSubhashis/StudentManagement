
// Controller Layer: Exposes API endpoints for each of the required 
// queries and maps entities to DTOs for data transfer.

using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs;
using StudentManagement.Models;
using StudentManagement.Repository;

namespace StudentManagement.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;

        public StudentsController(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // a) Get all students with profile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetAllStudentsWithProfile()
        {
            var students = await _studentRepository.GetAllStudentsWithProfileAsync();
            return Ok(students.Select(s => MapStudentToDTO(s)));
        }

        // b) Get student with profile by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudentWithProfileById(int id)
        {
            var student = await _studentRepository.GetStudentWithProfileByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(MapStudentToDTO(student));
        }

        // c) Get all students with profile by Classroom ID
        [HttpGet("classroom/{classroomId}")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsByClassroomId(int classroomId)
        {
            var students = await _studentRepository.GetStudentsByClassroomIdAsync(classroomId);
            return Ok(students.Select(s => MapStudentToDTO(s)));
        }

        // d) Get all classrooms
        [HttpGet("classrooms")]
        public async Task<ActionResult<IEnumerable<ClassroomDTO>>> GetAllClassrooms()
        {
            var classrooms = await _studentRepository.GetAllClassroomsAsync();
            return Ok(classrooms.Select(c => new ClassroomDTO
            {
                ClassroomID = c.ClassroomID,
                ClassroomName = c.ClassroomName
            }));
        }

        // e) Get classroom by ID
        [HttpGet("classroom/{id}")]
        public async Task<ActionResult<ClassroomDTO>> GetClassroomById(int id)
        {
            var classroom = await _studentRepository.GetClassroomByIdAsync(id);
            if (classroom == null) return NotFound();
            return Ok(new ClassroomDTO
            {
                ClassroomID = classroom.ClassroomID,
                ClassroomName = classroom.ClassroomName
            });
        }

        // f) Get profile by Student ID
        [HttpGet("student/{studentId}/profile")]
        public async Task<ActionResult<ProfileDTO>> GetProfileByStudentId(int studentId)
        {
            var profile = await _studentRepository.GetProfileByStudentIdAsync(studentId);
            if (profile == null) return NotFound();
            return Ok(new ProfileDTO
            {
                ProfileID = profile.ProfileID,
                Address = profile.Address,
                DOB = profile.DOB
            });
        }

        // g) Get profile by Profile ID
        [HttpGet("profile/{profileId}")]
        public async Task<ActionResult<ProfileDTO>> GetProfileByProfileId(int profileId)
        {
            var profile = await _studentRepository.GetProfileByProfileIdAsync(profileId);
            if (profile == null) return NotFound();
            return Ok(new ProfileDTO
            {
                ProfileID = profile.ProfileID,
                Address = profile.Address,
                DOB = profile.DOB
            });
        }

        // h) Get students by Teacher ID
        [HttpGet("teacher/{teacherId}/students")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsByTeacherId(int teacherId)
        {
            var students = await _studentRepository.GetStudentsByTeacherIdAsync(teacherId);
            return Ok(students.Select(s => MapStudentToDTO(s)));
        }

        // i) Get teacher info by Student ID
        [HttpGet("student/{studentId}/teacher")]
        public async Task<ActionResult<TeacherDTO>> GetTeacherByStudentId(int studentId)
        {
            var teacher = await _studentRepository.GetTeacherByStudentIdAsync(studentId);
            if (teacher == null) return NotFound();
            return Ok(new TeacherDTO
            {
                TeacherID = teacher.TeacherID,
                Name = teacher.Name
            });
        }

        // j) Get subjects by Student ID
        [HttpGet("student/{studentId}/subjects")]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetSubjectsByStudentId(int studentId)
        {
            var subjects = await _studentRepository.GetSubjectsByStudentIdAsync(studentId);
            return Ok(subjects.Select(s => new SubjectDTO
            {
                SubjectID = s.SubjectID,
                SubjectName = s.SubjectName
            }));
        }

        // k) Get student with profile data by Subject ID
        [HttpGet("subject/{subjectId}/students")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsWithProfileBySubjectId(int subjectId)
        {
            var students = await _studentRepository.GetStudentsWithProfileBySubjectIdAsync(subjectId);
            return Ok(students.Select(s => MapStudentToDTO(s)));
        }

        // Helper method to map Student entity to DTO
        private StudentDTO MapStudentToDTO(Student student)
        {
            return new StudentDTO
            {
                StudentID = student.StudentID,
                Name = student.Name,
                Profile = new ProfileDTO
                {
                    ProfileID = student.Profile.ProfileID,
                    Address = student.Profile.Address,
                    DOB = student.Profile.DOB
                },
                Classroom = new ClassroomDTO
                {
                    ClassroomID = student.Classroom.ClassroomID,
                    ClassroomName = student.Classroom.ClassroomName
                },
                Teacher = new TeacherDTO
                {
                    TeacherID = student.Teacher.TeacherID,
                    Name = student.Teacher.Name
                },
                Subjects = student.StudentSubjects.Select(ss => new SubjectDTO
                {
                    SubjectID = ss.Subject.SubjectID,
                    SubjectName = ss.Subject.SubjectName
                }).ToList()
            };
        }
    }


}