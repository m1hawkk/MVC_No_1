//// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.
////<script>
////    document.addEventListener("DOMContentLoaded", function () {
////        // Mô phỏng quá trình tải dữ liệu
////        fetch("/api/your-big-data-endpoint")  // API thật hoặc controller trả dữ liệu
////            .then(response => response.json())
////            .then(data => {
////                // xử lý dữ liệu ở đây
////                document.getElementById("loadingOverlay").style.display = "none";
////                // render dữ liệu ra table hoặc view
////            });
////    });
////</script>


//document.addEventListener("DOMContentLoaded", function () {
//    loadData();
//});

//function loadData() {
//    const overlay = document.getElementById("loadingOverlay");
//    overlay.style.display = "flex";

//    fetch("/api/your-big-data-endpoint")
//        .then(response => response.json())
//        .then(data => {
//            const tbody = document.querySelector("#dataTable tbody");

//            // Xóa dữ liệu cũ nếu có
//            tbody.innerHTML = "";

//            // Render dữ liệu
//            data.forEach(item => {
//                const row = document.createElement("tr");
//                row.innerHTML = `<td>${item.name}</td><td>${item.age}</td>`;
//                tbody.appendChild(row);
//            });

//            // Đợi DOM cập nhật xong
//            requestAnimationFrame(() => {
//                overlay.style.display = "none"; // Ẩn loading sau khi DOM cập nhật xong
//            });
//        })
//        .catch(err => {
//            console.error("Lỗi khi tải dữ liệu:", err);
//            overlay.style.display = "none";
//        });
//}
