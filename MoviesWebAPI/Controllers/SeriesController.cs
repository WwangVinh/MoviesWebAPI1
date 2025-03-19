using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesDB.Models;
using MoviesDB.Interfaces;

namespace MoviesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly MoviesDbContext _context;

        public SeriesController(MoviesDbContext context)
        {
            _context = context;
        }

        // GET: api/Series
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> GetSeries(
            string? search = null, // Tìm theo tên series
            string sortBy = "id",  // Trường sắp xếp (mặc định theo id)
            string sortDirection = "asc", // Hướng phân loại (asc/desc)
            int page = 1, // Số trang (mặc định là trang 1)
            int pageSize = 5 // Số mục trên mỗi trang (mặc định là 5)
        )
        {
            // Kiểm tra nếu page hoặc pageSize nhỏ hơn 1, đặt giá trị mặc định
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 5;

            // Khởi tạo query cơ bản cho bảng Series
            var query = _context.Series.AsQueryable();

            // Lọc theo tên series nếu có search
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{search}%"));
            }

            // Sắp xếp theo Title, Director, hoặc ID
            switch (sortBy?.ToLower())
            {
                case "name":
                    query = sortDirection.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.Title)
                        : query.OrderBy(s => s.Title);
                    break;

                case "id":
                    query = sortDirection.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.SeriesID)
                        : query.OrderBy(s => s.SeriesID);
                    break;

                default:
                    query = sortDirection.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.SeriesID)
                        : query.OrderBy(s => s.SeriesID);
                    break;
            }

            // Lấy tổng số bản ghi để tính phân trang
            var totalRecords = await query.CountAsync();

            // Phân trang: Skip và Take
            var seriesList = await query
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            // Trả về dữ liệu với thông tin phân trang
            return Ok(new
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Series = seriesList
            });
        }

        // GET: api/Series/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeriesById(int id)
        {
            var series = await _context.Series.FindAsync(id);

            if (series == null)
            {
                return NotFound();
            }

            return Ok(series);
        }

        // POST: api/Series
        [HttpPost]
        public async Task<ActionResult<Series>> CreateSeries([FromBody] Series series)
        {
            if (series == null)
            {
                return BadRequest("Invalid series data.");
            }

            _context.Series.Add(series);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeriesById), new { id = series.SeriesID }, series);
        }

        // PUT: api/Series/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeries(int id, [FromBody] Series series)
        {
            if (id != series.SeriesID)
            {
                return BadRequest("ID mismatch.");
            }

            _context.Entry(series).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Series/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }

            _context.Series.Remove(series);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Series deleted successfully." });
        }

        private bool SeriesExists(int id)
        {
            return _context.Series.Any(e => e.SeriesID == id);
        }
    }
}
