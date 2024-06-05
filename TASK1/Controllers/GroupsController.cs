using System;
using System.Collections.Generic;
using System.Linq;
using CourseApi.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using TASK1.Dtos.GroupDtos;

namespace CourseApi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class GroupsController : Controller {
    private readonly AppDbContext _context;

    public GroupsController(AppDbContext context) {
      _context = context;
    }

    [HttpGet("")]
    public ActionResult<List<GroupGetAllDto>> GetAll(int pageNumber = 1, int pageSize = 1) {
      if (pageNumber <= 0 || pageSize <= 0) {
        return BadRequest("Page number and page size must be greater than zero!");
      }
      var data = _context.Groups.Skip((pageNumber - 1) * pageSize)
          .Take(pageSize)
          .Select(x => new GroupGetAllDto {
            Name = x.Name,
            Limit = x.Limit
          }).ToList();

      return StatusCode(200, data);
    }

    [HttpGet("{id}")]
    public ActionResult<GroupGetOneDto> GetById(int id) {
      var data = _context.Groups.FirstOrDefault(x => x.Id == id);
      if (data == null) {
        return NotFound();
      }
      GroupGetOneDto groupGetOneDto = new() {
        Name = data.Name,
        Limit = data.Limit
      };

      return StatusCode(200, groupGetOneDto);
    }

    [HttpPost("")]
    public ActionResult Create(GroupCreateOneDto groupCreateOneDto) {
      Group group = new() {
        Name = groupCreateOneDto.Name,
        Limit = groupCreateOneDto.Limit
      };

      _context.Groups.Add(group);
      _context.SaveChanges();

      return StatusCode(201, new { group.Id });
    }

    [HttpPut("{id}")]
    public ActionResult Update(GroupUpdateOneDto groupUpdateOneDto) {
      var existingGroup = _context.Groups.FirstOrDefault(x => x.Id == groupUpdateOneDto.Id);
      if (existingGroup == null) {
        return NotFound();
      }
      existingGroup.Name = groupUpdateOneDto.Name;
      existingGroup.Limit = groupUpdateOneDto.Limit;

      _context.Groups.Update(existingGroup);
      _context.SaveChanges();

      return StatusCode(200);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id) {
      var existingGroup = _context.Groups.FirstOrDefault(x => x.Id == id);
      if (existingGroup == null) {
        return NotFound();
      }

      _context.Groups.Remove(existingGroup);
      _context.SaveChanges();

      return StatusCode(200);
    }
  }
}
