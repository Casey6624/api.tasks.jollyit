﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TasksAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
            if(_context.TaskItems.Count() == 0)
            {
                _context.TaskItems.Add(new TaskItem { assignedTo = "Casey" });
                _context.SaveChanges();
            }
        }
        // GET api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            return await _context.TaskItems.ToListAsync();
        }

        // GET api/tasks/{id}
        [HttpGet("{taskID}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(long taskID)
        {
            var taskItem = await _context.TaskItems.FindAsync(taskID);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem item)
        {
            _context.TaskItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskItem), new { taskID = item.taskID }, item);
        }

        // PUT: api/task/1
        // put requires the whole request, whereas patch can be just a segment 
        [HttpPut("{taskID}")]
        public async Task<IActionResult> PutTaskItem(long taskID, TaskItem item)
        {
            if (taskID != item.taskID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Task/1
        [HttpDelete("{taskID}")]
        public async Task<IActionResult> DeleteTaskItem(long taskID)
        {
            var taskItem = await _context.TaskItems.FindAsync(taskID);

            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
