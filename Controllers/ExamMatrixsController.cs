﻿using backend_online_testing.Models;
using backend_online_testing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using backend_online_testing.Dtos;

namespace backend_online_testing.Controllers
{
    [Route("api/exam-matrix")]
    [ApiController]
    public class ExamMatrixsController : ControllerBase
    {
        private readonly ExamMatrixsService _examMatrixsService;

        public ExamMatrixsController(ExamMatrixsService examMatrixsService)
        {
            _examMatrixsService = examMatrixsService;
        }

        //Get all exam matrix
        [HttpGet]
        public async Task<IActionResult> GetAllExamsMatrix()
        {
            var examMatrixs_list = await _examMatrixsService.GetAllExamMatrix();
            if (examMatrixs_list == null || !examMatrixs_list.Any())
            {
                return NotFound(new { message = "No exam founds" });
            }
            return Ok(new { status = "Success", data = examMatrixs_list });
        }
        
        //Get by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdExamMatrix(string id)
        {
            var result = await _examMatrixsService.GetByIdExamMatrix(id);

            if (result == null)
            {
                return NotFound(new { message = "Exam matrix not found" });
            }

            return Ok(new { status = "Success", data = result });
        }

        //Searchby name
        [HttpGet("search-name")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var results = await _examMatrixsService.SearchByName(name);

            if (results == null || !results.Any())
            {
                return NotFound(new { message = "No exam matrix found" });
            }

            return Ok(new { status = "Success", data = results });
        }

        //Add exam matrix
        [HttpPost]
        public async Task<IActionResult> AddExamMatrix([FromBody] ExamMatrixsModel examMatrixData, string matrixLogUserId)
        {
            if (examMatrixData == null)
            {
                return BadRequest(new { message = "Invalid exam matrix data" });
            }

            var result = await _examMatrixsService.AddExamMatrix(examMatrixData, matrixLogUserId);

            if (result == "Exam matrix created successfully")
            {
                return Ok(new { status = "Success", message = result });
            }

            return BadRequest(new { status = "Failure", message = result });
        }

        //Add tag
        [HttpPost("tags/")]
        public async Task<IActionResult> AddTag(string examMatrixId, [FromBody] ExamMatrixAddDto tagsData)
        {
            var result = await _examMatrixsService.AddTag(tagsData);
            if (result == "Tags added successfully")
            {
                return Ok(new { status = "Success", message = result });
            }

            return BadRequest(new { status = "Failure", message = result });
        }

        //Update exam matrix
        [HttpPost("{examMatrixId}")]
        public async Task<IActionResult> UpdateMatrixExam(string examMatrixId, [FromBody] ExamMatrixUpdateDto examMatrixData)
        {
            var result = await _examMatrixsService.UpdateExamMatrix(examMatrixId, examMatrixData);
            if (result == "Exam matrix updated successfully")
            {
                return Ok(new { status = "Success", message = result });
            }

            return BadRequest(new { status = "Failure", message = result });
        }

        //Update tag
        [HttpPost("{examMatrixId}/tags/{tagName}")]
        public async Task<IActionResult> UpdateTags(string examMatrixId, [FromBody] ExamMatrixUpdateDto tagsData)
        {
            var result = await _examMatrixsService.UpdateTag(examMatrixId, tagsData);
            if (result == "Tags updated successfully")
            {
                return Ok(new { status = "Success", message = result });
            }

            return BadRequest(new { status = "Failure", message = result });
        }

        [HttpDelete("delete-exam-matrix")]
        public async Task<IActionResult> DeleteMatrixExam(string examMatrixId, string matrixLogUserId)
        {
            var result = await _examMatrixsService.DeleteExamMatrix(examMatrixId, matrixLogUserId);
            if (result == "Exam matrix marked as unavailable")
            {
                return Ok(new { status = "Success", message = result });
            }

            return BadRequest(new { status = "Failure", message = result });
        }

        [HttpDelete("delete-tag")]
        public async Task<IActionResult> DeleteTagExam(string examMatrixId, string tagName, string matrixLogUserId)
        {
            var result = await _examMatrixsService.DeleteTag(examMatrixId, tagName, matrixLogUserId);
            if (result == "Tag deleted successfully")
            {
                return Ok(new { status = "Success", message = result });
            }

            return BadRequest(new { status = "Failure", message = result });
        }

        //Insert form data
        [HttpGet("seed")]
        public async Task<IActionResult> SeedData()
        {
            await _examMatrixsService.SeedData();
            return new OkObjectResult(new { status = "Success", message = "Example exam matrix data seeded successfully." });
        }
    }
}
