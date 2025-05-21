using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_No_1.Data;
using MVC_No_1.Models;

namespace MVC_No_1.Controllers
{
    public class ManagerController : Controller
    {
            private readonly ApplicationDbContext _context; // Khai báo biến DbContext

            // Sử dụng Dependency Injection để inject DbContext vào Controller
            public ManagerController(ApplicationDbContext context)
            {
                _context = context;
            }

            // HIỂN THỊ DANH SÁCH NHÂN VIÊN
            public async Task<IActionResult> Index2()
            {
                // Lấy tất cả nhân viên từ database
                // .ToListAsync() là một method bất đồng bộ, nên cần dùng await
                var employees = await _context.Employees.ToListAsync();
                return View(employees);
            }

        // GET: Manager/Create
        // Action này hiển thị form trống để thêm nhân viên mới
        public IActionResult Create()
        {
            return View(); // Trả về View Create.cshtml
        }

        // POST: Manager/Create
        // Action này xử lý dữ liệu từ form thêm mới và lưu vào database
        [HttpPost]
        [ValidateAntiForgeryToken] // RẤT QUAN TRỌNG CHO BẢO MẬT
        public async Task<IActionResult> Create(Employee employee)
        {
            // Kiểm tra xem dữ liệu Model gửi lên có hợp lệ không
            // (ví dụ: các trường Name, Position, Department có được điền không nếu bạn đã thêm [Required] vào Model)
            if (ModelState.IsValid)
            {
                // Thêm đối tượng employee vào bộ nhớ của DbContext
                _context.Add(employee);
                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();
                // Sau khi thêm thành công, chuyển hướng người dùng trở lại trang danh sách
                return RedirectToAction(nameof(Index2));
            }
            // Nếu Model không hợp lệ, hiển thị lại form với dữ liệu đã nhập và các thông báo lỗi
            return View(employee);
        }

        // ... (Các Action Index2, Create, Delete của bạn ở đây) ...

        // GET: Manager/Edit/5
        // Action này hiển thị form sửa thông tin cho một nhân viên cụ thể
        public async Task<IActionResult> Edit(int? id) // 'int?' cho phép Id có thể là null
                {
                    if (id == null)
                    {
                        // Nếu không có Id được cung cấp, trả về lỗi NotFound (HTTP 404)
                        return NotFound();
                    }

                    // Tìm nhân viên trong database dựa trên Id (FindAsync là cách tối ưu cho Primary Key)
                    var employee = await _context.Employees.FindAsync(id);

                    if (employee == null)
                    {
                        // Nếu không tìm thấy nhân viên, trả về lỗi NotFound
                        return NotFound();
                    }

                    // Truyền đối tượng nhân viên tìm được đến View Edit.cshtml để điền sẵn vào form
                    return View(employee);
                }

                // POST: Manager/Edit/5
                // Action này xử lý dữ liệu từ form sửa và cập nhật vào database
                [HttpPost]
                [ValidateAntiForgeryToken] // RẤT QUAN TRỌNG CHO BẢO MẬT
                public async Task<IActionResult> Edit(int id, Employee employee)
                {
                    // Kiểm tra xem Id trong URL có khớp với Id của đối tượng gửi lên không
                    if (id != employee.Id)
                    {
                        return NotFound();
                    }

                    // Kiểm tra xem dữ liệu Model gửi lên có hợp lệ không (ví dụ: các trường required đã được điền)
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            // Cập nhật đối tượng employee trong bộ nhớ của DbContext
                            _context.Update(employee);
                            // Lưu thay đổi vào database
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            // Xử lý trường hợp đối tượng đã bị xóa hoặc sửa bởi người khác
                            if (!_context.Employees.Any(e => e.Id == employee.Id))
                            {
                                return NotFound(); // Không tìm thấy đối tượng để cập nhật
                            }
                            else
                            {
                                throw; // Ném lại ngoại lệ nếu có lỗi khác
                            }
                        }
                        // Sau khi sửa thành công, chuyển hướng người dùng trở lại trang danh sách
                        return RedirectToAction(nameof(Index2));
                    }
                    // Nếu Model không hợp lệ, hiển thị lại form với dữ liệu đã nhập và các thông báo lỗi
                    return View(employee);
                }


        // GET: Manager/Delete/5
        // Action này hiển thị trang xác nhận xóa cho một nhân viên cụ thể
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                // Nếu không có Id được cung cấp, trả về lỗi NotFound (HTTP 404)
                return NotFound();
            }

            // Tìm nhân viên trong database dựa trên Id
            // FirstOrDefaultAsync đảm bảo rằng nếu không tìm thấy, nó sẽ trả về null thay vì ném lỗi
            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                // Nếu không tìm thấy nhân viên, trả về lỗi NotFound
                return NotFound();
            }

            // Truyền đối tượng nhân viên tìm được đến View Delete.cshtml
            return View(employee);
        }

        // POST: Manager/Delete/5
        // Action này thực hiện việc xóa nhân viên sau khi người dùng xác nhận
        [HttpPost, ActionName("Delete")] // Đặt tên action là "Delete" nhưng chỉ phản hồi cho POST requests
        [ValidateAntiForgeryToken] // Bảo vệ chống lại tấn công giả mạo yêu cầu chéo trang (CSRF)
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Tìm lại đối tượng nhân viên cần xóa trong database
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                // Xóa đối tượng khỏi DbSet (chưa xóa khỏi database thực tế)
                _context.Employees.Remove(employee);
                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();
            }
            else
            {
                // Tùy chọn: Nếu nhân viên không tìm thấy (có thể đã bị xóa bởi người khác), bạn có thể xử lý ở đây
                // Ví dụ: log lỗi hoặc hiển thị thông báo
            }

            // Sau khi xóa, chuyển hướng người dùng trở lại trang danh sách (Index2)
            return RedirectToAction(nameof(Index2));
        }
    }
    
    }
