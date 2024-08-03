using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class ClassController : Controller
    {
        private readonly SchoolDbContext _context;

        public ClassController()
        {
            _context = new SchoolDbContext();
        }

        public IActionResult Index()
        {
            var classes = _context.GetAllClasses();
            return View("List", classes);
        }

        public IActionResult Show(int id)
        {
            var classData = _context.GetClassById(id);
            if (classData == null)
            {
                return NotFound();
            }
            return View(classData);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Class classData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.AddClass(classData);
                    return Json(new { success = true, message = "Class created successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors = errors });
        }

        public IActionResult Edit(int id)
        {
            var classData = _context.GetClassById(id);
            if (classData == null)
            {
                return NotFound();
            }
            return View(classData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Class classData)
        {
            if (id != classData.ClassId)
            {
                return Json(new { success = false, message = "Class not found" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.UpdateClass(classData);
                    return Json(new { success = true, message = "Class updated successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors = errors });
        }

        public IActionResult Delete(int id)
        {
            var classData = _context.GetClassById(id);
            if (classData == null)
            {
                return NotFound();
            }
            return View(classData);
        }

        [HttpDelete]
        [Route("~/Class/Delete/{id}")]
        public IActionResult DeleteClass(int id)
        {
            var classEntity = _context.GetClassById(id);
            if (classEntity == null)
            {
                return NotFound();
            }
            _context.DeleteClass(id);
            return Ok();
        }
    }
}
